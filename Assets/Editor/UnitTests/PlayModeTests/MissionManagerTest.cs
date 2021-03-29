using System.Collections;
using ClientMissions;
using ClientMissions.Controllers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Utilities;

namespace Editor.UnitTests.PlayModeTests{
    public class MissionManagerTest
    {
        [UnityTest]
        public IEnumerator MissionManagerWithEnumeratorPasses()
        {
            var gameObject = GameObject.Instantiate(Resources.Load("MissionManagerTestPrefab") as GameObject);
            var missionManager = gameObject.GetComponent<Missions>();
            Assert.AreEqual(typeof(Missions), missionManager.GetType());
            yield return null;
        }
        //TODO: Create tests...
    }
}
