using System;

namespace LandingPlatform.Domain.Platforms.Exceptions
{
    public class InvalidPlatformSizeException : Exception
    {
        private const string InvalidPlatformSizeMessage = "Invalid platform size.";
        public InvalidPlatformSizeException() : base(InvalidPlatformSizeMessage) { }
    }
}