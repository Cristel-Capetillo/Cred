using System;
using ClientMissions.MissionMessages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace ClientMissions{
    public class MissionUIViewer : MonoBehaviour
    {
        [SerializeField] TMP_Text storyText;
        [SerializeField] TMP_Text clientRequirementHeaderText;
        [SerializeField] TMP_Text requirementText;
        
        [SerializeField] Image clientNarrativePortrait;
        [SerializeField] GameObject clientMenuUIHolder;
        [SerializeField] GameObject narrativeMissionHolder;
        void Start(){
            EventBroker.Instance().SubscribeMessage<SelectMissionMessage>(UpdateMissionUI);
        }

        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<SelectMissionMessage>(UpdateMissionUI);
        }

        void UpdateMissionUI(SelectMissionMessage selectMissionMessage){
            var missionData = selectMissionMessage.missionData;
            clientMenuUIHolder.SetActive(false);
            narrativeMissionHolder.SetActive(true);
            clientNarrativePortrait.sprite = missionData.ClientTestData.Portrait;
            var clubInfo =  missionData.ClientTestData.ClientDialogData[missionData.SavableDialogData.ClubIndex];
            storyText.text = clubInfo.Dialog[missionData.SavableDialogData.DialogIndex];
            requirementText.text = "";
            foreach (var requirement in missionData.Requirements){
                requirementText.text += $"\n {requirement}";
            }
            clientRequirementHeaderText.text = $"{missionData.ClientTestData.name}s requests: ";
        }
    }
}
