namespace ClientMissions.Messages{
    public class FollowersDifficultyMessage{
        public readonly int EasyModeEndValue;
        public readonly int HardModeStartValue;
        public FollowersDifficultyMessage(int easyModeEndValue, int hardModeStartValue){
            EasyModeEndValue = easyModeEndValue;
            HardModeStartValue = hardModeStartValue;
        }
    }
}