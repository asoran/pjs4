using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TexteDeroulant : MonoBehaviour {
	private string currentText = "";
	public string fullText;
	public float delay = 0.1f;

	// Use this for initialization
	void Start () {
		StartCoroutine (AutoType ());
	}
	
	// Update is called once per frame
	IEnumerator AutoType () {
		for(int i = 0; i < fullText.Length; i++) {
			currentText = fullText.Substring (0, i);
			this.GetComponent<Text> ().text = currentText;
			yield return new WaitForSeconds (delay);
		}
	}
}
