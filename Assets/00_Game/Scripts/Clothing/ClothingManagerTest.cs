using System;
using UnityEngine;

namespace Cred.Scripts.Clothing {
    public class ClothingManagerTest : MonoBehaviour {

        //TODO: Use the event broker to update the body parts
        //TODO: Instead of colour, change the mesh or sprite or textures
        //TODO: Add a reset button to go back to default

        [SerializeField] Transform torso;
        [SerializeField] Transform leftLeg;
        [SerializeField] Transform rightLeg;

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
