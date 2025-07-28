using UnityEngine;

public class InteractionClose : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactionButtonText = "Close";
    public void Interact(GameObject gameObject)
    {        
        Destroy(gameObject); 
    }
}


