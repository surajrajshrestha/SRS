namespace RulesManager.Models
{
    public class Rule
    {
        public Rule(string memberName, string @operator, string targetValue = "", string errorMessage = "")
        {
            MemberName = memberName;
            Operator = @operator;
            TargetValue = targetValue;
            ErrorMessage = errorMessage;
        }
        public string MemberName { get; set; }
        public string Operator { get; set; }
        public string TargetValue { get; set; }
        public string ErrorMessage { get; set; }
    }
}
