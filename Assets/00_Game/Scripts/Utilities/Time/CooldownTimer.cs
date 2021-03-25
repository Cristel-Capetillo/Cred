using System;
using System.Collections;
using UnityEngine;

namespace Utilities.Time {
    public class CooldownTimer : MonoBehaviour {
        
        public Action<float> timeRemaining;
        public Action onComplete;
        public bool Started { get; private set; }
        public float Duration { get; private set; }

        public void StartTimer(float duration) {
            Started = true;
            this.Duration = duration;
            StartCoroutine(Tick(duration, 1f));
        }

        public void StartTimer(float duration, Action onComplete) {
            StartTimer(duration);
            this.onComplete = onComplete;
        }

        public void RestartTimer() {
            StopTimer();
            StartTimer(Duration);
        }
        
        public void StopTimer() {
            StopCoroutine(nameof(Tick));
            Started = false;
        }
        
        IEnumerator Tick(float duration, float interval) {
            
            while (duration > 0) {
                yield return new WaitForSeconds(interval);
                duration -= interval;
                timeRemaining?.Invoke(duration);
            }

            Started = false;
            onComplete?.Invoke();
        }

        void OnDestroy() {
            CancelInvoke(nameof(Tick));
        }
    }
}