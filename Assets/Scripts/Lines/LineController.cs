using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [Header("Lines")]
    public List<LineRenderer> connectedLineRenderers = new List<LineRenderer>();
    
    public void AddRenderer(LineRenderer lineRenderer)
    {
        // Save the reference to a renderer whos line is connected to this gameobject
        connectedLineRenderers.Add(lineRenderer);
    }
    
    public void ClearRenderers()
    {
        // Bug warning!
        Debug.LogWarning("This method contains bugs! - LineRenderers are not being deleted from the List of the second connected LineController!");

        // Delete every LineRenderer in the list to remove all lines connected to this object 
        foreach(LineRenderer renderer in connectedLineRenderers)
        {
            Destroy(renderer.gameObject);
        };

        // Clear the list
        connectedLineRenderers.Clear();
    }   
}



