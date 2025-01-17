using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableController : MonoBehaviour
{
    [SerializeField] private bool isAllowedToInstantiateAssets = false;
    [SerializeField] private bool isAllowedToDestuctAssets = false;
    [SerializeField] private AssetReferenceGameObject evidenceWindowReference;
    [SerializeField] private AddressableInstantiator instantiator;
    
    private void Awake() 
    {        
        // If a reference to the instantiator is missing, try go grab it via code
        if (instantiator == null) instantiator = FindFirstObjectByType<AddressableInstantiator>();
    }

    // Call the instantiator method with parameters to instantiate object and pass a gameobject to set a parent and coordinates
    public void InstantiateAsset(GameObject passedGameObject)
    {
        if (!isAllowedToInstantiateAssets)
        {
            Debug.LogWarning("This controller is not allowed to instantiate assets! Check is everything is set up correctly!");
            return;
        }         
        instantiator.LoadAsset(evidenceWindowReference, passedGameObject);
    }

    // Delete a gameobject and call the instantiator to release the adressable asset from the memory
    public void DestructAsset(GameObject passedGameObject)
    {
        if (!isAllowedToDestuctAssets)
        {
            Debug.LogWarning("This controller is not allowed to destruct assets! Check is everything is set up correctly!");
            return;
        } 

        instantiator.ReleaseAsset(passedGameObject);
    }
}
