using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionControllerScript : MonoBehaviour {

	List<GameObject> activatedObjects = new List<GameObject>();
	PuzzleControllerScript puzzleController;

	private bool enabled = true;

	// Use this for initialization
	void Start ()
	{
		puzzleController = GameObject.Find("PuzzleController").GetComponent<PuzzleControllerScript>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void EnableActions()
	{
		enabled = true;
	}

	public void DisableActions()
	{
		enabled = false;
	}

	public void Rotate(List<GameObject> rotateObjects)
	{
		if (!enabled)
			return;

		// If any object is rotating, don't rotate any of them.
		foreach(var rotateObject in rotateObjects)
		{
			if (rotateObject.GetComponent<RotatePieceScript>() != null)
				return;
		}

		foreach(var rotateObject in rotateObjects)
		{
			rotateObject.AddComponent<RotatePieceScript>();
		}
	}

	public void Swap(List<GameObject> swapObjects)
	{
		if (!enabled)
			return;

		// If any of the pieces to swap are currently swapping, we can't swap them again.
		foreach(var swapObject in swapObjects)
		{
			if (swapObject.GetComponent<OctagonSwapScript>() != null)
				return;
		}

		for(int i = 0; i < swapObjects.Count; i++)
		{
			var nextOctagonInt = i + 1;
			if (nextOctagonInt == swapObjects.Count)
				nextOctagonInt = 0;

			var distanceBetween = Vector3.Distance(swapObjects[i].transform.position, swapObjects[nextOctagonInt].transform.position);
			var positionToMoveTowards = swapObjects[nextOctagonInt].transform.position;

			swapObjects[i].AddComponent<OctagonSwapScript>();
			swapObjects[i].GetComponent<OctagonSwapScript>().distanceBetween = distanceBetween;
			swapObjects[i].GetComponent<OctagonSwapScript>().positionToMoveTowards = positionToMoveTowards;
			swapObjects[i].transform.SetAsLastSibling();
		}

	}

	public void Completed(GameObject gameObject)
	{
		if (activatedObjects.Contains(gameObject))
			activatedObjects.Remove(gameObject);

		// If no more objects are currently activated, check for win
		if (activatedObjects.Count == 0)
			puzzleController.CheckForWin();
	}
}
