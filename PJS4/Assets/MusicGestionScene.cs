using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicGestionScene : MonoBehaviour {

	public AudioSource mySound;
	public static float sliderValue =0.8f;


	void Start(){
		sliderValue = MusicVolume.sliderValue;
	}

	// Update is called once per frame
	void Update () {

		mySound.volume = sliderValue;
	}

}