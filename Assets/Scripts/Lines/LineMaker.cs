using UnityEngine;

public class LineMaker : MonoBehaviour
{
    private LineRenderer lineRenderer;    
    private Transform pointOne;
    private Transform pointTwo;
    private bool isLineSetup = false;

    private void Awake() 
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetFirstPoint(Transform point)
    {
        // Set first point position
        pointOne = point;
    }

    public void SetSecondPoint(Transform point)
    {
        // Set second point position
        pointTwo = point;
    }
    
    public void SetUpLine()
    {   
        // Determine the number of points to draw the line between and allow Update to start drawing
        lineRenderer.positionCount = 2;       
        isLineSetup = true;
    }

    private void Update() 
    {
        // Check if a line exists
        if (!isLineSetup) return;

        // We set both points in LineRenderer to draw the line 
        lineRenderer.SetPosition(0, pointOne.position);
        lineRenderer.SetPosition(1, pointTwo.position);

        // Code below is for adding points from array - may come in use in expanding the Line Renderer to use more than 2 points
        /*
        for(int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
        */
    }
}
