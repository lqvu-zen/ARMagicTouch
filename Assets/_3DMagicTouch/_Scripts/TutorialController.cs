using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public List<GameObject> cards;
    public List<string> spellNames;
    int currentID;
    // Start is called before the first frame update
    void Start()
    {
        Turn(0, true);
        FindObjectOfType<SpellTrigger>().TutSpellTrigger += OnSpellDetected;
    }

    void Turn(int id, bool vl)
    {
        cards[id].SetActive(vl);
        currentID = id;
    }

    void OnSpellDetected(string sn)
    {
        if (currentID >= spellNames.Count) return;
        if (sn == (spellNames[currentID]))
        {
            Turn(currentID, false);
            ++currentID;
            if (currentID < spellNames.Count)
                Turn(currentID, true);
        }
    }
}
