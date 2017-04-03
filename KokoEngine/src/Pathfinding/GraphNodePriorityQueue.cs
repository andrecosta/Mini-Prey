using System.Collections.Generic;

namespace KokoEngine
{
    class GraphNodePriorityQueue
    {
        // The queue
        List<IGraphNode> queue;

        // a bool to indicate that sorting is needed
        bool needsSorting = false;

        public GraphNodePriorityQueue()
        {
            queue = new List<IGraphNode>(); //initialize the List
        }

        // getter with the number of elements in the queue
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
            { //Sort if needed
                Sort();
            }
            IGraphNode first = queue[0]; //get the first element
            queue.RemoveAt(0); //remove it from list
            return first; //return it
        }

        // Adds a GraphNode and marks the queue that it need to Sort
        public void Add(IGraphNode o)
        {
            queue.Add(o); //add
            needsSorting = true; //mark to sort
        }

        // removes an element and marks to Sort
        public void Remove(IGraphNode o)
        {
            queue.Remove(o); //remove it
            needsSorting = true; //mark to sort
        }

        // clears the queue
        public void Clear()
        {
            queue.Clear();
        }

        // public method that marks the queue to sort
        public void MarkToSort()
        {
            needsSorting = true;
        }

        // sorts the queue
        public void Sort()
        {
            queue.Sort((a, b) => a.totalNodeCost.CompareTo(b.totalNodeCost)); //sort the queue using the totalNodeCost
            needsSorting = false; //mark that it does NOT need to to sort
        }
    }
}
