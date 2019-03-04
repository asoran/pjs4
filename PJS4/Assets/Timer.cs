using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	float timer;
	float minutes;
	float secondes;
	float centiemes;
	public int vie;
    public int collectableCount;
    private PlayerController player;
    public Texture2D textureToDisplay;

    GUIStyle police;

	// Use this for initialization
	void Start () {
		police = new GUIStyle();
		police.fontSize = 40;
        vie = 50;
        player = GameObject.Find("player").GetComponent<PlayerController>();
        
    }

	// Update is called once per frame
	void Update () {
        collectableCount = player.collectableCount;
        timer += Time.deltaTime;
		minutes = timer / 120;
		secondes = timer % 60;
		centiemes = (timer * 100) % 100;
	}

	void OnGUI () {
		GUI.Label (new Rect (5, 1, 60, 25), minutes.ToString("00") + ":" + secondes.ToString("00") + ":" + centiemes.ToString("00"), police);
		GUI.Label (new Rect (5, 40, 30, 25), "Vie : " + vie, police);
        GUI.Label(new Rect(5, 80, 50, 50), textureToDisplay );
        GUI.Label(new Rect(70, 82, 30, 25), "" + collectableCount, police);
    }
}