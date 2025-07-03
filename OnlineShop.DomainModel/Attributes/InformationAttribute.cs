namespace OnlineShop.DomainModel.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class InformationAttribute : Attribute
    {
        public string Key { get; }
        public object Value { get;}

        public InformationAttribute(string key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}
