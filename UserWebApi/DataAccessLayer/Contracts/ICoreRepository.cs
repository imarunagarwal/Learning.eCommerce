namespace UserWebApi.DataAccessLayer.Contracts
{
    public interface ICoreRepository
    {
        string GenerateHashedPassword(string password);
    }
}