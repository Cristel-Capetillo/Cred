using UnityEngine;
using UnityEngine.EventSystems;

namespace HUD
{
    public class ButtonFeedback : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
        float originalSizeX;
        float originalSizeY;

        public void Start() {
            RectTransform rectTransform = GetComponent<RectTransform>();
            var sizeDelta = rectTransform.sizeDelta;
            originalSizeX = sizeDelta.x;
            originalSizeY = sizeDelta.y;
        }

        public void OnPointerUp(PointerEventData eventData) {
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(originalSizeX, originalSizeY);
        }

        public void OnPointerDown(PointerEventData eventData) {
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(originalSizeX * 1.2f, originalSizeY * 1.2f);
        }
    }
}
