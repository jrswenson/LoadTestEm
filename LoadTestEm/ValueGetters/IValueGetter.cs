namespace LoadTestEm.ValueGetters
{
    public interface IValueGetter
    {
        object Value { get; set; }
    }

    public interface IValueGetter<T> : IValueGetter
    {
        new T Value { get; set; }
    }
}
