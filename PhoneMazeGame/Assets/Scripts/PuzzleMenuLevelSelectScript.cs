using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class PuzzleMenuLevelSelectScript : MonoBehaviour
{
	public string PuzzleToLoad = "";
	public string PuzzleCategory = "";

	public void OnClick()
	{
		if (!String.IsNullOrEmpty(PuzzleCategory) && !String.IsNullOrEmpty(PuzzleToLoad))
		{
			Debug.Log("Clicked: " + PuzzleToLoad);

			PlayerPrefs.SetString("PuzzleName", PuzzleToLoad);
			PlayerPrefs.SetString("Category", PuzzleCategory);

			SceneManager.LoadScene("PuzzleScene");
		}
	}
}
