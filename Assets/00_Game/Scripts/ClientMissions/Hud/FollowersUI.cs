using ClientMissions.Messages;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace ClientMissions.Hud{
    public class FollowersUI : MonoBehaviour{
        [SerializeField]Slider followerSlider;
        [SerializeField]int easyModeEndValue;
        [SerializeField]int hardModeStartValue;
        [SerializeField] int maxFollowers = 5000;
        bool isActive;
        
        void Awake(){
            if(isActive)
                return;
            EventBroker.Instance().SubscribeMessage<FollowersDifficultyMessage>(OnChangeDifficultyValues);
            EventBroker.Instance().SubscribeMessage<UpdateUIFollowersMessage>(OnSliderUIValueChange);
            isActive = true;
        }
        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<UpdateUIFollowersMessage>(OnSliderUIValueChange);
            EventBroker.Instance().UnsubscribeMessage<FollowersDifficultyMessage>(OnChangeDifficultyValues);
        }
        void OnChangeDifficultyValues(FollowersDifficultyMessage followersDifficultyMessage){
            easyModeEndValue = followersDifficultyMessage.EasyModeEndValue;
            hardModeStartValue = followersDifficultyMessage.HardModeStartValue;
        }
        void OnSliderUIValueChange(UpdateUIFollowersMessage updateUIFollowersMessage){
            if (updateUIFollowersMessage.Followers <= easyModeEndValue){
                followerSlider.maxValue = easyModeEndValue;
                followerSlider.minValue = 0;
                followerSlider.value = updateUIFollowersMessage.Followers;
                return;
            }
            if (updateUIFollowersMessage.Followers >= hardModeStartValue){
                followerSlider.minValue = hardModeStartValue;
                followerSlider.maxValue = maxFollowers;
                followerSlider.value = updateUIFollowersMessage.Followers;
                return;
            }
            followerSlider.minValue = easyModeEndValue;
            followerSlider.maxValue = hardModeStartValue;
            followerSlider.value = updateUIFollowersMessage.Followers;
        }
    }
}
