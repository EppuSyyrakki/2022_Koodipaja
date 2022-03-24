using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMover
{
    // Hahmon tämänhetkinen sijainti
    Vector2 Position
    {
        get;
    }

    // Hahmon nopeus. Nopeuden voi lukea ja asettaa
    float Speed
    {
        get;
        set;
    }

    void Move(Vector2 direction, float deltaTime);
}

public class WaypointFollower : MonoBehaviour
{
    // A Serialized List and Array work the same way as far as the editor is concerned.
    // Serialized lists dont need to be initialized with 'new List<Transform>()' 
    [SerializeField]
    private List<Transform> waypoints;

    private int _nextIndex;

	public Vector2 Position => transform.position;

	public Vector3 GetDirection()
	{
        Vector2 toWaypoint = waypoints[_nextIndex].position - transform.position;
        
        if (toWaypoint.magnitude < 0.2f)
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

        return toWaypoint.normalized;
    }

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
}
