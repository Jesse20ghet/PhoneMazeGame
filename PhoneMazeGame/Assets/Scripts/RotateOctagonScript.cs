using UnityEngine;
using System.Collections;

public class RotateOctagonScript : MonoBehaviour 
{

	public void ToggleRotationDirection()
	{
		Debug.Log("Rotation Direction Clicked");

		// Check to see if the object is currently rotating
		if(gameObject.GetComponent<RotatePieceScript>() == null)
			gameObject.AddComponent<RotatePieceScript>();
	}
}
