﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace AStar
{
    /// <summary>
    /// Represents a single node on a grid that is being searched for a path between two points
    /// </summary>
    public class Node
    {
        private Node parentNode;

        /// <summary>
        /// The node's location in the grid
        /// </summary>
        public Vector3 Location { get; private set; }

        /// <summary>
        /// True when the node may be traversed, otherwise false
        /// </summary>
        public bool IsWalkable { get; set; }
        
        /// <summary>
        /// Cost from start to here
        /// </summary>
        public float G { get; private set; }

        /// <summary>
        /// Estimated cost from here to end
        /// </summary>
        public float H { get; private set; }

        /// <summary>
        /// Flags whether the node is open, closed or untested by the PathFinder
        /// </summary>
        public NodeState State { get; set; }

        /// <summary>
        /// Estimated total cost (F = G + H)
        /// </summary>
        public float F
        {
            get { return this.G + this.H; }
        }

        /// <summary>
        /// Gets or sets the parent node. The start node's parent is always null.
        /// </summary>
        public Node ParentNode
        {
            get { return this.parentNode; }
            set
            {
                // When setting the parent, also calculate the traversal cost from the start node to here (the 'G' value)
                this.parentNode = value;
                this.G = this.parentNode.G + GetTraversalCost(this.Location, this.parentNode.Location);
            }
        }

        /// <summary>
        /// Creates a new instance of Node.
        /// </summary>
        /// <param name="x">The node's location along the X axis</param>
        /// <param name="y">The node's location along the Y axis</param>
        /// <param name="isWalkable">True if the node can be traversed, false if the node is a wall</param>
        /// <param name="endLocation">The location of the destination node</param>
		public Node(float x, float y, bool isWalkable, Vector3 endLocation)
        {
			this.Location = new Vector3(x, y);
            this.State = NodeState.Untested;
            this.IsWalkable = isWalkable;
            this.H = GetTraversalCost(this.Location, endLocation);
            this.G = 0;
        }

        public override string ToString()
        {
			return string.Format("{0}, {1}: {2}", this.Location.x, this.Location.y, this.State);
        }

        /// <summary>
        /// Gets the distance between two points
        /// </summary>
		internal static float GetTraversalCost(Vector3 location, Vector3 otherLocation)
        {
            float deltaX = otherLocation.x - location.x;
            float deltaY = otherLocation.y - location.y;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
