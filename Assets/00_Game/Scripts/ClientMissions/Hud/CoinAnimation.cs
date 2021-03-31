using UnityEngine;
namespace ClientMissions.Hud {
    public class CoinAnimation : MonoBehaviour
    {
        [SerializeField] [Range(0f, 4f)] float lerpTime;
        public Transform finalDestination;
        Vector3 vectorFinalDes;
        float movement = 0f;

        void Start() {
            SetCoinImagePosition();
        }

        void Update() {
            DoCoinAnimation();
        }

        void SetCoinImagePosition() {
            vectorFinalDes = 
                new Vector3(finalDestination.localPosition.x, finalDestination.localPosition.y, finalDestination.localPosition.z);
        }
        
        void DoCoinAnimation() {
            transform.localPosition = Vector3.Lerp(transform.localPosition, vectorFinalDes, lerpTime * Time.deltaTime);

            movement = Mathf.Lerp(movement, 1f, lerpTime * Time.deltaTime);

            if (movement > .9f) {
                movement = 0f;
                Destroy(gameObject);
            }
        }
    }
}
