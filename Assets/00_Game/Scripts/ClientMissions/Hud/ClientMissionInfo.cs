using UnityEngine;
using UnityEngine.UI;

namespace ClientMissions.Hud{
    public class ClientMissionInfo : MonoBehaviour{
        [SerializeField] Image clientPortrait;
        [SerializeField] Text nameText;

        //TODO:Add time left ui
        public void SetUp(string clientName, Sprite clientSprite){
            nameText.text = clientName;
            clientPortrait.sprite = clientSprite;
            
        }
    }
}