using UnityEngine;

public class InteractionOpenAddressable : MonoBehaviour, IInteractable
{   
    public string interactionButtonText = "Open";  
    private AddressableController controller;

    public void Interact(GameObject gameObject)
    {
        // Get the AdressableController from the gameobject to get the reference to the adressable asset
        controller = gameObject.GetComponent<AddressableController>();
        if (controller == null)
        {
            Debug.LogWarning("AdressableController missing! Check if everything is set up correctly!");
            return;
        }

        // Call for intatiation of the adressable asset
        controller.InstantiateAsset(gameObject);
    }

}
