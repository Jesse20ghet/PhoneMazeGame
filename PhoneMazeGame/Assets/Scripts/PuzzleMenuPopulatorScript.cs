using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PuzzleMenuPopulatorScript : MonoBehaviour {

	int currentPuzzle = 1;
	int puzzleRowCount = 0;

	GameObject puzzleUIElement;
	GameObject puzzleContainer;
	float currentXcoordinate = 0;
	float currentYcoordinate = 0;

	float puzzleUIElementWidth = 0;
	float puzzleUIElementHeight = 0;

	float buffer = 10F;

	// Use this for initialization
	void Start ()
	{
		puzzleContainer = GameObject.Find("SelectPuzzleContainer");

		puzzleUIElement = (GameObject)Resources.Load("UI/PuzzleUIElement");
		puzzleUIElementWidth = puzzleUIElement.GetComponent<RectTransform>().sizeDelta.x;
		puzzleUIElementHeight = puzzleUIElement.GetComponent<RectTransform>().sizeDelta.y;

		PopulatePuzzleMenu();
	}

	private void PopulatePuzzleMenu()
	{
		var puzzles = Resources.LoadAll("Puzzles");
		foreach(var puzzle in puzzles)
		{
			CreatePuzzleUIElement();
		}

		for(int i = 0; i < 37; i++)
		{
			CreatePuzzleUIElement();
		}
	}

	private void CreatePuzzleUIElement()
	{
		var uiElementRef = (GameObject)GameObject.Instantiate(puzzleUIElement, new Vector3(0, 0), new Quaternion());
		uiElementRef.transform.parent = puzzleContainer.transform;
		uiElementRef.transform.localPosition = new Vector3(currentXcoordinate, currentYcoordinate);

		uiElementRef.transform.Find("Text").gameObject.GetComponent<Text>().text = currentPuzzle.ToString();

		puzzleRowCount++;
		currentPuzzle++;

		currentXcoordinate += puzzleUIElementWidth + buffer;
		if (puzzleRowCount == 5)//Base 0 so 5 per row
		{
			currentYcoordinate += -puzzleUIElementHeight + -buffer;
			currentXcoordinate = 0;
			puzzleRowCount = 0;
		}

	}
}
