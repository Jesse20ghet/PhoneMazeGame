using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using Assets.Code.ConcreteClasses;
using System.IO;
using Assets.Code;
using Assets.Code.Utilities;
using System;

public class PuzzleControllerScript : MonoBehaviour {

	private List<Puzzle> PuzzleList;
	private int currentPuzzleIterator = 0;

	private string CurrentPuzzleName;
	private string CurrentPuzzleCategory;

	GameObject puzzleContainer;
	GameObject puzzleNameRef;
	GameObject puzzleCategoryRef;

	// Use this for initialization
	void Start ()
	{
		puzzleContainer = GameObject.Find("CurrentPuzzleContainer");

		CurrentPuzzleCategory = PlayerPrefs.GetString("Category");
		CurrentPuzzleName = PlayerPrefs.GetString("PuzzleName");

		puzzleNameRef = GameObject.Find("DynamicPuzzleName");
		puzzleCategoryRef = GameObject.Find("DynamicPuzzleCategory");

		PuzzleList = new TextPuzzleParser().ParsePuzzles("Puzzles/" + CurrentPuzzleCategory);

		currentPuzzleIterator = PuzzleList.FindIndex(x => x.PuzzleName == CurrentPuzzleName);
		InstantiatePuzzleInGameWorld(PuzzleList[currentPuzzleIterator]);
	}

	public void UpdateText()
	{
		puzzleNameRef.GetComponent<Text>().text = CurrentPuzzleName;
		puzzleCategoryRef.GetComponent<Text>().text = CurrentPuzzleCategory;
	}

	public void InstantiatePuzzleInGameWorld(Puzzle puzzleToInstantiate)
	{
		puzzleToInstantiate.Octagons = puzzleToInstantiate.Octagons.OrderBy(x => x.YCoordinate).ThenBy(x => x.XCoordinate).ToArray();

		// Set columns and rows of grid container
		var dynamicGridRef = puzzleContainer.GetComponent<DynamicGridScript>();
		var xDiffOctagons = GetXDiff(puzzleToInstantiate.Octagons);
		var yDiffOctagons = GetYDiff(puzzleToInstantiate.Octagons);
		dynamicGridRef.ResizeGrid(puzzleToInstantiate.Width, puzzleToInstantiate.Height);

		foreach(var octagon in puzzleToInstantiate.Octagons)
		{
			// Create appropriate game object type - Could be 3 exit, 4 exit, swap
			var octagonGameObject = GetAppropriateGameObjectOctagon(octagon.Type);
			
			var positionToInstantiate = new Vector3(octagon.XCoordinate, octagon.YCoordinate);
			var objectRotation = GetOctagonRotation(); // Might be trickyish, start rotating could be a 45x, x being the offset, up to 315

			var gameObjectRef = (GameObject)GameObject.Instantiate(octagonGameObject, positionToInstantiate, objectRotation);

			var gameObjectWidth = gameObjectRef.GetComponent<RectTransform>().rect.width;
			gameObjectRef.transform.SetParent(puzzleContainer.transform);
			gameObjectRef.transform.localScale = GetScale(gameObjectRef, xDiffOctagons, gameObjectWidth);

			RemoveWalls(gameObjectRef, octagon);
		}

		UpdateText();
	}

	private void RemoveWalls(GameObject gameObjectRef, Octagon octagon)
	{
		var children = gameObjectRef.GetComponentsInChildren<Transform>();
		foreach(var child in children)
		{
			if(child.tag == "Wall")
			{
				// Check to see if this wall is in the list of this octagons walls.. If not, turn it off
				var wallName = child.name.Split('-')[0];
				var wallNameEnum = (Enumerations.Wall)Enum.Parse(typeof(Enumerations.Wall), wallName);
				if (!octagon.Walls.Contains(wallNameEnum))
				{
					child.gameObject.SetActive(false);
				}
			}
		}
	}

	private Vector3 GetScale(GameObject gameObjectRef, int gridWidthCount, float gameObjectWidth)
	{
		var widthOfPuzzleContainer = puzzleContainer.GetComponent<RectTransform>().rect.width;
		var cellWidth = widthOfPuzzleContainer / gridWidthCount;
		var scale = cellWidth / gameObjectWidth;

		return new Vector3(scale, scale);

	}

	private int GetXDiff(Octagon[] octagons)
	{
		var maxX = octagons.Max(x => x.XCoordinate);
		var minX = octagons.Min(x => x.XCoordinate);

		return Math.Abs(maxX) + Math.Abs(minX) + 1;
	}

	private int GetYDiff(Octagon[] octagons)
	{
		var maxY = octagons.Max(x => x.YCoordinate);
		var minY = octagons.Min(x => x.YCoordinate);

		return Math.Abs(maxY) + Math.Abs(minY) + 1;
	}

	private Quaternion GetOctagonRotation()
	{
		return new Quaternion(0, 0, 0, 0);
	}

	private GameObject GetAppropriateGameObjectOctagon(Enumerations.OctagonType type)
	{
		GameObject octagonToReturn;

		switch(type)
		{
			case Enumerations.OctagonType.Endpoint:
				octagonToReturn = (GameObject)Resources.Load("Octagons/EndpointOctagon");
				break;

			case Enumerations.OctagonType.Normal:
				octagonToReturn = (GameObject)Resources.Load("Octagons/DefaultOctagonContainer");
				break;

			case Enumerations.OctagonType.Empty:
			default:
				octagonToReturn = (GameObject)Resources.Load("Octagons/EmptyOctagon");
				break;
		}

		return octagonToReturn;
	}
}
