namespace DataSuit.Providers
{
    public interface IIncrementalIntProvider : IDataProvider<int>
    {
        string Prop { get; }
    }
}