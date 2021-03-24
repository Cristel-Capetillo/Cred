using System.Collections.Generic;
using System.Linq;
using ClientMissions.Data;
using ClientMissions.MissionMessages;
using ClientMissions.MissionRequirements;
using Clothing;
using UnityEngine;
using Utilities;

namespace ClientMissions {
    public class MissionController : MonoBehaviour{

        MissionData activeMission;
        [SerializeField] CombinedWearables combinedWearables;
        List<CombinedWearables> wearablesOnClient = new List<CombinedWearables>();
        List<IMissionRequirement> requirements = new List<IMissionRequirement>();
        void Start() {
            /*if (FindObjectOfType<ActiveMission>().ActiveMissionData == null) return;
            activeMission = FindObjectOfType<ActiveMission>().ActiveMissionData;
            requirements = activeMission.Requirements;*/ 
            // Todo change this back when implemented in scene.....
            
            EventBroker.Instance().SubscribeMessage<EventClothesChanged>(OnClothingChanged);
            EventBroker.Instance().SubscribeMessage<ActiveMissionMessage>(OnMissionChanged);
        }
        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(OnClothingChanged);
            EventBroker.Instance().UnsubscribeMessage<ActiveMissionMessage>(OnMissionChanged);
        }

        void OnMissionChanged(ActiveMissionMessage activeMissionMessage) {
            activeMission = activeMissionMessage.missionData;
            requirements = activeMission.Requirements;
            EventBroker.Instance().SendMessage(new EventClothesChanged(combinedWearables));
        }
        void OnClothingChanged(EventClothesChanged eventClothesChanged) {
            if (!CheckIfItemExistsInList(eventClothesChanged.CombinedWearables)) {
                AddOrReplaceCombinedWearable(eventClothesChanged.CombinedWearables);
            }

            if (CheckAllRequirements()) {
                Debug.Log("User can enter the club " + combinedWearables.wearable[0].colorData.name);
                // User can enter club, button.IsActive(true) else false;
            }
        }

        bool CheckIfItemExistsInList(CombinedWearables combinedWearable) {
            return wearablesOnClient.Contains(combinedWearable);
        }
        void AddOrReplaceCombinedWearable(CombinedWearables combinedWearable) {
            for (int i = 0; i < wearablesOnClient.Count; i++) {
                if (wearablesOnClient[i].clothingType == combinedWearable.clothingType) {
                    wearablesOnClient[i] = combinedWearable;
                    return;
                }
            }
            wearablesOnClient.Add(combinedWearable);
        }

        bool CheckAllRequirements() {
            var completedRequirements = requirements.Count(Passed);
            return completedRequirements == requirements.Count;
        }

        bool Passed(IMissionRequirement requirement) {
            return wearablesOnClient.Any(requirement.PassedRequirement);
        }
    }
}