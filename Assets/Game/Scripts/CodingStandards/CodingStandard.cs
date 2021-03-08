using UnityEngine;

namespace Cred.Scripts.CodingStandards {
    public class CodingStandard : MonoBehaviour {
        //Back end fields should be written with underscore: 
        int _back;
        int Back { get; set; }
    }
}