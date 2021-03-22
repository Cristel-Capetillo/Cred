using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clothing
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BodyPart")]

    public class BodyPart : ScriptableObject
    { 
        public Texture texture;
    }
}