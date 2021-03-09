using System;
using UnityEngine;

namespace Cred.Scripts.Clothing {
    public class ClothingManagerTest : MonoBehaviour {

        //TODO: Use the event broker to update the body parts
        //TODO: Instead of colour, change the mesh or sprite or textures
        
        [SerializeField] Transform torso;
        [SerializeField] Transform leftLeg;
        [SerializeField] Transform rightLeg;

        public void SelectPants() {
            Debug.Log("Pants selected!");
        }

        public void SelectShirts() {
            Debug.Log("Shirts selected!");
            Debug.Log(torso);
            var torsoMesh = torso.GetComponent<MeshRenderer>();
            torsoMesh.material.color = Color.green;
        }

        void Start() {
            throw new NotImplementedException();
        }

    }
}
