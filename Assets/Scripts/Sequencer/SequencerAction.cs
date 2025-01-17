using System.Collections;
using UnityEngine;

public abstract class SequencerAction : ScriptableObject
{    
    public abstract IEnumerator StartSequence(Sequencer context);

    public virtual void Initialize(GameObject obj) { }
}
