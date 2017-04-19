using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class PuzzleMenuLevelSelectScript : MonoBehaviour {

	public string PuzzleToLoad = "";

	public void OnClick()
	{
		var category = transform.parent.name;

		if(!String.IsNullOrEmpty(category) && !String.IsNullOrEmpty(PuzzleToLoad))
		{
			Debug.Log("Clicked: " + PuzzleToLoad);

			PlayerPrefs.SetString("PuzzleName", PuzzleToLoad);
			PlayerPrefs.SetString("Category", category);

			SceneManager.LoadScene("PuzzleScene");
		}
	}
}
