using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTrigger : MonoBehaviour
{
    public DrawLine drawLine;
    public GestureDetector detector;
    public SpellController spellController;
    public PlayerTransform playerTransform;
    public event System.Action<string, float> onSpellTrigger;
    
    Dictionary<string, float> spellCD = new Dictionary<string, float>();
    void Awake(){
        for (int i = 0; i < spellController.spells.Count; ++i)
        {
            spellCD.Add(spellController.spells[i].spellName, spellController.cooldowns[i]);
        }
    }
    public float GetCooldowns(string spell)
    {
        Debug.Log(spell);
        return spellCD[spell]; 
    }
    // Start is called before the first frame update
    void Start()
    {
        drawLine.OnFinishDraw += DetectAndTriggerSpell;
        for (int i = 0; i < spellController.activeTime.Count; ++i)
        {
            spellController.activeTime[i] = 0f;
        }
    }

    public void DetectAndTriggerSpell(LineRenderer line){
        if (line.positionCount < 2) return;
        string spellName = detector.RecognizeSpell(line);
        // string spellName = "circle";
        if (spellController.SpawnSpell(spellName, playerTransform))
        {
            if (onSpellTrigger != null)
            {
                onSpellTrigger(spellName, GetCooldowns(spellName));
            }
        }
    }
}
