using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaAlgorithm : MonoBehaviour
{
    [SerializeField] Vector2 _startPoint, _middlePoint, _endPoint;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] float _angle = 45f;

    const int _lineSize = 100;

    public void StartPoint()
    {
        if (!_lineRenderer) return;

        ResetPoint();
        StartCoroutine(DrawingLines());
    }

    IEnumerator DrawingLines()
    {
        var b = Mathf.Tan(_angle * Mathf.Deg2Rad);
        var a = (_endPoint.y - b * _endPoint.x) / (_endPoint.x * _endPoint.x);

        for (int x = 0; x <= _endPoint.x; x++)
        {
            var y = a * x * x + b * x;
            _lineRenderer.SetPosition(x, new Vector2(x, y));
            yield return null;
        }
    }

    void ResetPoint()
    {
        _lineRenderer.positionCount = 0;
        _lineRenderer.positionCount = _lineSize;
    }
}
