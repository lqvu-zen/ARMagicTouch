using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "SaveLoadToFile", menuName = "ScriptableObjects/SaveLoadToFile")]
public class SaveLoadToFile : ScriptableObject 
{
    public string path;
    public string fileName;

    public TextAsset textAsset;
    
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
        // string fullPath = Path.Combine(Application.dataPath, path,fileName);
        // string json = File.ReadAllText(fullPath, System.Text.Encoding.UTF8);
        string json = textAsset.text;
        Debug.Log(json);
        GestureDataList res = JsonUtility.FromJson<GestureDataList>(json);
        if (res == null){
            res = new GestureDataList();
            res.dataList = new List<GestureData>();
        }
        return res;
    }
}
