using Cred.Coins;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor.UnitTests.PlayModeTests {
    public class LoadTest {
        [Test]
        public void TestNull() {
            var saveHandler = new GameObject("GO");
            var coin = saveHandler.AddComponent<Coin>();
            Assert.IsNotNull(coin);
            
        }

        [Test]
        public void TestSaving() {
            var saveHandler = new GameObject("GO");
            var coin = saveHandler.AddComponent<Coin>();
            coin.Coins = 1;
            Assert.AreEqual(coin.Coins, 1);
        }

        [Test]
        public void TestLoading() {
            var gameObject = new GameObject("GameObject");
            var coin = gameObject.AddComponent<Coin>();
            coin.OnLoad(3);
            Assert.AreEqual(coin.Coins, 3);
        }
    }
}