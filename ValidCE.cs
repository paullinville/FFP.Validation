using Observer;
namespace FFP.Validations
{
    public class ValidCE : ChannelEvent
    {
        public ValidCE(IPublisher Source, string Name, IRule Value) :
            base(Source, StandardEventChannel.Validation, Name, Value)
        {
            Rule = Value;
        }

        public IRule Rule { get; set; }

        public bool CheckRule { get; set; } = true;
    }
}

