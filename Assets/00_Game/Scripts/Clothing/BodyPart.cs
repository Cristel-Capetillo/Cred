using UnityEngine;

namespace Clothing {
    [CreateAssetMenu(menuName = "ScriptableObjects/BodyPart")]
    public class BodyPart : ScriptableObject{
        [SerializeField] string singularName;
        
        public string SingularName => singularName;
    }
}