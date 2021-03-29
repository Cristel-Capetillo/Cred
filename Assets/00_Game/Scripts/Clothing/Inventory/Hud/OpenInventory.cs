using System;
using Clothing.Upgrade;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Utilities;

namespace Clothing.Inventory {
    public class OpenInventory : MonoBehaviour {
        public Canvas canvas;
        public GameObject[] categories;

        Vector3 topPosition;
        Vector3 oldPosition;

        void Start() {
            topPosition = categories[0].GetComponent<RectTransform>().localPosition;
            EventBroker.Instance().SubscribeMessage<EventSceneSwap>(ValidateScene);

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("DressupScene")) {
                canvas.enabled = true;
            }
            else {
                canvas.enabled = false;
            }
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventSceneSwap>(ValidateScene);
        }

        public void ToggleInventory(CanvasGroup scrollView) {
            EventBroker.Instance().SendMessage(new EventTogglePopWindow(false));
            EventBroker.Instance().SendMessage(new EventUpdateCombinedUI(null));
            ChangeCanvasGroupValues(!scrollView.interactable, scrollView);

            foreach (var category in categories) {
                if (EventSystem.current.currentSelectedGameObject != category) {
                    category.SetActive(!scrollView.interactable);
                }
                else {
                    if (scrollView.interactable) {
                        oldPosition = category.GetComponent<RectTransform>().localPosition;
                        category.GetComponent<RectTransform>().localPosition = topPosition;
                    }
                    else {
                        category.GetComponent<RectTransform>().localPosition = oldPosition;
                    }
                }
            }
        }

        public void ShowUpgradeIcons(CanvasGroup canvasGroup) {
            ChangeCanvasGroupValues(!canvasGroup.interactable, canvasGroup);
        }

        void ValidateScene(EventSceneSwap sceneSwap) {
            canvas.enabled = SceneManager.GetActiveScene() == SceneManager.GetSceneByName("DressupScene");
        }

        void ChangeCanvasGroupValues(bool value, CanvasGroup canvasGroup) {
            canvasGroup.interactable = value;
            canvasGroup.blocksRaycasts = value;
            canvasGroup.alpha = canvasGroup.interactable ? 1 : 0;
        }
    }
}