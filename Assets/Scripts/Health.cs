using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Koodipaja
{

    public class Health : MonoBehaviour
    {
        [SerializeField]
        private int health = 5;

        /// <summary>
        /// Reduces health by one and destroys the object if health is zero.
        /// </summary>
        /// <returns>True if object was destroyed, false if still alive.</returns>
        public bool TakeDamage()
		{
            health--;

            if (health <= 0)
			{
                // First remove the collider so that Turret's OnTriggerExit2D has a change to execute. That will remove 
                // this gameObject's Transform from it's targetsInRange list. If we simply Destroy an item that is on
                // a list somewhere, there might be null reference errors.
                Destroy(GetComponent<Collider2D>());
                Destroy(gameObject, 0.2f);  // wait 0.2 seconds before destroying the whole thing.
                return true;
			}

            return false;
		}
    }
}
