using UnityEngine;

public class LineControllerTest : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController lineController;

    private void Start() 
    {
        lineController.SetUpLine(points);
    }
}
