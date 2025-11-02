
namespace PocketbaseNetSdk.Services
{
    public interface ITokenService
    {
        public string? Token { get; set; }
        public bool IsValid();
        public void ClearToken();
    }
}
