using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundGestionScene : MonoBehaviour {

	public AudioSource mySound;
	public static float sliderValue =1f;

	void Start(){
		sliderValue = SoundsVolume.sliderValue;
	}

	// Update is called once per frame
	void Update () {
		
		mySound.volume = sliderValue;
	}
		
}