using System.Collections.Generic;
using UnityEngine;

namespace Club {
    [CreateAssetMenu(menuName = "ScriptableObjects/ClientRequestData/MissionData")]
    public class MissionData : ScriptableObject {
        [SerializeField] ClubData clubData;
        [SerializeField] DialogData dialogData;
        [SerializeField] List<ColorData> requiredColors;
        [SerializeField] int minimumStylePoints;
        [SerializeField] int maximumStylePoints;
        [SerializeField] int targetReword;
        public ClubData ClubData => clubData;
        public DialogData DialogData => dialogData;
        public List<ColorData> RequiredColors => requiredColors;
        public int MinimumStylePoints => minimumStylePoints;
        public int MaximumStylePoints => maximumStylePoints;

        public int TargetReword => targetReword;
    }
}