using ClientMissions.Messages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace ClientMissions.Hud{
    public class ClientSelection : MonoBehaviour
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
            var missionData = selectMissionMessage.MissionData;
            clientMenuUIHolder.SetActive(false);
            narrativeMissionHolder.SetActive(true);
            clientNarrativePortrait.sprite = missionData.ClientData.Portrait;
            var clubInfo =  missionData.ClientData.ClientDialogData[missionData.SavableDialogData.ClubIndex];
            storyText.text = clubInfo.Dialog[missionData.SavableDialogData.DialogIndex];
            requirementText.text = "";
            foreach (var requirement in missionData.Requirements){
                requirementText.text += $"\n {requirement}";
            }
            clientRequirementHeaderText.text = $"{missionData.ClientData.name}s requests: ";
        }
    }
}
