using Core;
using UnityEngine;
using UnityEngine.UI;

namespace HUD {
    public class ClientSelect : MonoBehaviour {
        public GameObject clientGroup;
        public GameObject clientMenuButton;
        public GameObject clientNarrations;
        public Text narrationText;

        [SerializeField] Client selectedClient;
        SelectedClient sc;

        SwapScene ss;

        void Start() {
            sc = FindObjectOfType<SelectedClient>();
            ss = GetComponent<SwapScene>();
        }

        public void ToggleMenu() {
            clientGroup.SetActive(true);
            var clientObjects = GameObject.Find("Clients").GetComponentsInChildren<Button>();
            foreach (var child in clientObjects) {
                // clients.Add(child.gameObject);
            }

            clientMenuButton.SetActive(false);
        }

        public void ExitMenu() {
            clientGroup.SetActive(false);
            clientMenuButton.SetActive(true);
        }

        public void BackOut() {
            clientNarrations.SetActive(false);
            clientGroup.SetActive(true);
        }

        public void MakeSelection(Client cl) {
            // Debug.Log(EventSystem.current.currentSelectedGameObject);
            selectedClient = cl;
            clientNarrations.SetActive(true);
            clientGroup.SetActive(false);
            DisplayNarration();
        }

        public void ConfirmSelection() {
            //Close all menus & swap model
            sc.selectedClient = selectedClient;
            ss.LoadScene("DressupScene");
        }

        void DisplayNarration() {
            narrationText.text = selectedClient.narration;
        }
    }
}