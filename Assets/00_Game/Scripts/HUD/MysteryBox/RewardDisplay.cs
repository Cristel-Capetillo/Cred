using System.Collections;
using Clothing.Inventory;
using HUD.Clothing;
using UnityEngine;
using Utilities;

public class RewardDisplay : MonoBehaviour {
    [SerializeField] float sizeToDisplayReward = 4f;

    void OnEnable() {
        EventBroker.Instance().SubscribeMessage<EventShowReward>(ShowReward);
    }

    void OnDisable() {
        EventBroker.Instance().UnsubscribeMessage<EventShowReward>(ShowReward);
    }

    void ShowReward(EventShowReward eventShowReward) {
        var reward = eventShowReward.Reward;
        var instance = Instantiate(reward.gameObject, this.transform);

        for (var i = 1; i < instance.transform.childCount; i++) {
            instance.transform.GetChild(i).gameObject.SetActive(false);
        }

        Resize(instance, sizeToDisplayReward);
        instance.GetComponent<AssignCombinedWearableToUpCycle>().enabled = false;
        StartCoroutine(DestroyRewardOnClick(instance));
    }

    IEnumerator DestroyRewardOnClick(GameObject go) {
        while (!Input.GetKeyDown(KeyCode.Mouse0)) {
            yield return null;
        }

        Destroy(go);
    }

    void Resize(GameObject go, float newScale) {
        var newSize = new Vector2(newScale, newScale);
        go.GetComponent<RectTransform>().localScale = newSize;
    }
}