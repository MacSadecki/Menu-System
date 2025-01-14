using UnityEngine;

public class InteractionOpenAdressable : MonoBehaviour, IInteractable
{     
    AdressableController controller;

    public void Interact(GameObject gameObject)
    {
        // Get the AdressableController from the gameobject to get the reference to the adressable asset
        controller = gameObject.GetComponent<AdressableController>();
        if (controller == null)
        {
            Debug.LogWarning("AdressableController missing! Check if everything is set up correctly!");
            return;
        }

        // Call for intatiation of the adressable asset
        controller.InstantiateAsset(gameObject);
    }

}
