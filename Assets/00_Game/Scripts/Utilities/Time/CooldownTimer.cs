using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.Time {
    public class CooldownTimer : MonoBehaviour {

        float countDown;
        Action toPerform;
        public Text timerText;

        public void StartTimer(float countDown) {
            this.countDown = countDown;
            StartCoroutine(tickdown(countDown));
        }

        public void StartTimer(float countDown, Action onComplete) {
            this.countDown = countDown;
            toPerform = onComplete;
            StartCoroutine(tickdown(countDown));
        }

        IEnumerator tickdown(float duration) {
            countDown = duration;
            while (countDown > 0) {
                yield return new WaitForSeconds(1f);
                countDown -= 1f;
                timerText.text = duration.ToString("000:00");
            }

            countDown = 0;
            timerText.text = "Ready";
            toPerform?.Invoke();
        }

        public void OnComplete(Action onComplete) {
            toPerform = onComplete;
        }
    }
}