using System;
using _00_Game.Scripts.Clothing;
using EventBrokerFolder;
using UnityEngine;

namespace Cred.Scripts.Clothing {
    public class ClothingManagerTest : MonoBehaviour {

        //TODO: Use the event broker to update the body parts
        //TODO: Instead of colour, change the mesh or sprite or textures
        //TODO: Add a reset button to go back to default

        [SerializeField] GameObject torso;
        [SerializeField] GameObject leftLeg;
        [SerializeField] GameObject rightLeg;

        [SerializeField] GameObject AlexTorso;
        [SerializeField] GameObject AlexPants;
        
        void Start() {
            EventBroker.Instance().SubscribeMessage<EventClothesChanged>(UpdateClothes);
        }

        void UpdateClothes(EventClothesChanged eventClothesChanged) {
            print("eventclothesChanged called");
            switch (eventClothesChanged.bodyPart) {
                case "Shirt":
                    AlexTorso.GetComponent<SkinnedMeshRenderer>().material.mainTexture = eventClothesChanged.textureChanged;
                    break;
                case "Pants":
                    AlexPants.GetComponent<SkinnedMeshRenderer>().material.mainTexture = eventClothesChanged.textureChanged;
                    break;
            }
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(UpdateClothes);
        }
        
        public void SelectPants() {
            Debug.Log("Pants selected!");
            var leftMesh = leftLeg.GetComponent<MeshRenderer>();
            leftMesh.material.color = Color.blue;
            var rightMesh = rightLeg.GetComponent<MeshRenderer>();
            rightMesh.material.color = Color.blue;
        }

        public void SelectShirt() {
            Debug.Log("Shirts selected!");
            var torsoMesh = torso.GetComponent<MeshRenderer>();
            torsoMesh.material.color = Color.green;
        }
    }
}
