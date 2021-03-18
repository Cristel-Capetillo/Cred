using UnityEngine;
using UnityEngine.EventSystems;

namespace Club{
    public class MissionButtonScript : MonoBehaviour, IPointerClickHandler{

        MissionData missionData;
        
        public void Setup(MissionData missionData){
            this.missionData = missionData;
        }
        public void OnPointerClick(PointerEventData eventData){
            throw new System.NotImplementedException();
        }
    }
}