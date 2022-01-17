using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureDetector : MonoBehaviour
{
    DollarRecognizer dollarRecognizer;
    GestureDataList gestureDataList;
    public SaveLoadToFile saveLoadToFile;
    public DrawLine drawLine;
    // Start is called before the first frame update
    void Start()
    {
        dollarRecognizer = new DollarRecognizer();
        gestureDataList = saveLoadToFile.Load();
        //DebugText.UpdateDebugText(gestureDataList.dataList.Count.ToString());
        if (gestureDataList == null){
            Debug.Log("GestureDataList null");
            return;
        }
        foreach (GestureData data in gestureDataList.dataList){
            dollarRecognizer.SavePattern(data.name, data.points);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string RecognizeSpell(){
        LineRenderer line = drawLine.currentLine;
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < line.positionCount; ++i){
            points.Add(line.GetPosition(i));
        }
        DollarRecognizer.Result res = dollarRecognizer.Recognize(points);
        Debug.Log(res.ToString());
        Debug.Log(res.Match.Name);
        return res.Match.Name;
    }
    public string RecognizeSpell(LineRenderer line){
        // LineRenderer line = drawLine.currentLine;
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < line.positionCount; ++i){
            points.Add(line.GetPosition(i));
        }
        DollarRecognizer.Result res = dollarRecognizer.Recognize(points);
        //Debug.Log(res.ToString());
        //Debug.Log(res.Match.Name);
        return res.Match.Name;
    }

    string ExtractFirstWord(string word){
        string t = "";
        foreach(char c in word){
            if (c == ' ') break;
            t += c;
        }
        return t;
    }
}
