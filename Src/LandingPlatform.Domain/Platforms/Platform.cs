using System;
using LandingPlatform.Domain.Platforms.Exceptions;

namespace LandingPlatform.Domain.Platforms
{
    public class Platform
    {
        private const string OkForLanding = "ok for landing";

        private const string OutOfPlatform = "out of platform";

        private const string Clash = "clash";

        private readonly Position topLefCorner;

        private readonly Position bottomRightCorner;

        private static readonly object syncObject = new();

        private Position latestReservedPosition;

        public Platform(Position topLefCorner, Position bottomRightCorner)
        {
            if (topLefCorner >= bottomRightCorner) throw new InvalidPlatformSizeException();

            this.topLefCorner = topLefCorner;
            this.bottomRightCorner = bottomRightCorner;
        }

        public string GetLandingStatus(Position position)
        {
            MakeSurePositionIsNotNull(position);

            if (IsOutsidePlatform(position)) return OutOfPlatform;

            lock (syncObject)
            {
                if (HasPositionPreviouslyBeenCheckedByAnotherRocket(position)) return Clash;

                if (IsPositionLocatedNextToAPositionThatHasPreviouslyBeenCheckedByAnotherRocket(position)) return Clash;

                latestReservedPosition = position;
            }

            return OkForLanding;
        }

        private static void MakeSurePositionIsNotNull(Position position)
        {
            if (position == null) throw new ArgumentNullException(nameof(position));
        }

        private bool IsOutsidePlatform(Position position)
        {
            return position < topLefCorner || position > bottomRightCorner;
        }

        private bool HasPositionPreviouslyBeenCheckedByAnotherRocket(Position position)
        {
            return latestReservedPosition != null && latestReservedPosition.Equals(position);
        }

        private bool IsPositionLocatedNextToAPositionThatHasPreviouslyBeenCheckedByAnotherRocket(Position position)
        {
            return latestReservedPosition != null && latestReservedPosition.IsItOnPerimeter(position);
        }
    }
}