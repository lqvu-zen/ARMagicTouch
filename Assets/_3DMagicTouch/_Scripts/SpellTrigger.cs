using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTrigger : MonoBehaviour
{
    public DrawLine drawLine;
    public GestureDetector detector;
    public SpellController spellController;
    public PlayerTransform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        drawLine.OnFinishDraw += DetectAndTriggerSpell;
    }

    void DetectAndTriggerSpell(LineRenderer line){
        string spellName = detector.RecognizeSpell(line);
        spellController.SpawnSpell(spellName, playerTransform);
    }
}
