using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawLine : MonoBehaviour
{

    private LineRenderer line;
    //public float maxDist = 9;
    public GameObject circle;



    [Range(0, 1)]
    private float pourcentage;
    [SerializeField]
    [Range(0, 1)]
    private float colorR;
    [SerializeField]
    [Range(0, 1)]
    private float colorV;

    [SerializeField]
    float maxDist;




    // Use this for initialization
    void Start()
    {
        maxDist = circle.GetComponent<DrawCircle>().xradius * gameObject.transform.localScale.x;
        pourcentage = 0;
        line = GetComponent<LineRenderer>();
        if (circle == null)
            Debug.Log("Ah");
    }

    // Update is called once per frame
    void Update()
    {
        displayLine();
        handleColorBlackRed();
        //handleColor();
    }

    void displayLine()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        float dist = Vector3.Distance(mousePosition, transform.position);

        if (dist <= maxDist)
        {
            Cursor.visible = false;
            //Debug.Log(d);
            line.enabled = isActiveAndEnabled;
            line.SetPositions(new Vector3[] { transform.position, mousePosition });
        }
        else if (dist > maxDist)
        {
            Cursor.visible = true;
            //line.enabled = false;
            line.enabled = isActiveAndEnabled;

            Vector3 maxPosition = (mousePosition - transform.position).normalized * maxDist + transform.position;


            line.SetPositions(new Vector3[] { transform.position, maxPosition });
        }
    }

    private void handleColor()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float dist = Vector3.Distance(mousePosition, transform.position);
        pourcentage = 1 - (dist / maxDist); //pourcentage de la ligne
        //Debug.Log(pourcentage);


        if (pourcentage > 0.5)
        {
            colorV = 1;
            colorR = (float)(1 - ((pourcentage - 0.5) * 2));
        }
        else if (pourcentage < 0.5)
        {
            colorR = 1;
            colorV = (pourcentage * 2);
        }
        else if (pourcentage == 0.5)
        {
            colorR = 1;
            colorV = 1;
        }

        Color c1 = new Color(0, 1, 0, 1);
        //Color c1 = new Color(colorR, colorV, 0, 1);
        Color c2 = new Color(colorR, colorV, 0, 1);
        line.startColor = c1;
        line.endColor = c2;
        //content.fillAmount = fillAmount;
    }



    private void handleColorBlackRed()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float dist = Vector3.Distance(mousePosition, transform.position);
        pourcentage = 1 - (dist / maxDist); //pourcentage de la ligne
        //Debug.Log(pourcentage);

        colorR = (float)pourcentage;


        Color c1 = new Color(1, 0, 0, 1);
        //Color c1 = new Color(colorR, 0, 0, 1);
        Color c2 = new Color(colorR, 0, 0, 1);
        line.startColor = c1;
        line.endColor = c2;
        //content.fillAmount = fillAmount;
    }


}
