using System;
using KokoEngine;

namespace MiniPreyGame
{
    class Waypoint : Behaviour, IGraphNode
    {
        public float totalNodeCost { get; set; }
        public float realNodeCost { get; set; }
        static Random r = new Random();

        protected override void Awake()
        {
            // Place the waypoint on a random location on the screen
            Transform.Position = new Vector3(r.Next(0, Screen.Width), r.Next(0, Screen.Height));
        }

        protected override void Update(float dt)
        {
        }
    }
}
