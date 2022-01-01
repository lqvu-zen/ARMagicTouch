using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GestureCreator : MonoBehaviour
{
    public SaveLoadToFile saveLoadManager;
    public DrawLine drawLine;
    public TMP_InputField inputField;
    // Start is called before the first frame update

    public void AddNewData(){
        LineRenderer line = drawLine.currentLine;
        string name = inputField.text;

        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < line.positionCount; ++i){
            points.Add(line.GetPosition(i));
        }
        
        GestureData data = new GestureData();
        data.name = name;
        data.points = points;
        saveLoadManager.Save(data);
    }

}
