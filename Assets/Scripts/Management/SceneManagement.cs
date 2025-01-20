using UnityEngine;

public class SceneManagement : MonoBehaviour
{   
    [SerializeField] private Sequencer sequencerStartLevel;    
    [SerializeField] bool isPlayedAtStart = false;
    [SerializeField] private Sequencer sequencerEndLevel;
    
    
    private void Start() 
    {
        if (isPlayedAtStart) StartLevelSequence();
    }

    public void StartLevelSequence()
    {
        sequencerStartLevel.InitializeSequene();
    }

    public void EndLevelSequence()
    {        
        sequencerEndLevel.InitializeSequene();
    }

}
