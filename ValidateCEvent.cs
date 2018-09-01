using Observer;
namespace FFP.Validations
{
    public class ValidateCEvent : ChannelEvent
    {
        public ValidateCEvent(IPublisher Source, string Name, IRuleDescription Value) :
            base(Source, StandardEventChannel.Validation, Name, Value)
        {
            Rule = Value;
        }

        public IRuleDescription Rule { get; set; }

        public bool CheckRule { get; set; } = true;
    }
}

