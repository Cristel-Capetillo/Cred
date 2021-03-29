using ClientMissions.Data;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace ClientMissions.Hud{
    public class FollowersUI : MonoBehaviour{
        [SerializeField]Slider followerSlider;
        bool isActive;
        
        void Start(){
            if(isActive)
                return;
            EventBroker.Instance().SubscribeMessage<UpdateUIFollowersMessage>(OnUpdateSliderUI);
            isActive = true;
        }

        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<UpdateUIFollowersMessage>(OnUpdateSliderUI);
        }

        void OnUpdateSliderUI(UpdateUIFollowersMessage obj){
            followerSlider.maxValue = obj.MaxFollowers;
            followerSlider.minValue = obj.MinFollowers;
            followerSlider.value = obj.Followers;
        }
    }
}
