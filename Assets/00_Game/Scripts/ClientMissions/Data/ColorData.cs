using UnityEngine;

namespace ClientMissions.Data {
    [CreateAssetMenu(menuName = "ScriptableObjects/ClientRequestData/ColorData")]
    
    //This is the clothing part/pattern
    public class ColorData : ScriptableObject {
        public Color color;
        public string GetHexColorID() {
            return ColorUtility.ToHtmlStringRGB(color);
        }
    }
}