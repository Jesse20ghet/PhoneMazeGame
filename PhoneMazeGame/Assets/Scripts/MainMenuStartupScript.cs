using UnityEngine;
using System.Collections;

public class MainMenuStartupScript : MonoBehaviour {

	public GameObject MainMenuCanvas;
	public GameObject PuzzleCategoryCanvas;
	public GameObject PuzzleMenuCanvas;

	// Use this for initialization
	void Start ()
	{
		MainMenuCanvas.GetComponent<Canvas>().enabled = true;
		PuzzleCategoryCanvas.GetComponent<Canvas>().enabled = false;
		PuzzleMenuCanvas.GetComponent<Canvas>().enabled = false;
	}
}
