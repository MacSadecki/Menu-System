using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private GameObject cursor;    
    [SerializeField] private GameObject lineMaker;
    private GameObject currentLineRendererGO;  
    private LineMaker currentLineMaker;  
    private bool isLineStarted = false;

    // Get's the reference to gameobject calling this method and passes it to the CreateLine to be used in positioning the line from LineRenderer
    // This method is redundant but was setup in case of future modifications
    public void PassGameObject(GameObject gameObject)
    {
        CreateLine(gameObject);
    }

    public void CreateLine(GameObject linePoint)
    {
        // If the line drawing hasn't yet started or was already completed
        if(!isLineStarted)
        {
            // Instantiate object that draws the lines and cache the references
            currentLineRendererGO = Instantiate(lineMaker, this.gameObject.transform);
            currentLineMaker = currentLineRendererGO.GetComponent<LineMaker>();            

            // Set start of the line as a position of starting gameobject
            currentLineMaker.SetFirstPoint(linePoint.transform);
            // Set end point of the line as a cursor untill player chooses the second point manually
            currentLineMaker.SetSecondPoint(cursor.transform);
            // Setup the line and allow Line Renderer to draw
            currentLineMaker.SetUpLine();

            linePoint.GetComponent<LineController>().AddRenderer(currentLineRendererGO.GetComponent<LineRenderer>());
            isLineStarted = true;
        }
        // If the drawing line is currently in progress
        else if(isLineStarted)
        {            
            // Override the second point of the line from cursor to the designated gameobject
            currentLineMaker.SetSecondPoint(linePoint.transform);

            linePoint.GetComponent<LineController>().AddRenderer(currentLineRendererGO.GetComponent<LineRenderer>());
            currentLineRendererGO = null;
            isLineStarted = false;
        }
    }

    // Get and Set used to change the status of currently connected LineRenderer
    public bool GetLineStatus() => isLineStarted;

    public void SetLineStatus(bool status)
    {
        isLineStarted = status;
    }

    
}
