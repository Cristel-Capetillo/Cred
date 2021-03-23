using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using Clothing;
using HUD.Clothing;
using NUnit.Framework;
using UnityEngine.TestTools;
using Random = UnityEngine.Random;

namespace Editor.UnitTests.PlayModeTests{
    public class InventoryTests : MonoBehaviour{
        [Test]
        public void InstantiationSimpleTest(){
            var gameObject = Instantiate(Resources.Load("InventoryTestPrefab") as GameObject);
            var inventoryDataHandler = gameObject.GetComponent<InventoryDataHandler>();
            Assert.AreEqual(typeof(InventoryDataHandler), inventoryDataHandler.GetType());
        }
        [UnityTest]
        public IEnumerator SortListByRarity(){
            var gameObject = Instantiate(Resources.Load("InventoryTestPrefab") as GameObject);
            var inventoryDataHandler = gameObject.GetComponent<InventoryDataHandler>();
            var addressableMock = gameObject.GetComponent<AddressableLoaderMock>();
            yield return new WaitForSeconds(1f);
            var randomNumber = Random.Range(1, 10);
            for (int i = 0; i < randomNumber; i++){
                addressableMock.SendWearablePantsMessage();
            }

            var index = 0;
            foreach (var item in inventoryDataHandler.wearableDictionary.First().Value){
                Debug.Log($"{index}: {item.name} {item.Rarity.name} {item.BodyPart.name}");
                index++;
            }
            Assert.AreEqual(addressableMock.common.name, inventoryDataHandler.wearableDictionary[addressableMock.pants].First().Rarity.name);
            Assert.AreEqual(addressableMock.epic.name, inventoryDataHandler.wearableDictionary[addressableMock.pants].Last().Rarity.name);
           yield return null;
        }

        [UnityTest]
        public IEnumerator ToggleUIContentTest(){
            var gameObject = Instantiate(Resources.Load("InventoryTestPrefab") as GameObject);
            var inventoryCanvas = gameObject.GetComponent<InventoryCanvas>();
            var addressableMock = gameObject.GetComponent<AddressableLoaderMock>();
            var inventoryDataHandler = gameObject.GetComponent<InventoryDataHandler>();
            yield return new WaitForSeconds(1f);
            
            addressableMock.SendWearablePantsMessage();
            var randomNumber = Random.Range(5, 10);
            for (int i = 0; i < randomNumber; i++){
                addressableMock.SendWearableShirtsMessage();
                if (i % 2 == 1){
                    inventoryCanvas.ToggleButton(addressableMock.pants);
                    Assert.LessOrEqual(inventoryDataHandler.wearableDictionary[addressableMock.pants].Count, inventoryCanvas.InventoryContentCount);
                    Debug.Log($"Pants: {inventoryDataHandler.wearableDictionary[addressableMock.pants].Count} <= Hidden+Visible UI prefabs: {inventoryCanvas.InventoryContentCount}");
                }
                else{
                    inventoryCanvas.ToggleButton(addressableMock.shirt);
                    Assert.LessOrEqual(inventoryDataHandler.wearableDictionary[addressableMock.shirt].Count, inventoryCanvas.InventoryContentCount);
                    Debug.Log($"Shirts: {inventoryDataHandler.wearableDictionary[addressableMock.shirt].Count} <= Hidden+Visible UI prefabs: {inventoryCanvas.InventoryContentCount}");
                }
            }
            yield return null;
        }
    }
}
