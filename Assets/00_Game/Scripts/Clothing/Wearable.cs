using System.Collections.Generic;
using ClientMissions.Data;
using Club;
using UnityEngine;

namespace Clothing {
    [CreateAssetMenu(menuName = "ScriptableObjects/Wearable")]
    public class Wearable : ScriptableObject {
        [SerializeField] BodyPart bodyPart;
        [SerializeField] Texture texture; //TODO: Inventory icon
        [SerializeField] Sprite sprite;
        public ColorData colorData;

        public Texture addedUpcycleTexture;

        ////////////////////*Upcycle Wearables*///////////////////////////
        [SerializeField ]public bool isUpCycledWearable;
        
        [HideInInspector]public bool unlockedUpcycle;
        /////////////////////////////////////////////////////////////////

        public bool HasUnlockedUpCycledWearable()
        {
            return isUpCycledWearable && unlockedUpcycle;
        }

        public Texture Texture => texture;
        public Sprite Sprite => sprite;
        public BodyPart BodyPart => bodyPart;

    }
}