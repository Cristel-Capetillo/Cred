using System.Collections.Generic;
using System.Linq;
using ClientMissions.Data;
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
        MissionData activeMission;
        Dictionary<ClothingType,CombinedWearables> wearablesOnClient = new Dictionary<ClothingType, CombinedWearables>();
        List<IMissionRequirement> requirements = new List<IMissionRequirement>();
        void Start() {
            if (FindObjectOfType<ActiveMission>().ActiveMissionData == null) return;
            activeMission = FindObjectOfType<ActiveMission>().ActiveMissionData;
            requirements = activeMission.Requirements;
            EventBroker.Instance().SubscribeMessage<EventClothesChanged>(OnClothingChanged);
            EventBroker.Instance().SubscribeMessage<RemoveAllClothes>(OnReset);
        }

        void OnReset(RemoveAllClothes obj){
            onMeetAllRequirements?.Invoke(false);
            foreach (var requirement in requirements){
                EventBroker.Instance().SendMessage(new RequirementUIMessage(requirement.ToString(), false));
            }
            EventBroker.Instance().SendMessage(new CurrentStylePointsMessage(0));
            wearablesOnClient.Clear();
        }
        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(OnClothingChanged);
            EventBroker.Instance().UnsubscribeMessage<RemoveAllClothes>(OnReset);
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
            var currentStylePoints = wearablesOnClient.Values.Sum(wearables => wearables.stylePoints);
            EventBroker.Instance().SendMessage(new CurrentStylePointsMessage(currentStylePoints));
            return currentStylePoints >= activeMission.StylePointValues.MinStylePoints;
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