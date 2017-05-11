using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeUpdaterScript : MonoBehaviour {

	private float timer;

	private bool updateTimer = true;

	Text textRef;

	// Use this for initialization
	void Start ()
	{
		textRef = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (updateTimer)
		{
			timer += Time.deltaTime;

			float minutes = Mathf.Floor(timer / 60);
			float seconds = Mathf.RoundToInt(timer % 60);

			string minutesAsString = minutes.ToString();
			string secondsAsString = seconds.ToString();

			if (minutes < 10)
				minutesAsString = "0" + minutes.ToString();
			if (seconds < 10)
				secondsAsString = "0" + Mathf.RoundToInt(seconds).ToString();

			textRef.text = minutesAsString + ":" + secondsAsString;
		}
	}

	public void ResetTimer()
	{
		timer = 0;
	}
}
