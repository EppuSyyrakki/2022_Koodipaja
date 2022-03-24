using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Koodipaja
{
    public class Turret : MonoBehaviour
    {
        private const string KEY_KILLED = "enemiesKilled";

        [SerializeField]
        private float rotationSpeed = 10f;

        [SerializeField]
        private float range = 3f;

        [SerializeField]
        private float shootingInterval = 0.5f;

        [SerializeField]
        private int enemiesKilled = 0;

        private List<Transform> _targetsInRange;
        private Transform _target;

        private void Start()
        {
            // Retrieve the integer from PlayerPrefs (registry)
            enemiesKilled = PlayerPrefs.GetInt(KEY_KILLED);

            // Set the trigger radius as the range
            CircleCollider2D collider = GetComponent<CircleCollider2D>();
            collider.radius = range;

            // Initialize the list. It's not a SerializedField so it needs to be 'started' like this.
            _targetsInRange = new List<Transform>();

            // The coroutine that handles 'attacking' the _target.
            StartCoroutine(Attack());         
        }

        private IEnumerator Attack()
        {
            // Be careful with this kind of loop: There's no exit clause.
            while (true)
            {
                yield return new WaitForSeconds(shootingInterval);  // Wait and then continue the loop

                if (_target == null) { continue; }  // no target, go back the beginning of the loop
                
                // These debug lines will only show up in the editor, and only if the 'Gizmos' button is 
                // selected/on in the Game/Scene window.
                Debug.DrawLine(transform.position, _target.position, Color.red, shootingInterval);

                if (_target.GetComponent<IHealth>().TakeDamage())
                {
                    enemiesKilled++;

                    // An enemy was killed so save the new amount to the registry. It would be better to do
                    // this operation only once, for example when a level is completed or at game over.
                    PlayerPrefs.SetInt(KEY_KILLED, enemiesKilled);
                }
            }
        }

        private void Update()
        {
            _target = GetClosest();

            if (_target != null)
			{
                // (SomePosition - OurPosition).normalized is the direction vector (length 1) from 'us' to 'them', 
                // and not the actual vector from here to there.
                Vector2 toTarget = (_target.position - transform.position).normalized;

                // SignedAngle is something between -180 and 180.
                float angleToTarget = Vector2.SignedAngle(transform.up, toTarget);
                float speed = rotationSpeed * Time.deltaTime;

                // We want to rotate along the Z (depth) axis.
                Vector3 rotation = new Vector3(0, 0, angleToTarget * speed);
                transform.Rotate(rotation);              
            }
        }

        /// <summary>
        /// A simple algorithm to find the closest Transform to our transform in the _targetsInRange list.
        /// </summary>
        /// <returns>The transform that is closest to us, null if no Transforms in range.</returns>
        private Transform GetClosest()
		{
            if (_targetsInRange.Count == 0) { return null; }

            Transform closest = _targetsInRange[0];

            foreach (Transform current in _targetsInRange)
			{
                // The Vector3.Distance() calculation uses square root to calculate the distance between two
                // Vector3's. SqrMagnitude skips the root (which is CPU-heavy to do), and can be used when simply
                // comparing two distances to each other, but not if we need the actual distance.
                                
                float distanceToClosest = Vector3.SqrMagnitude(closest.position - transform.position);
                float distanceToCurrent = Vector3.SqrMagnitude(current.position - transform.position);

                if (distanceToCurrent < distanceToClosest)
				{
                    closest = current;
				}
			}

            return closest;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
            // When an enemy comes into range (enters the trigger area)
            if (!collision.gameObject.CompareTag("Enemy")) { return; }

            _targetsInRange.Add(collision.transform);
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
            // When an enemy exits the range (moves outside or otherwise exits the trigger area)
            if (!collision.gameObject.CompareTag("Enemy")) { return; }

            _targetsInRange.Remove(collision.transform);
        }
	}
}
