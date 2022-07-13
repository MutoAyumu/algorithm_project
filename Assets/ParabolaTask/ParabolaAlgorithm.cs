using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaAlgorithm : MonoBehaviour
{
    [SerializeField] Transform _startPoint, _middlePoint, _endPoint;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] float _angle = -60f;

    //ëΩï™èIÇÌÇ¡ÇƒÇ»Ç¢ÇÃÇ≈ìríÜÇ‹Ç≈Ç≈Ç∑

    public void StartPoint()
    {
        if (!_lineRenderer) return;

        ResetPoint();
        StartCoroutine(DrawingLines());
    }

    IEnumerator DrawingLines()
    {
        var b = Mathf.Tan(_angle * Mathf.Deg2Rad);
        var a = (_endPoint.position.y - b * _endPoint.position.x) / (_endPoint.position.x * _endPoint.position.x);

        int count = 0;

        for (float x = _startPoint.position.x; x <= _endPoint.position.x; x++)
        {
            var y = a * x * x + b * x;
            _lineRenderer.SetPosition(count, new Vector3(x, y));

            if(count == _endPoint.position.x / 2)
            {
                _middlePoint.position = _lineRenderer.GetPosition(count);
            }

            count++;
            yield return null;
        }
    }

    void ResetPoint()
    {
        _lineRenderer.positionCount = 0;
        _lineRenderer.positionCount = (int)(Mathf.Abs(_startPoint.position.x) + Mathf.Abs(_endPoint.position.x)) + 1;
    }
}
