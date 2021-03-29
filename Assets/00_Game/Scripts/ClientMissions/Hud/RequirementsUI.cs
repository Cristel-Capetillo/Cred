using System.Collections.Generic;
using ClientMissions.Controllers;
using ClientMissions.Messages;
using TMPro;
using UnityEngine;
using Utilities;

namespace ClientMissions.Hud{
    public class RequirementsUI : MonoBehaviour{
        [SerializeField] TMP_Text requirementHeader;
        [SerializeField] TMP_Text stylePoints;
        [SerializeField] TMP_Text requirementTMPTextPrefab;
        [SerializeField] Transform requirementTextParent;
        Dictionary<string,TMP_Text> requirements = new Dictionary<string, TMP_Text>();
        string requiredStylepoints;
        
        void Start(){
            if(FindObjectOfType<ActiveClient>() == null) 
                return;
            if (FindObjectOfType<ActiveClient>().ActiveMissionData == null) 
                return;
            var missionData = FindObjectOfType<ActiveClient>().ActiveMissionData;
            EventBroker.Instance().SubscribeMessage<RequirementUIMessage>(UpdateRequirementUI);
            EventBroker.Instance().SubscribeMessage<CurrentStylePointsMessage>(UpdateStylePointsUI);
            requiredStylepoints = $"/{missionData.StylePointValues.MinStylePoints.ToString()}";
            requirementHeader.text = $"{missionData.ClientData.name}s requirements:";
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
            stylePoints.text = $"Style points: {currentStylePoints.CurrentStylePoints}{requiredStylepoints}";
        }
        void UpdateRequirementUI(RequirementUIMessage requirementUIMessage){
            if (requirements.ContainsKey(requirementUIMessage.RequirementName)){
                requirements[requirementUIMessage.RequirementName].fontStyle = requirementUIMessage.IsCompleted
                    ? FontStyles.Strikethrough
                    : FontStyles.Normal;
            }
        }
    }
}