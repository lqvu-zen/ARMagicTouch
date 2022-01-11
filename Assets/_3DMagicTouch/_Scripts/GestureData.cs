using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GestureData 
{
    public string name;
    public List<Vector2> points;
}

[Serializable]
public class GestureDataList
{
    public List<GestureData> dataList;
}
