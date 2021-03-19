using MysteryBox;
using UnityEngine;
using UnityEngine.UI;

namespace HUD.MysteryBox {
    public class SelectorMenu : MonoBehaviour {
        
        [SerializeField] RectTransform selectionArea;
        [SerializeField] GameObject mysteryBoxButtonPrefab;
        
        [SerializeField] LootTable basicLootTable;
        [SerializeField] LootTable normalLootTable;

        public void CloseSelectorMenu() {
            Destroy(this.gameObject);
        }

        public void GenerateMysteryBoxes(int quantity) {
            var whichOneWillBeBetter = Random.Range(0, quantity);
            
            for (int i = 0; i < quantity; i++) {
                var instance = Instantiate(mysteryBoxButtonPrefab, selectionArea);
                instance.GetComponent<MysteryBoxButton>().AssignLootTable(i == whichOneWillBeBetter ? normalLootTable : basicLootTable);
                instance.GetComponent<Button>().onClick.AddListener(instance.GetComponent<MysteryBoxButton>().SpawnMysteryBox);
                instance.GetComponent<Button>().onClick.AddListener(CloseSelectorMenu);
            }
        }

        public void GenerateAllTopTierMysteryBoxes(int quantity) {
            for (int i = 0; i < quantity; i++) {
                var instance = Instantiate(mysteryBoxButtonPrefab, selectionArea);
                instance.GetComponent<MysteryBoxButton>().AssignLootTable(normalLootTable);
            }
        }
    }
}