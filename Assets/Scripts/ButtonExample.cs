using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Koodipaja
{
	/// <summary>
	/// By attaching this script to a GameObject and dragging it to a button's OnClick event we can assign any public
	/// method to the button as long as it has an accepted signature: returns void, with 0 or 1 parameters.
	/// </summary>
    public class ButtonExample : MonoBehaviour
    {
        [SerializeField]
        private GameObject objectToSwitch = null;



        public void SwitchAttachedGameObject(bool enabled)
		{
            // If the variable has not been set in the Inspector, do early exit from this method.
            if (objectToSwitch == null) 
			{
				Debug.LogWarning("No GameObject attached to " + this.name);
				return; 
			}

            // Set the attached GameObject active or inactive according to the input parameter.
            objectToSwitch.SetActive(enabled);
		}

		// Other examples of what the OnClick parameter can be set to in the inspector:
		public void ButtonExampleFloat(float f)
		{
			Debug.Log("Button pressed with float " + f);
		}

		public void ButtonExampleInt(int i)
		{
			Debug.Log("Button pressed with int " + i);
		}

		public void ButtonExampleString(string s)
		{
			Debug.Log("Button pressed with string " + s);
		}

		public void ButtonExampleObject(Object obj)
		{
			// The Object here can be anything that inherits Unity's Object class like GameObject or Component.
			// MonoBehaviours are Components, just like Transform, Sprite Renderer, RigidBody etc.

			Debug.Log("Button pressed with parameter " + obj.name); // All Objects in Unity have a name string.
		}

		public void ButtonExampleObjectInherited(Transform t)
		{
			// This can be used in a button because Transform is a Component.
			// Round the position of the transform parameter to whole numbers (simply drop the decimals).
			Vector2Int pos = new Vector2Int((int)t.position.x, (int)t.position.y);

			Debug.Log("Button pressed, param is transform of " + t.gameObject.name + " at " + pos);
		}



		public void LoadScene(int sceneIndex)
		{


			// with 'using UnityEngine.SceneManagement' we get access to SceneManager.
			SceneManager.LoadScene(sceneIndex);

			// The sceneIndex of each scene can be found from File - Build Settings. Scenes can be dragged
			// from the Project tab or Hierarchy to the settings window.			
		}

		public void LogNamesAndReloadScene()
		{
			// From the Scene struct (struct is much like a class but not quite) we can access stuff like
			// that scene's build index or the root GameObjects in that scene.
			Scene currentScene = SceneManager.GetActiveScene();
			GameObject[] rootGameObjects = currentScene.GetRootGameObjects();
			string s = "Root Objects: ";

			foreach (GameObject o in rootGameObjects)
			{
				// Loop each root object in the scene and add their name to the end of the string
				s += o.name + ", ";
			}

			Debug.Log(s);	// Print the names
			int currentSceneIndex = currentScene.buildIndex;
			SceneManager.LoadScene(currentSceneIndex);	
			
			// Getting the index and loading the scene could be written in a single line:
			// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}


	}
}
