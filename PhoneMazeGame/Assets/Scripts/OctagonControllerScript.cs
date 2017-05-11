using UnityEngine;
using Assets.Code.Utilities;
using System.Collections.Generic;
using System.Linq;

public class OctagonControllerScript : MonoBehaviour 
{
	public Enumerations.OctagonAction octagonAction;
	public Enumerations.OctagonColor octagonColor;
	public Enumerations.OctagonType octagonType;

	public int XCoordinate;
	public int YCoordinate;
	public int OctagonId;

	public List<GameObject> linkedOctagons = new List<GameObject>();

	private ActionControllerScript actionControllerScript;

	void Start()
	{
		actionControllerScript = GameObject.Find("ActionController").GetComponent<ActionControllerScript>();
	}

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
		// Get this octagon with and linked octagons
		var octagonList = new List<GameObject>();
		foreach (var linkedOctagon in linkedOctagons)
			octagonList.Add(linkedOctagon);

		octagonList.Add(gameObject);

		actionControllerScript.Rotate(octagonList);
	}

	private void SwapOctagon()
	{
		// Get this octagon with and linked octagons
		var octagonList = new List<GameObject>();
		foreach (var linkedOctagon in linkedOctagons)
			octagonList.Add(linkedOctagon);

		octagonList.Add(gameObject);
		octagonList = octagonList.OrderBy(x => x.GetComponent<OctagonControllerScript>().OctagonId).ToList();

		actionControllerScript.Swap(octagonList);
	}
}
