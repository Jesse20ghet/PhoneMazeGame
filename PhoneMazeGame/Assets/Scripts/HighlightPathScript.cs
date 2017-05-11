using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighlightPathScript : MonoBehaviour
{
	float timeBetweenAction = .15F;
	float nextActionTime;

	private int currentIter = 0;

	public List<GameObject> correctPath = new List<GameObject>();

	// Use this for initialization
	void Start ()
	{
		nextActionTime = Time.time + timeBetweenAction;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (currentIter > correctPath.Count - 1)
			Destroy(this);

		if(Time.time > nextActionTime)
		{
			correctPath[currentIter].transform.FindChild("Background").GetComponent<Image>().color = Color.blue;
			correctPath[currentIter].AddComponent<OctagonWinScript>();
			nextActionTime += timeBetweenAction;
			currentIter++;
		}
	}
}
