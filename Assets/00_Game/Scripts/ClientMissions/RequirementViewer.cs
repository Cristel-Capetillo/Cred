using System.Collections.Generic;
using ClientMissions.MissionMessages;
using TMPro;
using UnityEngine;
using Utilities;

namespace ClientMissions{
    public class RequirementViewer : MonoBehaviour{
        [SerializeField] TMP_Text requirementHeader;
        [SerializeField] TMP_Text stylePoints;
        [SerializeField] TMP_Text requirementTMPTextPrefab;
        [SerializeField] Transform requirementTextParent;
        Dictionary<string,TMP_Text> requirements = new Dictionary<string, TMP_Text>();
        string requiredStylepoints;
        
        void Start(){
            if (FindObjectOfType<ActiveMission>().ActiveMissionData == null) return;
            var missionData = FindObjectOfType<ActiveMission>().ActiveMissionData;
            EventBroker.Instance().SubscribeMessage<RequirementUIMessage>(UpdateRequirementUI);
            EventBroker.Instance().SubscribeMessage<CurrentStylePointsMessage>(UpdateStylePointsUI);
            requiredStylepoints = $"/{missionData.StylePointValues.MinStylePoints.ToString()}";
            requirementHeader.text = $"{missionData.ClientTestData.name}s requirements:";
            stylePoints.text = $"Style points: 0{requiredStylepoints}";
            foreach (var requirement in missionData.Requirements){
                var temp = Instantiate(requirementTMPTextPrefab, requirementTextParent);
                temp.text = requirement.ToString();
                requirements.Add(requirement.ToString(),temp);
            }
        }
        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<RequirementUIMessage>(UpdateRequirementUI);
        }

        void UpdateStylePointsUI(CurrentStylePointsMessage currentStylePoints){
            stylePoints.text = $"Style points: {currentStylePoints.currentStylePoints}{requiredStylepoints}";
        }
        void UpdateRequirementUI(RequirementUIMessage requirementUIMessage){
            if (requirements.ContainsKey(requirementUIMessage.requirementName)){
                requirements[requirementUIMessage.requirementName].fontStyle = requirementUIMessage.isCompleted
                    ? FontStyles.Strikethrough
                    : FontStyles.Normal;
            }
        }
    }
    public class RequirementUIMessage{
        public readonly string requirementName;
        public readonly bool isCompleted;

        public RequirementUIMessage(string requirementName, bool isCompleted){
            this.requirementName = requirementName;
            this.isCompleted = isCompleted;
        }
    }
}