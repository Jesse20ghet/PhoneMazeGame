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
	private List<GameObject> OctagonsList = new List<GameObject>();
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
		dynamicGridRef.ResizeGrid(puzzleToInstantiate.Width, puzzleToInstantiate.Height);

		foreach(var octagon in puzzleToInstantiate.Octagons)
		{
			// Create appropriate game object type - Could be 3 exit, 4 exit, swap
			var octagonGameObject = GetAppropriateGameObjectOctagon(octagon.Type);
			var octagonGameObjectControllerScriptRef = octagonGameObject.GetComponent<OctagonClickControllerScript>();

			// Set the gameobject to the appropriate octagon action
			if (octagonGameObjectControllerScriptRef != null)
			{
				octagonGameObjectControllerScriptRef.octagonAction = octagon.Action;
				octagonGameObjectControllerScriptRef.octagonColor = octagon.Color;
			}

			// Set actual color of octagon
			if(octagon.Type != Enumerations.OctagonType.Empty)
				SetVisibleColor(octagonGameObject, octagonGameObjectControllerScriptRef);

			var positionToInstantiate = new Vector3(octagon.XCoordinate, octagon.YCoordinate);
			var objectRotation = GetOctagonRotation(); // Might be trickyish, start rotating could be a 45x, x being the offset, up to 315

			var gameObjectRef = (GameObject)Instantiate(octagonGameObject, positionToInstantiate, objectRotation);
			OctagonsList.Add(gameObjectRef);

			var gameObjectWidth = gameObjectRef.GetComponent<RectTransform>().rect.width;
			gameObjectRef.transform.SetParent(puzzleContainer.transform);
			gameObjectRef.transform.localScale = GetScale(gameObjectRef, puzzleToInstantiate.Width, gameObjectWidth);

			RemoveWalls(gameObjectRef, octagon);
		}

		// Now that all the octagons are in the game world, go through them and wire up their associated tiles
		foreach (var octagonGameObject in OctagonsList)
		{
			var octagonGameObjectScriptRef = octagonGameObject.GetComponent<OctagonClickControllerScript>();
			if(octagonGameObjectScriptRef.octagonColor != Enumerations.OctagonColor.Default && 
				octagonGameObjectScriptRef.octagonColor != Enumerations.OctagonColor.Locked)
			{
				octagonGameObjectScriptRef.linkedOctagon = OctagonsList.Single(x => x.GetComponent<OctagonClickControllerScript>().octagonColor == octagonGameObjectScriptRef.octagonColor
																&& x != octagonGameObject);
			}
		}

		UpdateText();
	}

	private void SetVisibleColor(GameObject octagonGameObject, OctagonClickControllerScript octagonGameObjectControllerScriptRef)
	{
		octagonGameObject.transform.FindChild("Background").GetComponent<Image>().color = 
			EnumToColorConverter.SwapBackgroundColor(octagonGameObjectControllerScriptRef.octagonColor);

		var walls = GetWalls(octagonGameObject);
		foreach(var wall in walls)
		{
			wall.GetComponent<Image>().color = EnumToColorConverter.SwapWallColor(octagonGameObjectControllerScriptRef.octagonColor);
		}
	}

	private void RemoveWalls(GameObject gameObjectRef, Octagon octagon)
	{
		var walls = GetWalls(gameObjectRef);
		foreach(var wall in walls)
		{
				// Check to see if this wall is in the list of this octagons walls.. If not, turn it off
			var wallName = wall.name.Split('-')[0];
			var wallNameEnum = (Enumerations.Wall)Enum.Parse(typeof(Enumerations.Wall), wallName);
			if (!octagon.Walls.Contains(wallNameEnum))
			{
				wall.gameObject.SetActive(false);
			}
		}
	}

	private List<GameObject> GetWalls(GameObject parent)
	{
		var children = parent.GetComponentsInChildren<Transform>();

		List<GameObject> returnList = new List<GameObject>();
		foreach(var child in children)
		{
			if (child.tag == "Wall")
				returnList.Add(child.gameObject);
		}

		return returnList;

	}

	private Vector3 GetScale(GameObject gameObjectRef, int gridWidthCount, float gameObjectWidth)
	{
		var widthOfPuzzleContainer = puzzleContainer.GetComponent<RectTransform>().rect.width;
		var cellWidth = widthOfPuzzleContainer / gridWidthCount;
		var scale = cellWidth / gameObjectWidth;

		return new Vector3(scale, scale);

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