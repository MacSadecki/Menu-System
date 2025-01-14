using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AdressableInstantiator : MonoBehaviour
{
    GameObject instanceReference;

    // For testing purposes, will be deleted later
    private void Update() 
    {
        //if (Input.GetKeyDown(KeyCode.I)) LoadAsset();
        //if (Input.GetKeyDown(KeyCode.O)) ReleaseAsset();
    }

    // Work in progress
    // Start loading and instantiating a gameobject from an adressable
    public void LoadAsset(AssetReferenceGameObject reference, GameObject gameObject)
    {
        //windowReference.InstantiateAsync().Completed += OnAdressableInstantiated;

        // Instantiate a gameobject from passed adressable reference at a set position with a set parent
        reference.InstantiateAsync(gameObject.transform.position, Quaternion.identity, gameObject.transform.parent.gameObject.transform);        
    }

    // Work in progress
    // Release the passed adressable asset from the memory. Used when deleting or disabling an asset containg an adressable package
    public void ReleaseAsset(GameObject gameObject)
    {
       // windowReference.ReleaseInstance(instanceReference);
    }

    // Work in progress
    // When instantiation is complete, grab a reference to the instantiated object for later use, eg. releasing asset
    private void OnAdressableInstantiated(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded) instanceReference = handle.Result; 
        Debug.Log(instanceReference);       
    }
}
