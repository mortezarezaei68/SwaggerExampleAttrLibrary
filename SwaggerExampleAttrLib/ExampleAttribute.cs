namespace SwaggerExampleAttrLib
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = true)]
    public class ExampleAttribute: Attribute
    {
        public ExampleAttribute(object message)
        {
            Message = message;
        }
        public ExampleAttribute(params object[] message)
        {
            Message = message;
        }

        public object Message { get; }
    }
}