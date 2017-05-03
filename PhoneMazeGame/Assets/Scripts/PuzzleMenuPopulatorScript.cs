using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Assets.Code.ConcreteClasses;
using System.Xml;
using System.Collections.Generic;
using Assets.Code;

public class PuzzleMenuPopulatorScript : MonoBehaviour {

	int currentPuzzleCounter = 1;
	int puzzleRowCount = 0;

	GameObject puzzleUIElement;
	GameObject puzzleContainer;
	float currentXcoordinate = 0;
	float currentYcoordinate = 0;

	float puzzleUIElementWidth = 0;
	float puzzleUIElementHeight = 0;

	float buffer = 10F;
	string currentCategory;

	// Use this for initialization
	void Start ()
	{
		
	}

	public void PopulatePuzzleMenu()
	{
		puzzleContainer = GameObject.Find("PuzzlesContainer");

		// Reset puzzle Coutner to 1 so the gameobjects know what text to value to display
		currentPuzzleCounter = 1;
		foreach (Transform child in puzzleContainer.transform)
		{
			GameObject.Destroy(child.gameObject);
		}

		puzzleUIElement = (GameObject)Resources.Load("UI/PuzzleUIElement");
		puzzleUIElementWidth = puzzleUIElement.GetComponent<RectTransform>().sizeDelta.x;
		puzzleUIElementHeight = puzzleUIElement.GetComponent<RectTransform>().sizeDelta.y;

		// Load the puzzles under the current category
		currentCategory = PlayerPrefs.GetString("PuzzleCategory");
		var puzzlesRaw = Resources.LoadAll("Puzzles/" + currentCategory);

		// Take all the puzzles loaded from resources and convert them into text assets
		List<TextAsset> puzzles = new List<TextAsset>();
		foreach(var puzzleRaw in puzzlesRaw)
		{
			var puzzleToAdd = puzzleRaw as TextAsset;
			puzzles.Add(puzzleToAdd);
		}

		// Create a UI element for each puzzle under the categories
		foreach(var puzzle in puzzles)
		{
			CreatePuzzleUIElement(puzzle);
		}
	}

	private void CreatePuzzleUIElement(TextAsset puzzle)
	{
		var uiElementRef = (GameObject)GameObject.Instantiate(puzzleUIElement, new Vector3(0, 0), new Quaternion());
		uiElementRef.transform.parent = puzzleContainer.transform;
		uiElementRef.transform.localPosition = new Vector3(currentXcoordinate, currentYcoordinate);
		uiElementRef.transform.localScale = Vector3.one;

		uiElementRef.transform.Find("Text").gameObject.GetComponent<Text>().text = currentPuzzleCounter.ToString();

		var levelSelectScript = uiElementRef.GetComponent<PuzzleMenuLevelSelectScript>();
		levelSelectScript.PuzzleCategory = currentCategory;

		var puzzleName = new TextPuzzleParser().ParsePuzzle(puzzle.ToString()).PuzzleName;
		levelSelectScript.PuzzleToLoad = puzzleName;

		puzzleRowCount++;
		currentPuzzleCounter++;

		currentXcoordinate += puzzleUIElementWidth + buffer;
	}
}
