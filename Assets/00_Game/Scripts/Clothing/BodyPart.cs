using UnityEngine;

namespace Clothing {
    [CreateAssetMenu(menuName = "ScriptableObjects/ClothingType")]
    public class BodyPart : ScriptableObject{
        [SerializeField] string singularName;
        
        public string SingularName => singularName;
    }
}