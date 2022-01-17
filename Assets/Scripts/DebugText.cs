using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    // Start is called before the first frame update

    TextMeshProUGUI textMesh;

    static string debugText;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = "Debug";
    }

    static public void UpdateDebugText(string text)
    {
        debugText = text;
    }    

    // Update is called once per frame
    void Update()
    {
        textMesh.text = debugText;
    }
}
