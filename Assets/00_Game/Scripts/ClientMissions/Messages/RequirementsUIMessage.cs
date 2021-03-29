namespace ClientMissions.Messages{
    public class RequirementUIMessage{
        public readonly string RequirementName;
        public readonly bool IsCompleted;

        public RequirementUIMessage(string requirementName, bool isCompleted){
            RequirementName = requirementName;
            IsCompleted = isCompleted;
        }
    }
}