using System.Collections;
using System.Collections.Generic;
using Cred.AddressableLoadSystem;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Editor.UnitTests.PlayModeTests{
    public class MonoBehaviorAddressableTests
    {
        //For multiple tests setup use [OneTimeSetUp]
        [Test]
        public void MonoBehaviorAddressableSimpleTest()
        {
            var gameObject = new GameObject("GameObject");
            var addressableHandler = gameObject.AddComponent<AddressableManager>();
            Debug.Log(addressableHandler.AssetGroupReferencesCount);
            Assert.AreEqual(typeof(AddressableManager), addressableHandler.GetType());
        }
        [Test]
        public void MonoBehaviorAddressableTestWithResourceLoadPrefabInstantiate(){
            var gameObject = GameObject.Instantiate(Resources.Load("AddressableManager") as GameObject);
            var addressableHandler = gameObject.GetComponent<AddressableManager>();
            Debug.Log(addressableHandler.AssetGroupReferencesCount);
            Assert.AreEqual(typeof(AddressableManager),addressableHandler.GetType());
        }
        [UnityTest]
        public IEnumerator MonoBehaviorAddressableTestWithSceneLoading(){
            var loadSceneAsync = SceneManager.LoadSceneAsync("AddressableTestScene");
            yield return new WaitUntil(()=>loadSceneAsync.isDone);
            
            var addressableHandler = GameObject.FindObjectOfType<AddressableManager>();
            Debug.Log(addressableHandler.AssetGroupReferencesCount);
            Assert.AreEqual(typeof(AddressableManager),addressableHandler.GetType());
            yield return null;
        }

        [UnityTest]
        public IEnumerator AsyncLoadAddressableLabelPackage(){
            var gameObject = GameObject.Instantiate(Resources.Load("AddressableManager") as GameObject);
            var addressableManager = gameObject.GetComponent<AddressableManager>();
            addressableManager.LoadAssetPackageAsync("Pants");
            Debug.Log("start: "+addressableManager.MaterialList.Count);
            yield return new WaitUntil(()=>addressableManager.IsLoaded);
            Debug.Log("After wait: "+addressableManager.MaterialList.Count);
            Assert.AreEqual(typeof(AddressableManager),addressableManager.GetType());
            // Assert.AreEqual(typeof(Material), addressableManager.MaterialList[0].GetType());
            yield return null;
        }
    }
}
