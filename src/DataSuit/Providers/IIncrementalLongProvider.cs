namespace DataSuit.Providers
{
    public interface IIncrementalLongProvider : IDataProvider<long>
    {
        string Prop { get; }
    }
}