using UnityEngine;

public class InteractionClose : MonoBehaviour, IInteractable
{
    public string interactionButtonText = "Close";
    public void Interact(GameObject gameObject)
    {        
        Destroy(gameObject); 
    }
}


