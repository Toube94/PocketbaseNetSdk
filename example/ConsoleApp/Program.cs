using PocketbaseNetSdk;
using PocketbaseNetSdk.Models;
using PocketbaseNetSdk.Models.Base;
using PocketbaseNetSdk.Services;

namespace ConsoleApp
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var pocketBaseClient = new PocketbaseClient("http://127.0.0.1:8090");
            //Example usage of AuthService
            Console.WriteLine("Auth Example");
            IAuthService authService = new AuthService<ExampleAuthModel>(pocketBaseClient);

            var loginRequest = new PasswordLoginRequest
            {
                Identity = "test@test.com",
                Password = "test1234"
            };

            var emailAuthResult = await authService.AuthWithPassword(loginRequest);
            if (emailAuthResult.IsSuccess && emailAuthResult.Value is not null)
            {
                Console.WriteLine($"Authentication successful. Token: {emailAuthResult.Value.Token}");
            }
            else
            {
                Console.WriteLine($"Authentication failed. Error: {emailAuthResult.Error}");
                return;
            }

            //Example usage of Collections
            Console.WriteLine("Collection Example");
            ICollectionService collectionService = new CollectionService(pocketBaseClient);
            var creationResult = await collectionService.Collection<ExampleModel>("Test")
                .CreateAsync(
                new ExampleModel
            {
                Name = "Test Name"
            });
            if (creationResult.IsSuccess && creationResult.Value is not null)
            {
                Console.WriteLine($"Record created successfully. ID: {creationResult.Value.Id}, Name: {creationResult.Value.Name}");
            }
            else
            {
                Console.WriteLine($"Record creation failed. Error: {creationResult.Error}");
            }
        }
    }
}
