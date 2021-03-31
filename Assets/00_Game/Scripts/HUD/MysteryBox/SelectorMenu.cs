using System.Collections.Generic;
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

        [Space] [Header("Loot Tables for Mystery Boxes")] 
        [SerializeField] int howManyMysteryBoxesToSpawn = 6;
        [SerializeField] int howManyHigherTierMysteryBoxes = 2;
        [SerializeField] LootTable basicLootTable;
        [SerializeField] LootTable normalLootTable;

        void Start() {
            titleText.text = title;
            descriptionText.text = description;
            GenerateMysteryBoxes(howManyMysteryBoxesToSpawn);
        }

        public void GenerateMysteryBoxes(int range) {
            var whichOneWillBeBetter = GenerateRandomNumbers(howManyHigherTierMysteryBoxes, 0, range);
            
            for (int i = 0; i < range; i++) {
                var instance = Instantiate(selectionButtonPrefab, selectionArea);
                instance.GetComponent<SelectionButton>().AssignLootTable(whichOneWillBeBetter.Contains(i) ? normalLootTable : basicLootTable);
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
        
        List<int> GenerateRandomNumbers(int quantity, int rangeMin, int rangeMax) {
            var randomNumbers = new List<int>();

            while (randomNumbers.Count < quantity) {
                var tmpRandom = Random.Range(rangeMin, rangeMax);
                if(!randomNumbers.Contains(tmpRandom))
                    randomNumbers.Add(tmpRandom);
            }

            foreach (var number in randomNumbers) {
                Debug.Log("Normal Loot in box : " +(number+1));
            }
            return randomNumbers;
        }
    }
}