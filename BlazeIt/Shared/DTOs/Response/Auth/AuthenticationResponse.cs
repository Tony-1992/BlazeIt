namespace BlazeIt.Shared.DTOs.Response
{
    public class AuthenticationResponse
    {
        public bool Successful { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
    }
}
