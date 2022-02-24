using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Koodipaja
{
    public class DontDestroyExample : MonoBehaviour
    {
        [SerializeField]
        private int score = 100;

        public void MakePersistent()
		{
            // Be careful with DontDestroyOnLoad() as any object marked with it will persist until it is 
            // manually destroyed, leading to possible bugs and using up resources.

            DontDestroyOnLoad(this);
        }

        public int RetrieveScoreAndDestroy()
		{
            // We expect this method to be called from outside. We could also simply destroy this object
            // from outside at the same time as getting the score, but it's clearer to let every object
            // be in charge of destroying themselves if possible.
            Destroy(gameObject);
            return score;
		}
    }
}
