using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellController", menuName = "ScriptableObjects/SpellController")]
public class SpellController : ScriptableObject 
{
    public List<Spell> spells;
    public List<float> cooldowns;
    public List<float> activeTime;
    // public float currentTime;



    public Spell FindSpellByName(string spellName){
        // foreach(Spell spell in spells){
        //     if (spell.spellName.Equals(spellName)){
        //         return spell;
        //     }
        // }
        for (int i = 0; i < spells.Count; ++i)
        {
            if (spells[i].spellName.Equals(spellName))
            {
                Debug.Log(Time.time);
                if ( activeTime[i] + cooldowns[i] <= Time.time)
                {
                    activeTime[i] = Time.time;
                    return spells[i];
                }
                else
                {
                    return null;
                }
            }
        }
        return null;
    }

    public bool SpawnSpell(string spellName, PlayerTransform player){
        Debug.Log("SpawnSpell:" +spellName);
        Spell spell = FindSpellByName(spellName);
        if (spell == null){
            return false;
        }
        Debug.Log("Success");
        Vector3 pos = spell.FindPosition(player);
        Quaternion rot = spell.FindRotation(player);
        Instantiate(spell, pos, rot);
        return true;
    }

}
