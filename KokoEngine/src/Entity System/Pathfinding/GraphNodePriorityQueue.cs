using System.Collections.Generic;

namespace KokoEngine
{
    class GraphNodePriorityQueue
    {
        List<IGraphNode> queue;

        // A bool to indicate that sorting is needed
        bool needsSorting = false;

        public GraphNodePriorityQueue()
        {
            queue = new List<IGraphNode>();
        }

        // Getter with the number of elements in the queue
        public int count
        {
            get
            {
                return queue.Count;
            }
        }

        // This method will remove and return the GraphNode with the lowest cost
        public IGraphNode PopFirst()
        {
            if (needsSorting)
            {
                Sort();
            }
            IGraphNode first = queue[0];
            queue.RemoveAt(0);
            return first;
        }

        // Adds a GraphNode and marks the queue that it need to Sort
        public void Add(IGraphNode o)
        {
            queue.Add(o);
            needsSorting = true;
        }

        // Removes an element and marks to Sort
        public void Remove(IGraphNode o)
        {
            queue.Remove(o);
            needsSorting = true;
        }

        // Clears the queue
        public void Clear()
        {
            queue.Clear();
        }

        // Public method that marks the queue to sort
        public void MarkToSort()
        {
            needsSorting = true;
        }

        // Sorts the queue
        public void Sort()
        {
            queue.Sort((a, b) => a.totalNodeCost.CompareTo(b.totalNodeCost));
            needsSorting = false;
        }
    }
}
