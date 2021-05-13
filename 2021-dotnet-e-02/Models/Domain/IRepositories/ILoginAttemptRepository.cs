namespace _2021_dotnet_e_02.Models
{
    public interface ILoginAttemptRepository
    {
        LoginAttempt GetLatestLoginAttemptBy(string username);
        
    }
}