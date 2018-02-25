namespace DataSuit.Infrastructures
{
    public interface ISessionManager
    {
        long IncreaseLong(string prop);
        int IncreaseInteger(string prop);
    }
}