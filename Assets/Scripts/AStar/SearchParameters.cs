using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace AStar
{
    /// <summary>
    /// Defines the parameters which will be used to find a path across a section of the map
    /// </summary>
    public class SearchParameters
    {
		public Vector3 StartLocation { get; set; }

		public Vector3 EndLocation { get; set; }
        
        public bool[,] Map { get; set; }

		public SearchParameters(Vector3 startLocation, Vector3 endLocation, bool[,] map)
        {
            this.StartLocation = startLocation;
            this.EndLocation = endLocation;
            this.Map = map;
        }
    }
}
