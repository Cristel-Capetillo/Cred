using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace HUD {
    public class ClientSelect : MonoBehaviour {
        
        public GameObject clientGroup;
        public GameObject clientMenuButton;
        public GameObject clientNarrations;
        public TMP_Text narrationText;

        [SerializeField] Client selectedClient;
        SelectedClient sc;

        SwapScene ss;

        void Start() {
            sc = FindObjectOfType<SelectedClient>();
            ss = GetComponent<SwapScene>();
        }

        public void ToggleMenu() {
            clientGroup.SetActive(true);
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
            selectedClient = cl;
            clientNarrations.SetActive(true);
            clientGroup.SetActive(false);
            DisplayNarration();
        }

        public void ConfirmSelection() {
            sc.client = selectedClient;
            ss.LoadScene("DressupScene");
        }

        void DisplayNarration() {
            narrationText.text = selectedClient.narration;
        }
    }
}
