# PocketbaseNetSdk
A community .Net SDK for [Pocketbase.IO](https://pocketbase.io/).
The project is still under development.

## Requirement
- .Net 9

## Installation
coming soon

## Usage
### Auth
```
            //Create PocketBaseClient which connnects to PocketBase
            var pocketBaseClient = new PocketbaseClient("http://127.0.0.1:8090");

            //Create AuthService.
            IAuthService authService = new AuthService<BaseAuthModel>(pocketBaseClient);

            //Create a email/password login request. Replace with your own email and password
            var loginRequest = new PasswordLoginRequest
            {
                Identity = "test@test.com",
                Password = "test1234"
            };

            //Check if authentication was successful
            if (emailAuthResult.IsSuccess && emailAuthResult.Value is not null)
            {
                Console.WriteLine($"Authentication successful. Token: {emailAuthResult.Value.Token}");
            }
            else
            {
                Console.WriteLine($"Authentication failed. Error: {emailAuthResult.Error}");
                return;
            }
```
### Collections
```
            //Create CollectionService to interact with collections
            ICollectionService collectionService = new CollectionService(pocketBaseClient);
            //Create a new record in the "Test" collection
            var newExampleModel = new ExampleModel
            {
                Name = "Test Name"
            };
            var creationResult = await collectionService
                .Collection<ExampleModel>("Test")
                .CreateAsync(newExampleModel);
            if (creationResult.IsSuccess && creationResult.Value is not null)
            {
                Console.WriteLine($"Record created successfully. ID: {creationResult.Value.Id}, Name: {creationResult.Value.Name}");
            }
            else
            {
                Console.WriteLine($"Record creation failed. Error: {creationResult.Error}");
            }

            //Update the created record in the "Test" collection
            var updateResult = await collectionService
                .Collection<ExampleModel>("Test")
                .UpdateAsync(new ExampleModel
                {
                    Id = creationResult.Value!.Id,
                    Name = "Updated Test Name"
                });

            //View a record from the "Test" collection
            var viewResult = await collectionService
                .Collection<ExampleModel>("Test")
                .ViewAsync(creationResult.Value!.Id);

            //Fetch records from the "Test" collection. Page 1 with 30 items per page by default
            var getResult = await collectionService
                .Collection<ExampleModel>("Test")
                .GetAsync();

            //Delete a record from the "Test" collection
            var deleteResult = await collectionService
                .Collection<ExampleModel>("Test")
                .DeleteAsync(creationResult.Value!.Id);
```
## Development
1. Clone Repository
<br>`https://github.com/Toube94/PocketbaseNetSdk`
2. Open the pocketbase-csharp-sdk.sln with Visual Studio 
