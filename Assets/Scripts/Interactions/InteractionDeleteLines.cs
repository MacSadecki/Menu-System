using UnityEngine;

public class InteractionDeleteLines : MonoBehaviour, IInteractable
{
    public string interactionButtonText = "Clear Lines";  
    private LineController controller;
    public void Interact(GameObject gameObject)
    {
        controller = gameObject.GetComponent<LineController>();
        if (controller == null)
        {
            Debug.LogWarning("LineController is missing on this gameobject! Check if everything is set up correctly!");
            return;
        }
        controller.ClearRenderers();        
    }

}
