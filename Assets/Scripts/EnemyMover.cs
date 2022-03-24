using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour, IMover
{
	[SerializeField]
	private float defaultSpeed = 10f;

	private WaypointFollower wpFollower;

	public Vector2 Position => transform.position;

	public float Speed { get ; set; }

	public void Move(Vector2 direction, float deltaTime)
	{
		Vector3 result = direction * Speed * deltaTime;
		transform.position += result;
	}

	// Start is called before the first frame update
	void Start()
    {
		Speed = defaultSpeed;
		wpFollower = GetComponent<WaypointFollower>();
    }

	private void Update()
	{
		Vector2 dir = wpFollower.GetDirection();
		Move(dir, Time.deltaTime);
	}
}
