using UnityEngine;
using Assets.Code.Utilities;

public class OctagonClickControllerScript : MonoBehaviour 
{
	public Enumerations.OctagonAction octagonAction;
	public Enumerations.OctagonColor octagonColor;

	public GameObject linkedOctagon;

	public void OnClick()
	{
		if (octagonColor == Enumerations.OctagonColor.Locked)
			return;

		switch(octagonAction)
		{
			case Enumerations.OctagonAction.Turn:
				RotateOctagon();
				break;

			case Enumerations.OctagonAction.Swap:
				SwapOctagon();
				break;

			default:
				break;
		}
	}

	private void RotateOctagon()
	{
		// If there's a linked octagon, see if its currently rotating
		RotatePieceScript linkedRotatePieceScript = null;
		if (linkedOctagon != null)
			linkedRotatePieceScript = linkedOctagon.gameObject.GetComponent<RotatePieceScript>();

		// See if this octagon is currently rotating
		var thisRotatePieceScript = gameObject.GetComponent<RotatePieceScript>();
		
		// If both scripts are null, that means that we're ok to add a script to them to rotate them
		if (thisRotatePieceScript == null && linkedRotatePieceScript == null)
		{
			gameObject.AddComponent<RotatePieceScript>();

			// If there is a linked octagon, rotate it as well.
			if(linkedOctagon != null)
				linkedOctagon.AddComponent<RotatePieceScript>();
		}
	}

	private void SwapOctagon()
	{
		// If there's a linked octagon, see if its currently rotating
		var linkedOctagonSwapScript = linkedOctagon.gameObject.GetComponent<OctagonSwapScript>();

		// See if this octagon is currently rotating
		var octagonSwapScript = gameObject.GetComponent<OctagonSwapScript>();

		// If both scripts are null, that means that we're ok to add a script to them to rotate them
		if (octagonSwapScript == null && linkedOctagonSwapScript == null)
		{
			var thisPosition = transform.position;
			var linkedPosition = linkedOctagon.transform.position;

			var distanceBetween = Vector3.Distance(thisPosition, linkedPosition);

			gameObject.AddComponent<OctagonSwapScript>();
			gameObject.GetComponent<OctagonSwapScript>().distanceBetween = distanceBetween;
			gameObject.GetComponent<OctagonSwapScript>().positionToMoveTowards = linkedPosition;
			gameObject.transform.SetAsLastSibling();

			linkedOctagon.AddComponent<OctagonSwapScript>();
			linkedOctagon.GetComponent<OctagonSwapScript>().distanceBetween = distanceBetween;
			linkedOctagon.GetComponent<OctagonSwapScript>().positionToMoveTowards = thisPosition;
			linkedOctagon.transform.SetAsLastSibling();
		}
	}
}
