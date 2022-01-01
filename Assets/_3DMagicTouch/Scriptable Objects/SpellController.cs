using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellController", menuName = "ScriptableObjects/SpellController")]
public class SpellController : ScriptableObject 
{
    public List<Spell> spells;

    public Spell FindSpellByName(string spellName){
        foreach(Spell spell in spells){
            if (spell.spellName.Equals(spellName)){
                return spell;
            }
        }
        return null;
    }

    public void SpawnSpell(string spellName, Vector3 pos){
        Debug.Log("SpawnSpell:" +spellName);
        Spell spell = FindSpellByName(spellName);
        if (spell == null){
            return;
        }
        Instantiate(spell, pos, Quaternion.identity);
    }
}
