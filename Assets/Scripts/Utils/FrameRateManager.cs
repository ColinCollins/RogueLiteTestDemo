﻿
using System.Collections;
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
	[Header("OnGUI for frame rate---")]
	public Color textColor = Color.white;
	public int guiFontSize = 50;
	private string label = string.Empty;
	private GUIStyle style = new GUIStyle();
	private float count;

	private void Awake()
	{
		// set owner frame rate
		Application.targetFrameRate = 60;
	}

	private IEnumerator Start()
	{
		while (true)
		{
			count = 1f / Time.deltaTime;
			label = string.Format("{0:N2}", count);
			yield return new WaitForSeconds(0.2f);

		}
	}
	// show fps
	private void OnGUI()
	{
		style.fontSize = guiFontSize;
		style.normal.textColor = textColor;
		GUI.Label(new Rect(300f, 200f, 500f, 300f), this.label, this.style);
	}
}