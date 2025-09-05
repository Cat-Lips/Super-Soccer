using System.Linq;

namespace Game;

public static class PlayerExtensions
{
    public static Player GetClosestTeamPlayerInView(this Player self)
    {
        return self.ActionZone.GetOverlappingBodies()
            .OfType<Player>()
            .Where(x => x != self)
            .OrderBy(x => x.Position.DistanceSquaredTo(self.Position))
            .FirstOrDefault();
    }
}
