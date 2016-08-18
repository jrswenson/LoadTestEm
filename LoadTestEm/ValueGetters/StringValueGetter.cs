namespace LoadTestEm.ValueGetters
{
    public class StringValueGetter : IValueGetter<string>
    {
        private string _value;

        public StringValueGetter(string value)
        {
            _value = value;
        }

        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }

        object IValueGetter.Value
        {
            get
            {
                return this.Value;
            }

            set
            {
                this.Value = value.ToString();
            }
        }
    }
}
