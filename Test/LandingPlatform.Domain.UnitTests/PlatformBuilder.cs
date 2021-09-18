using LandingPlatform.Domain.Platforms;

namespace LandingPlatform.Domain.UnitTests
{
    public class PlatformBuilder
    {
        private int topLeftX;
        private int topLeftY;
        private int bottomRightX;
        private int bottomRightY;

        public PlatformBuilder()
        {
            ResetState();
        }
        private void ResetState()
        {
            topLeftX = 5;
            topLeftY = 5;
            bottomRightX = 10;
            bottomRightY = 10;
        }

        public Platform Build()
        {
            var platform = new Platform(new Position(topLeftX, topLeftY),
                                        new Position(bottomRightX, bottomRightY));
            ResetState();
            return platform;
        }

        public PlatformBuilder WithTopLeftX(int topLeftX)
        {
            this.topLeftX = topLeftX;
            return this;
        }

        public PlatformBuilder WithTopLeftY(int topLeftY)
        {
            this.topLeftY = topLeftY;
            return this;
        }

        public PlatformBuilder WithBottomRightX(int bottomRightX)
        {
            this.bottomRightX = bottomRightX;
            return this;
        }

        public PlatformBuilder WithBottomRightY(int bottomRightY)
        {
            this.bottomRightY = bottomRightY;
            return this;
        }
    }
}