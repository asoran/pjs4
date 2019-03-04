using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;
    [Range(0, 10)]
    public float xradius;
    [Range(0, 10)]
    public float yradius;
    private LineRenderer line;

	public float getTaille(){
		return xradius;
	}

    public void Start()
    {
        line = GetComponent<LineRenderer>();

        //line.SetVertexCount(segments + 1);
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        CreatePoints();
    }

    public void Update()
    {
       /* Vector3 pos;
        float theta = 0f;
        for (int i = 0; i < size; i++)
        {
            theta += (2.0f * Mathf.PI * theta_scale);
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            x += gameObject.transform.position.x;
            y += gameObject.transform.position.y;
            pos = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, pos);
        }
        */
       /* Vector3 pos = Vector3.zero;
        Vector3 pos2 = Vector3.right * 5;
        Debug.Log(pos);
        Debug.Log(pos2);
        
        lineRenderer.SetPosition(0, pos);
        lineRenderer.SetPosition(1, pos2);
        */
		CreatePoints ();
    }

    void CreatePoints()
    {
        float x;
        float y;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }
}
