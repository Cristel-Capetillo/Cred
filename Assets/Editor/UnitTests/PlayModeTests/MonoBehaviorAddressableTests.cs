using System.Collections;
using System.Collections.Generic;
using Cred.AddressableLoadSystem;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.AddressableAssets;

namespace Editor.UnitTests.PlayModeTests{
    public class MonoBehaviorAddressableTests
    {
        [Test]
        public void MonoBehaviorAddressableSimpleTest()
        {
            var gameObject = new GameObject("GameObject");
            var addressableHandler = gameObject.AddComponent<AddressableManager>();
            Assert.AreEqual(typeof(AddressableManager), addressableHandler.GetType());
        }
        [Test]
        public void MonoBehaviorAddressableTestWithResourceLoadPrefabInstantiate(){
            var gameObject = GameObject.Instantiate(Resources.Load("AddressableManager") as GameObject);
            var addressableHandler = gameObject.GetComponent<AddressableManager>();
            Assert.AreEqual(typeof(AddressableManager),addressableHandler.GetType());
        }
        [UnityTest]
        public IEnumerator MonoBehaviorLoadAddressableAssetsWithLabelReference(){
            var gameObject = GameObject.Instantiate(Resources.Load("AddressableManager") as GameObject);
            var addressableManager = gameObject.GetComponent<AddressableManager>();
            addressableManager.LoadAssetGroup(addressableManager.AssetLabelReferences[0]);
            yield return new WaitForSeconds(0.5f);
            Debug.Log(addressableManager.ListCount);
            Assert.Less(0, addressableManager.ListCount);
            yield return null;
        }
        [UnityTest]
        public IEnumerator MonoBehaviorLoadAddressableAssetsWithMultipleLabelReference(){
            var gameObject = GameObject.Instantiate(Resources.Load("AddressableManager") as GameObject);
            var addressableManager = gameObject.GetComponent<AddressableManager>();
            addressableManager.PrepareLoadingMultipleAssetGroups(addressableManager.AssetLabelReferences);
            yield return new WaitForSeconds(0.5f);
            Debug.Log(addressableManager.ListCount);
            Assert.Less(2, addressableManager.ListCount);
            yield return null;
        }
    }
}
