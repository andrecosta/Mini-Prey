using System;
using KokoEngine;
using System.Collections.Generic;
using System.Linq;

namespace MiniPreyGame
{
    class WaypointsController : Behaviour
    {
        public Graph graph { get; private set; }
        public List<IGraphNode> shortestPath { get; private set; } = new List<IGraphNode>();
        public IGameObject player { get; set; }

        private List<IGraphNode> _waypoints = new List<IGraphNode>();
        private IGraphNode _randomWaypoint;

        protected override void Awake()
        {
            graph = new Graph(false);
        }

        protected override void Start()
        {
            foreach (IGraphNode w in _waypoints)
            {
                graph.AddNode(w);
            }

            Random r = new Random();

            // Add edges
            foreach (IGraphNode n in graph.nodes)
            {
                int i = 0;

                List<IGraphNode> sortedWaypoints =
                    graph.nodes.OrderBy(x => ((x as Waypoint).Transform.Position - (n as Waypoint).Transform.Position).SqrMagnitude)
                        .ToList();

                foreach (IGraphNode w in sortedWaypoints)
                {
                    if (i++ > 3) continue;

                    var wt = w as Waypoint;

                    if (wt != n)
                    {
                        GraphEdge edge = new GraphEdge(n, wt, 1);
                        graph.AddEdge(edge);
                    }
                }
            }

            _randomWaypoint = graph.nodes[r.Next(0, graph.nodes.Count)];
            (_randomWaypoint as Waypoint).GetComponent<SpriteRenderer>().color = Color.Green;
        }

        public void AddWaypoint(IGraphNode w)
        {
            _waypoints.Add(w);
        }

        public void FindPath(IGraphNode source, IGraphNode target)
        {
            AStarGraphSearch graphSearch = new AStarGraphSearch();
            bool success = graphSearch.Search(graph, source, target, new WaypointEuclideanHeuristicCAlculator());

            if (success)
            {
                shortestPath.Clear();
                shortestPath.Add(source);
                shortestPath.AddRange(graphSearch.shortestPath);
                shortestPath.Add(target);
            }
        }

        protected override void Update(float dt)
        {
            foreach (IGraphNode n in graph.nodes)
            {
                var w = n as Waypoint;
                if ((player.Transform.Position - w.Transform.Position).SqrMagnitude < 40 && _randomWaypoint != w)
                {
                    FindPath(w, _randomWaypoint);
                }
            }
        }
    }
}
