namespace EmployeesApi.Config
{
    public class JwtOptions
    {
        public string PrivateKey        { get; set; }
        public int    LifetimeInSeconds { get; set; }
    }
}
