using System.Collections;
using System.Collections.Generic;
using Club;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor.UnitTests.PlayModeTests{
    public class MissionGeneratorTest{
        // A Test behaves as an ordinary method
        [Test]
        public void SimplePassCreateNewInstance(){
            var gameObject = new GameObject();
            var missionGenerator = gameObject.AddComponent<MissionGenerator>();
            Assert.AreEqual(typeof(MissionGenerator), missionGenerator.GetType());
        }

        [UnityTest]
        public IEnumerator SimplePassInstantiateInstance(){
            var gameObject = GameObject.Instantiate(Resources.Load("MissionControllerTestPrefab") as GameObject);
            var missionGenerator = gameObject.GetComponent<MissionGenerator>();
            Assert.AreEqual(typeof(MissionGenerator), missionGenerator.GetType());
            yield return null;
        }

        [UnityTest]
        public IEnumerator MissionPickerCycleTest(){
            var gameObject = GameObject.Instantiate(Resources.Load("MissionControllerTestPrefab") as GameObject);
            var missionGenerator = gameObject.GetComponent<MissionGenerator>();
            yield return new WaitForSeconds(0.5f); //<- Wait for start to finish...

            for (var i = 0; i <= 10; i++){
                var missionDifficulty = missionGenerator.CreateMissionData().Difficulty;
                Debug.Log($"Test({i}): Name: {missionDifficulty.name}");
                Assert.AreEqual(typeof(MissionDifficulty), missionDifficulty.GetType());
            }

            yield return null;
        }

        [UnityTest]
        public IEnumerator GenerateMissionRequirementsCycleTest(){
            var gameObject = GameObject.Instantiate(Resources.Load("MissionControllerTestPrefab") as GameObject);
            var missionGenerator = gameObject.GetComponent<MissionGenerator>();
            yield return new WaitForSeconds(0.5f); //<- Wait for start to finish...
            for (var i = 0; i <= 5; i++){
                var missionInstance = missionGenerator.CreateMissionData();
                Debug.Log(missionInstance.Difficulty.name);
                Assert.AreEqual(typeof(MissionData), missionInstance.GetType());
                Assert.AreEqual(typeof(List<IMissionRequirement>), missionInstance.Requirements.GetType());
            }

            yield return null;
        }

        [UnityTest]
        public IEnumerator GenerateMissionStylePointTest(){
            var gameObject = GameObject.Instantiate(Resources.Load("MissionControllerTestPrefab") as GameObject);
            var missionGenerator = gameObject.GetComponent<MissionGenerator>();
            yield return new WaitForSeconds(0.5f); //<- Wait for start to finish...
            for (var i = 0; i <= 5; i++){
                var missionInstance = missionGenerator.CreateMissionData();
                Debug.Log(
                    $"min: {missionInstance.StylePointValues.MinStylePoints} max: {missionInstance.StylePointValues.MaxStylePoints}");
                Assert.AreEqual(typeof(MissionData), missionInstance.GetType());
                Assert.AreEqual(typeof(StylePointValues), missionInstance.StylePointValues.GetType());
            }
            yield return null;
        }
    }
}
