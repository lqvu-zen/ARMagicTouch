using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "SaveLoadToFile", menuName = "ScriptableObjects/SaveLoadToFile")]
public class SaveLoadToFile : ScriptableObject 
{
    //public string path;
    public string fileName;
    
    GestureDataList dataList;

    public void Save(GestureData data){
        /*dataList = Load();
        dataList.dataList.Add(data);
        string json = JsonUtility.ToJson(dataList);
        Debug.Log(dataList.dataList.Count);
        Debug.Log(json);
        File.WriteAllText(fileName, json);*/
    }
    public GestureDataList Load()
    {
        var textFile = Resources.Load<TextAsset>(fileName);
        GestureDataList res = JsonUtility.FromJson<GestureDataList>(textFile.ToString());

        if (res == null)
        {
            DebugText.UpdateDebugText(textFile.ToString());
            res = new GestureDataList();
            res.dataList = new List<GestureData>();
        }
        return res;
    }
}
