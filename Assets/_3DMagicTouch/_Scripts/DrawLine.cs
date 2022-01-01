using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DrawLine : MonoBehaviour
{
    public LineRenderer currentLine;
    public float lineSegment;
    public LineRenderer baseLine;
    public bool useBaseLine;
    public event System.Action<LineRenderer> OnFinishDraw;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Vector2 mousePos = Input.mousePosition;
        float z = Camera.main.transform.position.z + 5f;
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0.5f));
        if (Input.GetMouseButtonDown(0)){
            if (currentLine)
                currentLine.gameObject.SetActive(false);
            currentLine = SpawnNewLine(currentPos);
        }
        if (Input.GetMouseButton(0)){
            AddNewPoint(currentLine, currentPos, lineSegment);
        }

        if (Input.GetMouseButtonUp(0)){
            currentLine.gameObject.SetActive(false);
            if (OnFinishDraw != null){
                OnFinishDraw(currentLine);
            }
        }
    }

	private void AddNewPoint(LineRenderer currentLine, Vector3 currentPos, float lineSegment)
	{
        int pointNum = currentLine.positionCount;
        if (Vector3.SqrMagnitude(currentPos - currentLine.GetPosition(pointNum - 1)) > lineSegment*lineSegment){
            currentLine.positionCount += 1;
            currentLine.SetPosition(currentLine.positionCount - 1, currentPos);
        }
	}

	private LineRenderer SpawnNewLine(Vector3 pos)
	{
        LineRenderer line;
        if (useBaseLine){
            line = Instantiate<LineRenderer>(baseLine);
        }
        else{
            line = new GameObject("line").AddComponent<LineRenderer>();
        }
        line.useWorldSpace = true;
        line.positionCount = 1;
        line.SetPosition(0, pos);
        return line;
	}
}
