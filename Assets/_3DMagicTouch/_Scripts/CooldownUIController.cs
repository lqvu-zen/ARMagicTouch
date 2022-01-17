using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownUIController : MonoBehaviour
{
    public List<string> spells;
    public List<CooldownUI> cooldownUIs;
    Dictionary<string, CooldownUI> spellCD = new Dictionary<string, CooldownUI>();
    public SpellTrigger spellTrigger;
    // Start is called before the first frame update
    void Start()
    {
        spellTrigger.onSpellTrigger += SetCooldown;   
        for (int i = 0; i < spells.Count; ++i)
        {
            spellCD.Add(spells[i], cooldownUIs[i]);
        }
    }

    public void SetCooldown(string spell, float sec)
    {
        CooldownUI cooldownUI = spellCD[spell];
        if (cooldownUI == null) return;
        cooldownUI.SetCooldown(sec);
    }

}
