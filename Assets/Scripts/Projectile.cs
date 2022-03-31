using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Koodipaja
{

	public class Projectile : MonoBehaviour
	{
		public float Damage { private get; set; }

		private ParticleSystem _ps = null;
		private List<ParticleCollisionEvent> _collisionEvents;

		private void Awake()
		{
			_ps = GetComponent<ParticleSystem>();
			_collisionEvents = new List<ParticleCollisionEvent>();
		}

		private void OnParticleCollision(GameObject other)
		{
			Debug.Log("Particle collision!");
			int events = _ps.GetCollisionEvents(other, _collisionEvents);

			for (int i = 0; i < events; i++)
			{
				if (other == _collisionEvents[i].colliderComponent.gameObject
					&& other.TryGetComponent(out Health h))
				{
					h.TakeDamage();
				}
			}
		}
	}
}
