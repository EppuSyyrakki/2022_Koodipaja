using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PeliprojektiExamples.Selection
{
	public class Selectable : MonoBehaviour, IPointerDownHandler
	{
		[SerializeField, Tooltip("Used as speed when moving this object to the touch position")]
		private float maxDelta = 5f;

		[SerializeField]
		private string objectName;

		[SerializeField]
		private Sprite icon;

		// Property, jossa vain get on määritetty. Käyttäytyy kuin read-only
		// muuttuja.
		public string Name
		{
			get { return objectName; }
			// set { objectName = value; }
		}

		public Sprite Icon
		{
			get
			{
				// 1. Onko icon määritetty? Jos on, palauta se
				if (icon != null)
				{
					return icon;
				}

				// 2. icon ei ole määritetty, uudelleenkäytä spriterendererin sprite
				SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
				if (spriteRenderer != null)
				{
					return spriteRenderer.sprite;
				}

				// 3. SpriteRenderer-komponenttia ei löytynyt
				return null;
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			SelectionSystem.Current.Selected = this;
		}

		// This is called by the SelectionSystem in Update depending on Input.
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
}
