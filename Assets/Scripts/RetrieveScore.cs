using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Koodipaja
{
    public class RetrieveScore : MonoBehaviour
    {
		private DontDestroyExample _dontDestroy = null;

		private void Start()
		{
			// We could use:

			// GameObject go = GameObject.FindGameObjectWithTag("Player");
			// _dontDestroy = go.GetComponent<DontDestroyExample>();

			// But Unity can't find deactivated GO's with the tag search (we had a button to deactivate the Dude GO).
			// FindObjectOfType has a bool parameter that includes inactive objects in the search.

			_dontDestroy = FindObjectOfType<DontDestroyExample>(includeInactive: true);

			// FindObjectsOfType<DontDestroyExample>() would find all of them and return an array. Then we'd need to
			// figure out which one to use. Now we know there's only one.

			TMP_Text scoreText = GetComponent<TMP_Text>();

			if (_dontDestroy == null)
			{
				// It's possible the object wasn't found if we didn't start from the scene that had it, 
				// or it wasn't marked with DontDestroyOnLoad().
				scoreText.text = "Not found";
				return;
			}

			int score = _dontDestroy.RetrieveScoreAndDestroy();
			scoreText.text = score.ToString();
		}
    }
}
