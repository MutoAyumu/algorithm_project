using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    List<Vector2> _points;
    LineRenderer _lineRenderer;
    Material _material;

    public LineRenderer LineRenderer { get => _lineRenderer;}

    private void Awake()
    {
        _points = new List<Vector2>();
        _lineRenderer = GetComponent<LineRenderer>();

        if(_material == null)
        {
            _material = new Material(Shader.Find("Sprites/Default"));
        }

        _lineRenderer.material = _material;
    }
    public void AddPoint(Vector3 point)
    {
        if(_points.Contains(point))
        {
            return;
        }

        _points.Add(point);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, point);
    }
}
