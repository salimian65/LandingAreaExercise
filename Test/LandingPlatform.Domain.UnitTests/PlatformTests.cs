using System;
using Xunit;
using FluentAssertions;
using LandingPlatform.Domain.Platforms;
using LandingPlatform.Domain.Platforms.Exceptions;

namespace LandingPlatform.Domain.UnitTests
{
    public class PlatformTests
    {
        private const string OkForLanding = "ok for landing";
        private const string OutOfPlatform = "out of platform";
        private const string Clash = "clash";

        private readonly PlatformBuilder platformBuilder = new();

        [Fact]
        public void Returns_oKForLanding_when_asks_for_position_5_5()
        {
            //Arrange
            var coordinate = new Position(5, 5);
            var sut = platformBuilder
                .WithTopLeftX(5)
                .WithTopLeftY(5)
                .WithBottomRightX(10)
                .WithBottomRightY(10)
                .Build();
            //Act
            var result = sut.GetLandingStatus(coordinate);
            //Assert
            result.Should().Be(OkForLanding);
        }

        [Fact]
        public void Returns_outOfPlatform_when_asks_for_position_16_15()
        {
            //Arrange
            var coordinate = new Position(16, 15);
            var sut = platformBuilder
                .WithTopLeftX(5)
                .WithTopLeftY(5)
                .WithBottomRightX(10)
                .WithBottomRightY(10)
                .Build();
            //Act
            var result = sut.GetLandingStatus(coordinate);
            //Assert
            result.Should().Be(OutOfPlatform);
        }

        [Fact]
        public void Return_Clash_when_ask_position_has_previously_been_checked_by_another_rocket()
        {
            //Arrange
            var coordinate = new Position(5, 6);
            var sut = platformBuilder
                .WithTopLeftX(5)
                .WithTopLeftY(5)
                .WithBottomRightX(10)
                .WithBottomRightY(10)
                .Build();
            //Act
            sut.GetLandingStatus(coordinate);
            var result = sut.GetLandingStatus(coordinate);
            //Assert
            result.Should().Be(Clash);
        }

        [Theory]
        [InlineData(6, 6)]
        [InlineData(6, 7)]
        [InlineData(6, 8)]
        [InlineData(7, 6)]
        [InlineData(7, 8)]
        [InlineData(8, 6)]
        [InlineData(8, 7)]
        [InlineData(8, 8)]
        public void Return_Clash_when_position_is_located_next_to_a_position_that_has_previously_been_checked_by_another_rocket(int x, int y)
        {
            //Arrange
            var previousCoordinate = new Position(7, 7);
            var sut = platformBuilder
                .WithTopLeftX(5)
                .WithTopLeftY(5)
                .WithBottomRightX(10)
                .WithBottomRightY(10)
                .Build();
            var newCoordinate = new Position(x, y);
            //Act
            sut.GetLandingStatus(previousCoordinate);
            var result = sut.GetLandingStatus(newCoordinate);
            //Assert
            result.Should().Be(Clash);
        }

        [Fact]
        public void Throw_argument_null_exception_when_position_is_null()
        {
            //Arrange
            var sut = platformBuilder.Build();
            //Act
            Action landingStatus = () => sut.GetLandingStatus(null);
            //Assert
            landingStatus.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Create_Platform()
        {
            //Arrange
            //Act
            var sut = platformBuilder.Build();
            //Assert
            sut.Should().NotBeNull();
        }

        [Fact]
        public void Throw_InvalidPlatformSizeException_when_top_left_corner_is_greater_bottom_right_corner()
        {
            //Arrange
            //Act
            Action sut = () => platformBuilder
                .WithTopLeftX(8)
                .WithTopLeftY(9)
                .WithBottomRightX(4)
                .WithBottomRightY(6)
                .Build();
            //Assert
            sut.Should().Throw<InvalidPlatformSizeException>();
        }

        [Fact]
        public void Throw_InvalidPlatformSizeException_when_top_left_corner_is_equal_bottom_right_corner()
        {
            //Arrange
            //Act
            Action sut = () => platformBuilder
                .WithTopLeftX(5)
                .WithTopLeftY(5)
                .WithBottomRightX(5)
                .WithBottomRightY(5)
                .Build();
            //Assert
            sut.Should().Throw<InvalidPlatformSizeException>();
        }
    }
}
