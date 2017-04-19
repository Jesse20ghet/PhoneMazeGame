using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Assets.Code.ConcreteClasses;
using System.IO;
using Assets.Code;
using Assets.Code.Utilities;
using System;

public class PuzzleControllerScript : MonoBehaviour {

	private List<Puzzle> PuzzleList;
	GameObject puzzleContainer;

	// Use this for initialization
	void Start ()
	{
		puzzleContainer = GameObject.Find("CurrentPuzzleContainer");

		var puzzleCategory = PlayerPrefs.GetString("Category");
		var puzzleName = PlayerPrefs.GetString("PuzzleName");
		PuzzleList = new TextPuzzleParser().ParsePuzzles("Puzzles/" + puzzleCategory);

		InstantiatePuzzleInGameWorld(PuzzleList.First(x => x.PuzzleName == puzzleName));
	}

	void InstantiatePuzzleInGameWorld(Puzzle puzzleToInstantiate)
	{
		foreach(var octagon in puzzleToInstantiate.Octagons)
		{
			// Create appropriate game object type - Could be 3 exit, 4 exit, swap
			var octagonGameObject = GetAppropriateGameObjectOctagon(octagon.Type);

			var positionToInstantiate = new Vector3(octagon.XCoordinate, octagon.YCoordinate);
			var objectRotation = GetOctagonRotation(); // Might be trickyish, start rotating could be a 45x, x being the offset, up to 315

			var gameObjectRef = (GameObject)GameObject.Instantiate(octagonGameObject, positionToInstantiate, objectRotation);
			gameObjectRef.transform.parent = puzzleContainer.transform;
			gameObjectRef.transform.Rotate(Vector3.back, octagon.Rotation);
		}
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
			case Enumerations.OctagonType.Start:
				octagonToReturn = (GameObject)Resources.Load("Octagons/StartOctagon");
				break;

			case Enumerations.OctagonType.Finish:
				octagonToReturn = (GameObject)Resources.Load("Octagons/FinishOctagon");
				break;

			case Enumerations.OctagonType.Empty:
			default:
				octagonToReturn = (GameObject)Resources.Load("Octagons/Octagon");
				break;
		}

		return octagonToReturn;
	}
}
