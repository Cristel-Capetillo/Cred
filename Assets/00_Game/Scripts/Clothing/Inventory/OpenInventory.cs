using System;
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

        public void ToggleInventory(GameObject scrollView) {
            scrollView.SetActive(!scrollView.activeSelf);

            foreach (var category in categories) {
                if (EventSystem.current.currentSelectedGameObject != category) {
                    category.SetActive(!scrollView.activeSelf);
                }
                else {
                    if (scrollView.activeSelf) {
                        oldPosition = category.GetComponent<RectTransform>().localPosition;
                        category.GetComponent<RectTransform>().localPosition = topPosition;
                    }
                    else {
                        category.GetComponent<RectTransform>().localPosition = oldPosition;
                    }
                }
            }
        }

        void ValidateScene(EventSceneSwap sceneSwap) {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("DressupScene")) {
                canvas.enabled = true;
            }
            else {
                canvas.enabled = false;
            }
        }
    }
}