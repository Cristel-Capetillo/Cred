using System.Collections;
using Game.Scripts.AddressableLoadSystem;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

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
    }
}
