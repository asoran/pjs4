using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsVolume : MonoBehaviour {

	public Slider mySlider;
	public AudioSource mySound;
	public static float sliderValue;
	public static SoundsVolume Instance;

	void Start(){
		mySlider.value = SoundGestionScene.sliderValue;
	}

	// Update is called once per frame
	void Update () {
		mySound.volume = mySlider.value;	
	}

	void OnDestroy(){
		sliderValue = mySlider.value;
		//print (sliderValue);
	}
}