using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;

    // A Serialized List and Array work the same way as far as the editor is concerned.
    // Serialized lists dont need to be initialized with 'new List<Transform>()' 
    [SerializeField]
    private List<Transform> waypoints;

    private int _nextIndex;

    private void Start()
    {       
        if (waypoints.Count < 2)
		{
            Debug.LogError(gameObject.name + " waypoint follower needs at least 2 waypoints!");
            return;
		}

        _nextIndex = 1;

        transform.position = waypoints[0].position; // Start this gameObject at the first waypoint position.
    }

    private void Update()
    {
        if (transform.position == waypoints[_nextIndex].position)
		{
            // We're now at the 'next' waypoint so let's increase the index to get hold of the next-next wp.
            _nextIndex++;

            if (_nextIndex >= waypoints.Count)  
			{
                // this means we've reached the last waypoint in the list and are trying to access one beyond that.
                // Setting the _nextIndex to 0 means the next waypoint will be the first one.
                _nextIndex = 0;
			}
		}

        float speed = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, waypoints[_nextIndex].position, speed);
    }
}
