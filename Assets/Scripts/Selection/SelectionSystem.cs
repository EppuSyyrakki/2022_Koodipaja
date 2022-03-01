using PeliprojektiExamples.UI;
using UnityEngine;

namespace PeliprojektiExamples.Selection
{
	public class SelectionSystem : MonoBehaviour
	{
		//  Viittaa scenessä olevaan SelectionSystemiin.
		private static SelectionSystem current;
		public static SelectionSystem Current
		{
			get { return current; }
		}

		private SelectionUI ui;

		// Valittu olio
		private Selectable selected;

		public Selectable Selected
		{
			get { return selected; }
			set
			{
				selected = value;

				// Viittaus olioon voi olla null. Tämä tarkoittaa, että oliota ei ole olemassa.
				// Null-viittaus on laillinen käyttötapaus ja siihen pitää varautua
				if (selected != null)
				{
					Debug.Log(selected.Name + " selected");
				}
				else
				{
					Debug.Log("Selection cleared");
				}

				// Välitetään valinta UI:lle
				ui.SetSelectedObject(selected);
			}
		}

		private void Awake()
		{
			current = this; // this on viittaus tähän olioon.
		}

		private void Start()
		{
			Input.simulateMouseWithTouches = true;
			ui = FindObjectOfType<SelectionUI>();
		}

		/// <summary>
		/// A multi-character example of using the old input system for moving the selected.
		/// </summary>
		private void Update()
		{
			// no touches or no selected so don't do anything
			if (Input.touchCount == 0 || selected == null) { return; }  

			Touch touch = Input.GetTouch(0);

			// only start moving once the finger is lifted (ensure only 1 move "order" per touch)
			if (touch.phase != TouchPhase.Ended) { return; }

			Vector3 target = GetWorldPosition(touch.position);
			selected.SetMoveTarget(target);
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
