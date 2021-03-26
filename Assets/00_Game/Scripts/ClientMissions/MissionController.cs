using System.Collections.Generic;
using System.Linq;
using ClientMissions.Data;
using ClientMissions.Helpers;
using ClientMissions.MissionMessages;
using ClientMissions.MissionRequirements;
using Clothing;
using Clothing.DressUp;
using Clothing.Inventory;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace ClientMissions {
    public class MissionController : MonoBehaviour{
        
        [SerializeField]UnityEvent<bool> onMeetAllRequirements = new UnityEvent<bool>();
        [SerializeField] List<ClothingType> legsClothingTypes = new List<ClothingType>();
        [SerializeField] List<GameObject> clientGameObjects = new List<GameObject>();
        [SerializeField] GameObject parentGameObject;
        MissionData activeMissionData;
        Dictionary<ClothingType,CombinedWearables> wearablesOnClient = new Dictionary<ClothingType, CombinedWearables>();
        List<IMissionRequirement> requirements = new List<IMissionRequirement>();
        int currentStylePoints;
        
        void Start() {
            EventBroker.Instance().SubscribeMessage<EventClothesChanged>(OnClothingChanged);
            EventBroker.Instance().SubscribeMessage<RemoveAllClothes>(OnReset);
            if (FindObjectOfType<ActiveMission>()== null){
                if (FindObjectOfType<ActiveMission>().ActiveMissionData == null)
                    return;
                return;
            }
            var activeMission = FindObjectOfType<ActiveMission>();
            activeMissionData = activeMission.ActiveMissionData;
            foreach (var client in clientGameObjects){
                client.SetActive(client.name == activeMissionData.ClientTestData.name);
            }
            requirements = activeMissionData.Requirements;
            
            if (activeMission.IsNewMission){
                EventBroker.Instance().SendMessage(new RemoveAllClothes());
            }
            parentGameObject.SetActive(true);
            activeMission.OnStartMission();
        }
        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(OnClothingChanged);
            EventBroker.Instance().UnsubscribeMessage<RemoveAllClothes>(OnReset);
        }

        public void OnClickEnterClub(){
            var reword = Helper.CalculateReword(activeMissionData.StylePointValues, currentStylePoints, activeMissionData.Difficulty.MaxReward);
            EventBroker.Instance().SendMessage(reword);
        }
        void OnReset(RemoveAllClothes obj){
            onMeetAllRequirements?.Invoke(false);
            currentStylePoints = 0;
            foreach (var requirement in requirements){
                EventBroker.Instance().SendMessage(new RequirementUIMessage(requirement.ToString(), false));
            }
            EventBroker.Instance().SendMessage(new CurrentStylePointsMessage(0));
            wearablesOnClient.Clear();
        }
        
        //TODO: Remove debug logs when everything works as intended...
        void OnClothingChanged(EventClothesChanged eventClothesChanged){
            print("Before: " + wearablesOnClient.Count);
            var combinedWearable = eventClothesChanged.CombinedWearables;
            if (IsNewWearable(combinedWearable)){
                LegsClothingType(combinedWearable.clothingType);
                AddOrReplaceClothingType(combinedWearable);
            }
            print("After: " + wearablesOnClient.Count);
            var checkStylePoints = CheckStylePoints();
            if (CheckAllRequirements() && checkStylePoints) {
                onMeetAllRequirements?.Invoke(true);
                return;
            }
            onMeetAllRequirements?.Invoke(false);
        }

        bool CheckStylePoints(){
            currentStylePoints = wearablesOnClient.Values.Sum(wearables => wearables.stylePoints);
            EventBroker.Instance().SendMessage(new CurrentStylePointsMessage(currentStylePoints));
            return currentStylePoints >= activeMissionData.StylePointValues.MinStylePoints;
        }

        bool IsNewWearable(CombinedWearables combinedWearables){
            if (!wearablesOnClient.ContainsKey(combinedWearables.clothingType)) return true;
            if (PlayerInventory.GetName(wearablesOnClient[combinedWearables.clothingType]) !=
                PlayerInventory.GetName(combinedWearables)) return true;
            
            wearablesOnClient.Remove(combinedWearables.clothingType);
            print("Remove old clothing");
            return false;
        }
       void LegsClothingType(ClothingType clothingType){
           if (!legsClothingTypes.Contains(clothingType)) return;
           foreach (var legsClothingType in legsClothingTypes){
                if (wearablesOnClient.ContainsKey(legsClothingType)){
                    wearablesOnClient.Remove(legsClothingType);
                    print("Remove clothes on legs:" + legsClothingType.name);
                }
            }
       }
        void AddOrReplaceClothingType(CombinedWearables combinedWearables){
            if (wearablesOnClient.ContainsKey(combinedWearables.clothingType)){
                wearablesOnClient[combinedWearables.clothingType] = combinedWearables;
                print("Replace clothes");
                return;
            }
            wearablesOnClient.Add(combinedWearables.clothingType,combinedWearables);
            print("Add clothes");
        }

        bool CheckAllRequirements() {
            var completedRequirements = requirements.Count(Passed);
            return completedRequirements >= requirements.Count;
        }

        bool Passed(IMissionRequirement requirement) {
            
            if(wearablesOnClient.Values.Any(requirement.PassedRequirement)){
                EventBroker.Instance().SendMessage(new RequirementUIMessage(requirement.ToString(), true));
                return true;
            }
            EventBroker.Instance().SendMessage(new RequirementUIMessage(requirement.ToString(), false));
            return false;
        }
    }
}