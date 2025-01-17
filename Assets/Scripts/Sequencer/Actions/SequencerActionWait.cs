using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Sequence/Wait", fileName = "Wait")]
public class SequencerActionWait : SequencerAction
{
    [SerializeField] private float waitTime = 1f;
    public override IEnumerator StartSequence(Sequencer context)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
