namespace DataSuit.Providers
{
    public interface IIncrementalLongProvider : IDataProvider<long>
    {
         void SetData(long data);
    }
}