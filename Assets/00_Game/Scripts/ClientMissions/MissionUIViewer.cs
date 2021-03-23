using System;
using ClientMissions.MissionMessages;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace ClientMissions{
    public class MissionUIViewer : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] Text storyText;
        [SerializeField] Text requirementText;
        [SerializeField] Image clientImage;
        [SerializeField] Canvas selectedMissionCanvas;
        [SerializeField] Canvas missionPickerCanvas;
        void Start(){
            EventBroker.Instance().SubscribeMessage<SelectMissionMessage>(UpdateMissionUI);
        }

        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<SelectMissionMessage>(UpdateMissionUI);
        }

        void UpdateMissionUI(SelectMissionMessage selectMissionMessage){
            var missionData = selectMissionMessage.missionData;
            selectedMissionCanvas.enabled = true;
            missionPickerCanvas.enabled = false;
            var clubInfo =  missionData.ClientTestData.ClientDialogData[missionData.SavableDialogData.ClubIndex];
            storyText.text = clubInfo.Dialog[missionData.SavableDialogData.DialogIndex];
            requirementText.text = "";
            foreach (var requirement in missionData.Requirements){
                requirementText.text += $"\n {requirement}";
            }
        }
    }
}
