using System;
using KokoEngine;

namespace MiniPreyGame
{
    class WaypointEuclideanHeuristicCAlculator : IHeuristicCalculator
    {
        public float Calculate(IGraphNode node, IGraphNode target)
        {
            Waypoint t1 = node as Waypoint;
            Waypoint t2 = target as Waypoint;

            Vector3 diff = t1.Transform.Position - t2.Transform.Position;

            return Math.Abs(diff.X) + Math.Abs(diff.Y) + Math.Abs(diff.Z);
        }
    }
}
