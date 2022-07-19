using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreater : MonoBehaviour
{
    Line _currentLine;
    [SerializeField] Line _linePrefab;

    public Line CurrentLine { get => _currentLine;}

    private void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            var pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.z = 0;

            CreateLine(pos);

            _currentLine.AddPoint(pos);
        }
        else if(Input.GetButtonUp("Fire2"))
        {
            DestroyLine();
        }
    }
    void CreateLine(Vector2 pos)
    {
        if (_currentLine) return;

        _currentLine = Instantiate(_linePrefab, Vector2.zero, Quaternion.identity, this.transform);
        _currentLine.name = "Line";
        _currentLine.LineRenderer.SetPosition(0, pos);
        _currentLine.LineRenderer.SetPosition(1, pos);
    }
    void DestroyLine()
    {
        if (!_currentLine) return;

        Destroy(_currentLine.gameObject);
        _currentLine = null;
    }
}
