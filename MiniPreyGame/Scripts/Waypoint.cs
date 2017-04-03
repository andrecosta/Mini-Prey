using InputManager;
using KokoEngine;

namespace MiniPreyGame
{
    class Waypoint : Behaviour, IGraphNode
    {
        public float totalNodeCost { get; set; }
        public float realNodeCost { get; set; }

        protected override void Awake()
        {
        }

        protected override void Update(float dt)
        {
        }
    }
}
