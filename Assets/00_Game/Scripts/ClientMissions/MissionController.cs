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
        MissionData activeMission;
        List<CombinedWearables> wearablesOnClient = new List<CombinedWearables>();
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
        void OnClothingChanged(EventClothesChanged eventClothesChanged){
            if (!CheckIfItemExistsInList(eventClothesChanged.CombinedWearables)) {
                AddOrReplaceCombinedWearable(eventClothesChanged.CombinedWearables);
            }
            var checkStylePoints = CheckStylePoints();
            if (CheckAllRequirements() && checkStylePoints) {
                Debug.Log("User can enter the club ");
                onMeetAllRequirements?.Invoke(true);
                return;
            }
            onMeetAllRequirements?.Invoke(false);
        }

        bool CheckStylePoints(){
            var currentStylePoints = wearablesOnClient.Sum(wearables => wearables.stylePoints);
            EventBroker.Instance().SendMessage(new CurrentStylePointsMessage(currentStylePoints));
            return currentStylePoints >= activeMission.StylePointValues.MinStylePoints;
        }

        bool CheckIfItemExistsInList(CombinedWearables combinedWearable) {
            var idOnNewItem = PlayerInventory.GetName(combinedWearable);
            return wearablesOnClient.Select(clothing => PlayerInventory.GetName(clothing)).Any(idOnClient => idOnClient == idOnNewItem);
        }
        void AddOrReplaceCombinedWearable(CombinedWearables combinedWearable) {
            for (var i = 0; i < wearablesOnClient.Count; i++) {
                if (wearablesOnClient[i].clothingType == combinedWearable.clothingType) {
                    wearablesOnClient[i] = combinedWearable;
                    return;
                }
            }
            wearablesOnClient.Add(combinedWearable);
        }

        bool CheckAllRequirements() {
            var completedRequirements = requirements.Count(Passed);
            return completedRequirements >= requirements.Count;
        }

        bool Passed(IMissionRequirement requirement) {
            
            if(wearablesOnClient.Any(requirement.PassedRequirement)){
                EventBroker.Instance().SendMessage(new RequirementUIMessage(requirement.ToString(), true));
                return true;
            }
            EventBroker.Instance().SendMessage(new RequirementUIMessage(requirement.ToString(), false));
            return false;
        }
    }
}