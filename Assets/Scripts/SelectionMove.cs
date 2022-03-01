using System.Collections;
using UnityEngine;

/// <summary>
/// Simple class that can be set to move towards a target.
/// </summary>
public class SelectionMove : MonoBehaviour
{
    [SerializeField, Tooltip("Used as speed when moving this object to the touch position")]
    private float maxDelta = 5f;

	public void StartMovingTo(Vector3 target)
	{
		StartCoroutine(Move(target));
	}

	private IEnumerator Move(Vector3 target)
    {
		// Vector2's and 3's are basically floating point numbers that shouldn't be compared with == or !=.
		// However, Unity has overridden the 'equals' and 'not equals' operators on Vectors so that it returns 
		// true on approximate matches.
		while (transform.position != target)
		{
			// Any distance values in Update should be multiplied with the deltaTime
			float delta = maxDelta * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target, delta);
			yield return null;
		}

		Debug.Log(gameObject.name + " reached target");
    }	
}
