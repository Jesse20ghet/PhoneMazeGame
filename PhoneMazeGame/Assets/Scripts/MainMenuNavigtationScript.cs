using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Assets.Code.Utilities;

public class MainMenuNavigtationScript : MonoBehaviour {

	public bool showMainMenu;
	public bool showCategoriesMenu;
	public bool showPuzzleMenuCanvas;

	public GameObject MainMenuCanvas;
	public GameObject CategoryCanvas;
	public GameObject SelectPuzzleCanvas;

	void Start()
	{
		MainMenuCanvas = GameObject.Find("MainMenuCanvas");
		CategoryCanvas = GameObject.Find("PuzzleCategorySelectorCanvas");
		SelectPuzzleCanvas = GameObject.Find("PuzzleMenuCanvas");

		if (MainMenuCanvas == null || CategoryCanvas == null || SelectPuzzleCanvas == null)
			throw new Exception("Didn't populate the public canvas gameobject with this script");

		ShowMainMenu();
	}

	public void ShowMainMenu()
	{
		Debug.Log("Show main Menu");
		MainMenuCanvas.GetComponent<Animator>().SetBool("Show", true);
		CategoryCanvas.GetComponent<Animator>().SetBool("Show", false);
	}

	public void ShowCategoriesMenu()
	{
		Debug.Log("Show Categores Menu");
		MainMenuCanvas.GetComponent<Animator>().SetBool("Show", false);
		CategoryCanvas.GetComponent<Animator>().SetBool("Show", true);
		SelectPuzzleCanvas.GetComponent<Animator>().SetBool("Show", false);
	}

	public void ShowPuzzleMenu()
	{
		Debug.Log("Show PUzzle Menu");
		CategoryCanvas.GetComponent<Animator>().SetBool("Show", false);
		SelectPuzzleCanvas.GetComponent<Animator>().SetBool("Show", true);

		//PlayerPrefs.SetString("PuzzleCategory", category);
		//SelectPuzzleCanvas.GetComponent<PuzzleMenuPopulatorScript>().PopulatePuzzleMenu();
	}
}
