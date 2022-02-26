namespace BlazeIt.Server.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetJwtSecretKey(this IConfiguration config)
        {
            return config["SECRET_KEY"];
        }

    }
}
