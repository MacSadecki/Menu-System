using UnityEngine;

public class InteractionCreateLine : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactionButtonText = "Create Line";   

    // Prefab for the Line Maker 
   // public GameObject lineMaker;

    // Line manager of the whole scene
    private LineManager lineManager;

    // Reference to a Line Maker instantiated in this stript
  //  private GameObject usedLineMaker;
    private LineController controller;



    private void Awake() 
    {
        // Cache the reference for ease of use. *This works, but getting the reference via another way than looking through a scene would be more efficient.

    }

    public void Interact(GameObject gameObject)
    {
        lineManager = FindFirstObjectByType<LineManager>();
        if (lineManager == null) Debug.LogError("LineController reference is missing! Check if everything is set up correctly");
        
        controller = gameObject.GetComponent<LineController>();
        if (controller == null)
        {
            Debug.LogWarning("LineController is missing on this gameobject! Check if everything is set up correctly!");
            return;
        }
        lineManager.PassGameObject(gameObject);         
    }



}
