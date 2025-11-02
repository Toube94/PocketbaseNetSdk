using PocketbaseNetSdk.Models;

namespace PocketbaseNetSdk.Exceptions
{
    public class PocketbaseServerException : Exception
    {
        public PocketbaseServerError? Error { get; init; }

        public PocketbaseServerException(string message, PocketbaseServerError? error)
            : base(message)
        {
            Error = error;
        }
    }
}
