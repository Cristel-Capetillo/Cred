using System.Collections;
using ClientMissions;
using ClientMissions.MissionMessages;
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
            var missionManager = gameObject.GetComponent<MissionManager>();
            Assert.AreEqual(typeof(MissionManager), missionManager.GetType());
            yield return null;
        }
        [UnityTest]
        public IEnumerator MissionManagerRemoveTest()
        {
            var gameObject = GameObject.Instantiate(Resources.Load("MissionManagerTestPrefab") as GameObject);
            var missionManager = gameObject.GetComponent<MissionManager>();
            yield return new WaitForSeconds(3);
            Debug.Log("Start amount: "+ missionManager.ActiveMissions.Count);
            for (var i = 0; i < 10; i++){
                var tempMission = missionManager.ActiveMissions[2];
                EventBroker.Instance().SendMessage(new SelectMissionMessage(tempMission));
                Debug.Log("Current mission: " + missionManager.CurrentMission.ClientTestData.name);
                missionManager.RemoveMission();
                Assert.AreNotEqual(missionManager.ActiveMissions[2], tempMission);
                Assert.AreEqual(3, missionManager.ActiveMissions.Count);
            }
            yield return null;
        }
        //TODO: Create tests...
    }
}
