using System.Collections;
using System.Collections.Generic;
using ClientMissions;
using ClientMissions.Data;
using ClientMissions.MissionRequirements;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor.UnitTests.PlayModeTests{
    public class MissionGeneratorTest{
        
        [UnityTest]
        public IEnumerator CycleTestGenerateSavableDataMissions(){
            var gameObject = GameObject.Instantiate(Resources.Load("MissionControllerTestPrefab") as GameObject);
            var missionGenerator = gameObject.GetComponent<MissionGenerator>();
            yield return new WaitForSeconds(1f); //<- Wait for start to finish...
        
            for (var i = 0; i <= missionGenerator.MissionCycleCount; i++){
                var savableMissionData = missionGenerator.GenerateMissionData();
                Debug.Log($"Test({i}): Name: {savableMissionData}, MissionIndex: {savableMissionData.MissionDifficultyIndex} ,ClientIndex: {savableMissionData.MissionClientIndex}");
                Assert.AreEqual(typeof(SavableMissionData), savableMissionData.GetType());
                Assert.AreEqual(typeof(SavableDialogData), savableMissionData.SavableDialogData.GetType());
                Assert.AreEqual(typeof(List<SavableRequirementData>), savableMissionData.SavableRequirementData.GetType());
            }
            yield return null;
        }
        [UnityTest]
        public IEnumerator CycleTestGenerateDataMissionsFromSavableDataMissions(){
            var gameObject = GameObject.Instantiate(Resources.Load("MissionControllerTestPrefab") as GameObject);
            var missionGenerator = gameObject.GetComponent<MissionGenerator>();
            yield return new WaitForSeconds(1f); //<- Wait for start to finish...
        
            for (var i = 0; i <= missionGenerator.MissionCycleCount; i++){
                var savableMissionData = missionGenerator.GenerateMissionData();
                var missionData = missionGenerator.GenerateMission(savableMissionData);
                Assert.AreEqual(typeof(MissionData), missionData.GetType());
                // Assert.AreEqual(typeof(MissionDifficulty),missionData.Difficulty.GetType());
                // Assert.AreEqual(typeof(List<IMissionRequirement>), missionData.Requirements.GetType());
                // Assert.AreNotEqual(null, missionData.StylePointValues.GetType());
                // Assert.AreEqual(typeof(ClientTestData), missionData.ClientTestData.GetType());
                // Assert.AreEqual(typeof(SavableDialogData), missionData.SavableDialogData.GetType());
                Debug.Log($"Test: {i} Difficulty: {missionData.Difficulty.name} Data: {savableMissionData.SavableRequirementData.Count} Spawned{missionData.Requirements.Count}");
                foreach (var requirement in missionData.Requirements){
                    Debug.Log(requirement.ToString());
                }
                missionGenerator.CycleIndex();//TODO: Shouldn't be public
            }
            yield return null;
        }
        
        // [UnityTest]
        // public IEnumerator MissionPickerCycleTest(){
        //     var gameObject = GameObject.Instantiate(Resources.Load("MissionControllerTestPrefab") as GameObject);
        //     var missionGenerator = gameObject.GetComponent<MissionGenerator>();
        //     yield return new WaitForSeconds(0.5f); //<- Wait for start to finish...
        //
        //     for (var i = 0; i <= 10; i++){
        //         var missionDifficulty = missionGenerator.GenerateMissionData().Difficulty;
        //         Debug.Log($"Test({i}): Name: {missionDifficulty.name}");
        //         Assert.AreEqual(typeof(MissionDifficulty), missionDifficulty.GetType());
        //     }
        //
        //     yield return null;
        // }
        //
        // [UnityTest]
        // public IEnumerator GenerateMissionRequirementsCycleTest(){
        //     var gameObject = GameObject.Instantiate(Resources.Load("MissionControllerTestPrefab") as GameObject);
        //     var missionGenerator = gameObject.GetComponent<MissionGenerator>();
        //     yield return new WaitForSeconds(0.5f); //<- Wait for start to finish...
        //     for (var i = 0; i <= 5; i++){
        //         var missionInstance = missionGenerator.GenerateMissionData();
        //         Debug.Log(missionInstance.Difficulty.name);
        //         Assert.AreEqual(typeof(MissionData), missionInstance.GetType());
        //         Assert.AreEqual(typeof(List<IMissionRequirement>), missionInstance.Requirements.GetType());
        //     }
        //     yield return null;
        // }
        //
        // [UnityTest]
        // public IEnumerator GenerateMissionStylePointTest(){
        //     var gameObject = GameObject.Instantiate(Resources.Load("MissionControllerTestPrefab") as GameObject);
        //     var missionGenerator = gameObject.GetComponent<MissionGenerator>();
        //     yield return new WaitForSeconds(0.5f); //<- Wait for start to finish...
        //     for (var i = 0; i <= 5; i++){
        //         var missionInstance = missionGenerator.GenerateMissionData();
        //         Debug.Log(
        //             $"min: {missionInstance.StylePointValues.MinStylePoints} max: {missionInstance.StylePointValues.MaxStylePoints}");
        //         Assert.AreEqual(typeof(MissionData), missionInstance.GetType());
        //         Assert.AreEqual(typeof(StylePointValues), missionInstance.StylePointValues.GetType());
        //     }
        //     yield return null;
        // }
        // [UnityTest]
        // public IEnumerator GenerateMissionClientTest(){
        //     var gameObject = GameObject.Instantiate(Resources.Load("MissionControllerTestPrefab") as GameObject);
        //     var missionGenerator = gameObject.GetComponent<MissionGenerator>();
        //     yield return new WaitForSeconds(0.5f); //<- Wait for start to finish...
        //     for (var i = 0; i <= 5; i++){
        //         var missionInstance = missionGenerator.GenerateMissionData();
        //         // Debug.Log(missionInstance.DialogData.name);
        //         Assert.AreEqual(typeof(SavableDialogData), missionInstance.SavableDialogData.GetType());
        //     }
        //     yield return null;
        // }
    }
}
