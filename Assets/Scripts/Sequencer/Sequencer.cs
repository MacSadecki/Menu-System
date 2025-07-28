using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour
{
    public List<SequencerAction> SequencerActions;

    private void Awake() 
    {
        foreach (SequencerAction action in SequencerActions)
        {
            action.Initialize(gameObject);
        }
    }

    // Start the sequence
    public void InitializeSequene()
    {
        StartCoroutine(ExecuteSequence());
    }

    // Call all sequence actions in a sequence
    private IEnumerator ExecuteSequence()
    {
        foreach(SequencerAction action in SequencerActions)
        {
            yield return StartCoroutine(action.StartSequence(this));
        }
    }
}
