using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Koodipaja
{
	public class OldInputMove : MonoBehaviour
	{
		[SerializeField, Tooltip("Used as speed when moving this object to the touch position")]
		private float maxDelta = 5f;

		[SerializeField, Tooltip("Draw the touch shape when completed")]
		private bool drawLineRenderer = false;

		private Vector3 _target;    // Saves the current target for this object
		private List<Vector3> _positions = null;    // All touched positions as a list
		private LineRenderer _lineRenderer = null;  // A component that is used to draw simple lines

		// NOTE: I use the _ in names to identify private member variables, it's nothing magical

		private void Start()
		{
			_positions = new List<Vector3>();   // Lists must be initialized with 'new'
			_lineRenderer = GetComponent<LineRenderer>();
		}

		// Update is called once per frame
		private void Update()
		{
			// Any distance values in Update should be multiplied with the deltaTime
			float delta = maxDelta * Time.deltaTime;

			// Vector3.MoveTowards is a useful method that takes a current position, target position and
			// a delta value that defines the maximum movement magnitude.
			transform.position = Vector3.MoveTowards(transform.position, _target, delta);

			if (Input.touchCount > 0)   // Do we have any number of current touches?
			{
				// Yes. So let's get hold of the first one of those (index 0)
				Touch touch = Input.GetTouch(0);

				// If this touch just started:
				if (touch.phase == TouchPhase.Began)
				{
					// Convert the screen position of the touch to a world position and set the _target variable.
					Vector3 touchPos = touch.position;
					Vector3 worldPos = GetWorldPosition(touchPos);
					_target = worldPos;
				}

				// If this touch just ended:
				if (touch.phase == TouchPhase.Ended)
				{
					// Return the _target to the 0, 0, 0 position
					_target = Vector3.zero;

					if (drawLineRenderer)   // If we want to draw the line
					{
						// Set the Line Renderer's position count to match our positions list size
						_lineRenderer.positionCount = _positions.Count;
						// Then convert the positions to an array so the Line Renderer understands it.
						Vector3[] positions = _positions.ToArray();
						_lineRenderer.SetPositions(positions);
					}

					// The line renderer has all the info it needs so we can reset our list
					_positions.Clear();
				}

				// Has the touch moved since last frame? Do we even need this information?
				if (touch.phase == TouchPhase.Moved && drawLineRenderer)
				{
					// Yes. Let's save the new position.
					Vector3 worldPos = GetWorldPosition(touch.position);
					_positions.Add(worldPos);
				}
			}
		}

		private static Vector3 GetWorldPosition(Vector3 touchPos)
		{
			// Change a screen position into a world position using the Main Camera.
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(touchPos);
			worldPos.z = 0;
			return worldPos;
		}
	}
}
