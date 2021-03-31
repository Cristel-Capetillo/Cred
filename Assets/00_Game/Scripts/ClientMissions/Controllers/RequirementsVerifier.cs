using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ClientMissions.Data;
using ClientMissions.Helpers;
using ClientMissions.Hud;
using ClientMissions.Messages;
using ClientMissions.Requirements;
using Clothing;
using Clothing.DressUp;
using Clothing.Inventory;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace ClientMissions.Controllers {
    public class RequirementsVerifier : MonoBehaviour {
        [SerializeField] UnityEvent<bool> onMeetAllRequirements = new UnityEvent<bool>();
        [SerializeField] List<ClothingType> legsClothingTypes = new List<ClothingType>();
        [SerializeField] GameObject parentGameObject;
        MissionData activeMissionData;
        Dictionary<ClothingType, CombinedWearables> wearablesOnClient = new Dictionary<ClothingType, CombinedWearables>();
        List<IMissionRequirement> requirements = new List<IMissionRequirement>();
        int currentStylePoints;
        public GameObject testBgMusic;

        void Start() {
            EventBroker.Instance().SubscribeMessage<SendActiveMissionMessage>(OnGetMissionData);
            EventBroker.Instance().SendMessage(new SceneChangeMessage());
        }
        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(OnClothingChanged);
            EventBroker.Instance().UnsubscribeMessage<RemoveAllClothes>(OnReset);
            EventBroker.Instance().UnsubscribeMessage<SendActiveMissionMessage>(OnGetMissionData);
            if (activeMissionData == null || wearablesOnClient.Count == 0)
                return;
            EventBroker.Instance().SendMessage(new CurrentMissionClothesMessage(wearablesOnClient));
        }

        void OnGetMissionData(SendActiveMissionMessage missionMessage) {
            if (missionMessage.MissionData.ClientData == null) {
                Debug.Log("No missionData");
                EventBroker.Instance().UnsubscribeMessage<SendActiveMissionMessage>(OnGetMissionData);
                return;
            }

            wearablesOnClient = missionMessage.CurrentWearables;
            EventBroker.Instance().SubscribeMessage<EventClothesChanged>(OnClothingChanged);
            EventBroker.Instance().SubscribeMessage<RemoveAllClothes>(OnReset);
            EventBroker.Instance().UnsubscribeMessage<SendActiveMissionMessage>(OnGetMissionData);

            activeMissionData = missionMessage.MissionData;
            requirements = activeMissionData.Requirements.ToList();
            parentGameObject.SetActive(true);
            Invoke(nameof(CheckRequirements), 0.1f);
        }

        public void OnClickEnterClub(GameObject gameObject) {
            var difficulty = activeMissionData.Difficulty;
            var currencyReward = CalculationsHelper.CalculateReward(activeMissionData.StylePointValues,
                currentStylePoints, difficulty.MaxCurrencyReward, difficulty.MINCurrencyReward);
            var followersReward = CalculationsHelper.CalculateReward(activeMissionData.StylePointValues,
                currentStylePoints, difficulty.MAXFollowersReward, difficulty.MINFollowersReward);
            gameObject.SetActive(true);
            testBgMusic.SetActive(false);
            FindObjectOfType<ClubSuccess>().ShowReward(currencyReward, followersReward);
            foreach (var client in wearablesOnClient) {
                EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(client.Value, -1));
            }
        }

        void OnReset(RemoveAllClothes obj) {
            onMeetAllRequirements?.Invoke(false);
            currentStylePoints = 0;
            foreach (var requirement in requirements) {
                EventBroker.Instance().SendMessage(new RequirementUIMessage(requirement.ToString(), false));
            }

            EventBroker.Instance().SendMessage(new CurrentStylePointsMessage(0));
            wearablesOnClient.Clear();
        }

        void OnClothingChanged(EventClothesChanged eventClothesChanged) {
            var combinedWearable = eventClothesChanged.CombinedWearables;
            if (IsNewWearable(combinedWearable)) {
                LegsClothingType(combinedWearable.clothingType);
                AddOrReplaceClothingType(combinedWearable);
            }

            if (requirements.Count == 0)
                return;
            CheckRequirements();
        }

        void CheckRequirements() {
            var checkStylePoints = CheckStylePoints();
            if (CheckAllRequirements() && checkStylePoints) {
                onMeetAllRequirements?.Invoke(true);
                return;
            }

            onMeetAllRequirements?.Invoke(false);
        }

        bool CheckStylePoints() {
            currentStylePoints = wearablesOnClient.Values.Sum(wearables => wearables.stylePoints);
            EventBroker.Instance().SendMessage(new CurrentStylePointsMessage(currentStylePoints));
            return currentStylePoints >= activeMissionData.StylePointValues.MinStylePoints;
        }

        bool IsNewWearable(CombinedWearables combinedWearables) {
            if (!wearablesOnClient.ContainsKey(combinedWearables.clothingType)) return true;
            if (PlayerInventory.GetName(wearablesOnClient[combinedWearables.clothingType]) !=
                PlayerInventory.GetName(combinedWearables)) return true;

            wearablesOnClient.Remove(combinedWearables.clothingType);
            print("Remove old clothing");
            return false;
        }

        void LegsClothingType(ClothingType clothingType) {
            if (!legsClothingTypes.Contains(clothingType)) return;
            foreach (var legsClothingType in legsClothingTypes) {
                if (wearablesOnClient.ContainsKey(legsClothingType)) {
                    wearablesOnClient.Remove(legsClothingType);
                }
            }
        }

        void AddOrReplaceClothingType(CombinedWearables combinedWearables) {
            if (wearablesOnClient.ContainsKey(combinedWearables.clothingType)) {
                wearablesOnClient[combinedWearables.clothingType] = combinedWearables;
                return;
            }

            wearablesOnClient.Add(combinedWearables.clothingType, combinedWearables);
        }

        bool CheckAllRequirements() {
            var completedRequirements = requirements.Count(Passed);
            return completedRequirements >= requirements.Count;
        }

        bool Passed(IMissionRequirement requirement) {
            if (wearablesOnClient.Values.Any(requirement.PassedRequirement)) {
                EventBroker.Instance().SendMessage(new RequirementUIMessage(requirement.ToString(), true));
                return true;
            }

            EventBroker.Instance().SendMessage(new RequirementUIMessage(requirement.ToString(), false));
            return false;
        }
    }
}