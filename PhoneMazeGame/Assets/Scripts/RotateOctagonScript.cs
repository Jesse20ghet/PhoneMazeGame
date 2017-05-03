using UnityEngine;
using System.Collections;

public class RotateOctagonScript : MonoBehaviour 
{

	public void ToggleRotationDirection()
	{
		Debug.Log("Rotation Direction Clicked");

		var rotatePieceScript = this.gameObject.GetComponent<RotatePieceScript>();
		var components = gameObject.GetComponents<MonoBehaviour>();
		// Check to see if the object is currently rotating
		if (rotatePieceScript == null)
			gameObject.AddComponent<RotatePieceScript>();
	}
}
