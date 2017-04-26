using System;
using KokoEngine;
using Random = KokoEngine.Random;

namespace MiniPreyGame
{
    class Waypoint : Behaviour, IGraphNode
    {
        public float totalNodeCost { get; set; }
        public float realNodeCost { get; set; }

        protected override void Awake()
        {
            // Place the waypoint on a random location on the screen
            Transform.Position = new Vector3(Random.Range(0, Screen.Width), Random.Range(0, Screen.Height));
        }

        protected override void Start()
        {
            // Play animation
            GetComponent<Animator>().Play("waypoint", 0.1f);
        }

        protected override void Update()
        {
        }
    }
}
