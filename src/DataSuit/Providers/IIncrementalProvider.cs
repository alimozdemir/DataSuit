namespace DataSuit.Providers
{
    public interface IIncrementalProvider : IDataProvider<int>
    {
        string Prop { get; }
    }
}