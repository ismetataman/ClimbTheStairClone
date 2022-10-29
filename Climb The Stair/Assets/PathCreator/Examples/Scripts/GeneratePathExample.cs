using UnityEngine;

namespace PathCreation.Examples {
    // Example of creating a path at runtime from a set of points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour {

        public bool closedLoop = true;
        public Transform[] waypoints;
        public GameObject parent;

        private void Awake() {
            
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                waypoints[i] = parent.transform.GetChild(i).transform.GetChild(0).transform;
            }
        }

        void Start () {
            if (waypoints.Length > 0) {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath (waypoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator> ().bezierPath = bezierPath;
            }
        }
    }
}