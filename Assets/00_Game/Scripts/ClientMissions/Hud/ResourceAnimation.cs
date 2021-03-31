using UnityEngine;

namespace ClientMissions.Hud {
    public class ResourceAnimation : MonoBehaviour {
        [SerializeField, Range(0f, 4f)]
        float lerpTime;
        public Transform finalDestination;
        float movement;
        Vector3 vectorFinalDes;

        void Start() {
            SetResourceSpritePosition();
        }

        void Update() {
            DoResourceAnimation();
        }

        void SetResourceSpritePosition() {
            var localPosition = finalDestination.localPosition;
            vectorFinalDes =
                new Vector3(localPosition.x, localPosition.y, localPosition.z);
        }

        void DoResourceAnimation() {
            transform.localPosition = Vector3.Lerp(transform.localPosition, vectorFinalDes, lerpTime * Time.deltaTime);

            movement = Mathf.Lerp(movement, 1f, lerpTime * Time.deltaTime);

            if (movement > .9f) {
                movement = 0f;
                Destroy(gameObject);
            }
        }
    }
}