using System.Collections;
using ClientMissions.Data;
using ClientMissions.Messages;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;
using FMODUnity;

namespace ClientMissions.Controllers{
    public class ClientButton : MonoBehaviour, IPointerClickHandler{
        [SerializeField] Image clientPortrait;
        [SerializeField] Image timerImage;
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField, Range(1,60)] int updateFrequency = 10;
        const string fmodEventPath = "event:/Menu/";
        long maxTime;
        long currentTime;
        public MissionData MissionData{ get; private set; }
        void OnEnable() {
            StartCoroutine(UpdateTimerUI());
        }
        void OnDisable() {
            StopAllCoroutines();
        }
        public void Setup(MissionData missionData, int missionTimeInSec, long currentUnixTime){
            StopAllCoroutines();
            if (missionData == null){
                Debug.LogWarning("No missionData was sent...");
                return;
            }
            clientPortrait.sprite = missionData.ClientData.Portrait;
            nameText.text = missionData.ClientData.name;
            maxTime = missionTimeInSec;
            currentTime = currentUnixTime - missionData.SavableMissionData.UnixUtcTimeStamp;
            MissionData = missionData;
        }
        IEnumerator UpdateTimerUI() {
            timerImage.fillAmount = Mathf.InverseLerp(maxTime, 0, currentTime);
            while (currentTime <= maxTime) {
                yield return new WaitForSeconds(updateFrequency);
                currentTime += updateFrequency;
                timerImage.fillAmount = Mathf.InverseLerp(maxTime, 0, currentTime);
            }
        }
        public void OnPointerClick(PointerEventData eventData){
            EventBroker.Instance().SendMessage(new SelectMissionMessage(MissionData));
            RuntimeManager.PlayOneShot($"{fmodEventPath}{MissionData.ClientData.name}VO");
        }
    }
}