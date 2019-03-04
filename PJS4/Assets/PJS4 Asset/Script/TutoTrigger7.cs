using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoTrigger7 : MonoBehaviour
{
    private bool showGui = false;
    public Texture2D texture;
    GUIStyle police;

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

        if (showGui)
        {

            GUI.Label(new Rect((Screen.width - w) / 2 - 100, (Screen.height - h) / 2 - 25, 60, 50), texture);
            GUI.Label(new Rect((Screen.width - w) / 2 - 100, (Screen.height - h) / 2, w, h), "Les Vortex permettent\nde vous téleporter à un autre vortex", police);


        }

    }
}