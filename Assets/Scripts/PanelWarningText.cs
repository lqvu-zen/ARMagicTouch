using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelWarningText : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    static string warningText;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = "";
    }

    static public void UpdateWarningText(string text)
    {
        warningText = text;
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = warningText;
    }
}
