using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoTrigger : MonoBehaviour
{
    private bool showGui = false;
    public Texture2D flecheGauche;
    public Texture2D flecheDroite;
    GUIStyle police;

    GUIContent content;
    GUIStyle style = new GUIStyle();
    // Use this for initialization
    void Start()
    {
        police = new GUIStyle();
        police.fontSize = 40;
        police.normal.textColor = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnTriggerEnter2D()
    {
        showGui = true;
    }
    void OnTriggerExit2D()
    {
        showGui = false;
    }

    void OnGUI()
    {
        int w = 250;
        int h = 300;
        Rect rect = new Rect((Screen.width - w) / 2, (Screen.height - h) / 2, w, h);
        if (showGui)
        {
           
            GUI.Label(new Rect((Screen.width - w) / 2 , (Screen.height - h) / 2, w, h), flecheGauche);
            GUI.Label(new Rect((Screen.width - w) / 2 + 100, (Screen.height - h) / 2, w, h), flecheDroite);
            GUI.Label(new Rect((Screen.width - w) / 2 + 200, (Screen.height - h) / 2 +10 , w, h), "Pour vous déplacer", police);

          
        }
            
    }


}
