using System.Collections;
using System.Collections.Generic;
using ClientMissions;
using ClientMissions.Controllers;
using ClientMissions.Data;
using ClientMissions.Requirements;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor.UnitTests.PlayModeTests{
    public class MissionGeneratorTest{
        
        [UnityTest]
        public IEnumerator CycleTestGenerateSavableDataMissionsNullChecks(){
            var gameObject = GameObject.Instantiate(Resources.Load("MissionControllerTestPrefab") as GameObject);
            var missionInitializer = gameObject.GetComponent<Initializer>();
            var missionGenerator = missionInitializer.CreateMissionGenerator();
            yield return new WaitForSeconds(1f); //<- Wait for start to finish...
        
            for (var i = 0; i <= 10; i++){
                var savableMissionData = missionGenerator.GenerateSavableMissionData();
                Debug.Log($"Test({i}): Name: {savableMissionData}, MissionIndex: {savableMissionData.MissionDifficultyIndex} ,ClientIndex: {savableMissionData.MissionClientIndex}");
                Assert.AreEqual(typeof(SavableMissionData), savableMissionData.GetType());
                Assert.AreEqual(typeof(SavableDialogData), savableMissionData.SavableDialogData.GetType());
                Assert.AreEqual(typeof(List<SavableRequirementData>), savableMissionData.SavableRequirementData.GetType());
            }
            yield return null;
        }
        [UnityTest]
        public IEnumerator CycleTestGenerateDataMissionsFromSavableDataMissionsNullChecks(){
            var gameObject = GameObject.Instantiate(Resources.Load("MissionControllerTestPrefab") as GameObject);
            var missionInitializer = gameObject.GetComponent<Initializer>();
            var missionGenerator = missionInitializer.CreateMissionGenerator();
            yield return new WaitForSeconds(1f); //<- Wait for start to finish...
        
            for (var i = 0; i <= 10; i++){
                var savableMissionData = missionGenerator.GenerateSavableMissionData();
                var missionData = missionInitializer.GetSavedMission(savableMissionData);
                Assert.AreEqual(typeof(MissionData), missionData.GetType());
                Assert.AreEqual(typeof(MissionDifficulty),missionData.Difficulty.GetType());
                Assert.AreEqual(typeof(List<IMissionRequirement>), missionData.Requirements.GetType());
                Assert.AreNotEqual(null, missionData.StylePointValues.GetType());
                Assert.AreEqual(typeof(ClientData), missionData.ClientData.GetType());
                Assert.AreEqual(typeof(SavableDialogData), missionData.SavableDialogData.GetType());
                Debug.Log($"Test: {i} {missionData.Difficulty.name}: ");
                foreach (var requirement in missionData.Requirements){
                    Debug.Log(requirement.ToString());
                }
            }
            yield return null;
        }
    }
}
