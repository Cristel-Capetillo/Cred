using System;
using MysteryBox;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace HUD.MysteryBox {
    public class SelectorMenu : MonoBehaviour {

        [Header("Selection Windows Title")]
        [SerializeField] Text titleText;
        [SerializeField] string title;
        [SerializeField] Text descriptionText;
        [SerializeField] string description;
        
        [Space][Header("Loot Box Selection Area")]
        [SerializeField] RectTransform selectionArea;
        [SerializeField] GameObject selectionButtonPrefab;
        
        [Space][Header("Loot Tables for Mystery Boxes")]
        [SerializeField] LootTable basicLootTable;
        [SerializeField] LootTable normalLootTable;

        
        void Start() {
            titleText.text = title;
            descriptionText.text = description;
            GenerateMysteryBoxes(6);
        }

        public void GenerateMysteryBoxes(int quantity) {
            var whichOneWillBeBetter = Random.Range(0, quantity);
            
            for (int i = 0; i < quantity; i++) {
                var instance = Instantiate(selectionButtonPrefab, selectionArea);
                instance.GetComponent<SelectionButton>().AssignLootTable(i == whichOneWillBeBetter ? normalLootTable : basicLootTable);
                instance.GetComponent<Button>().onClick.AddListener(instance.GetComponent<SelectionButton>().SpawnMysteryBox);
                instance.GetComponent<Button>().onClick.AddListener(CloseSelectorMenu);
            }
        }

        public void GenerateAllTopTierMysteryBoxes(int quantity) {
            for (int i = 0; i < quantity; i++) {
                var instance = Instantiate(selectionButtonPrefab, selectionArea);
                instance.GetComponent<SelectionButton>().AssignLootTable(normalLootTable);
                instance.GetComponent<Button>().onClick.AddListener(instance.GetComponent<SelectionButton>().SpawnMysteryBox);
                instance.GetComponent<Button>().onClick.AddListener(CloseSelectorMenu);
            }
        }
        
        public void CloseSelectorMenu() {
            Destroy(this.gameObject);
        }
    }
}