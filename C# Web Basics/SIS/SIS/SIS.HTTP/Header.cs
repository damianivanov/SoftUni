namespace SIS.HTTP
{
    public class Header
    {
        public Header(string Name,string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        public string Name { get; set; }
        public string Value { get; set; }
        public override string ToString()
        {
            return string.Format($"{Name}: {Value}");
        }
    }
}
