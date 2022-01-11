using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "SaveLoadToFile", menuName = "ScriptableObjects/SaveLoadToFile")]
public class SaveLoadToFile : ScriptableObject 
{
    public string path;
    public string fileName;
    
    GestureDataList dataList;

    public void Save(GestureData data){
        dataList = Load();
        dataList.dataList.Add(data);
        string json = JsonUtility.ToJson(dataList);
        Debug.Log(dataList.dataList.Count);
        Debug.Log(json);
        File.WriteAllText(path +'/' + fileName, json);
    }
    public GestureDataList Load(){
        string json = File.ReadAllText(path +'/' +fileName, System.Text.Encoding.UTF8);
        GestureDataList res = JsonUtility.FromJson<GestureDataList>(json);
        if (res == null){
            res = new GestureDataList();
            res.dataList = new List<GestureData>();
        }
        return res;
    }
}
