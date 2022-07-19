using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossLogic : MonoBehaviour
{
    [SerializeField] LineCreater _lineCreater = default;
    //[SerializeField] Transform p1, p2, e1, e2;
    int _count;
    LineRenderer _line;
    [SerializeField] SpriteRenderer[] _circleList = default;

    const float _rayLength = 100;

    private void Update()
    {
        //Debug.Log(IsCrossing(p1.position, e1.position, p2.position, e2.position));

        if (!_lineCreater.CurrentLine) return;

        _line = _lineCreater.CurrentLine.LineRenderer;

        if (_line.positionCount != _count)
        {
            _count = _line.positionCount;
            var count = _line.positionCount;

            var s1 = _line.GetPosition(count - 1);
            var e1 = _line.GetPosition(count - 2);

            for (int i = 0; i < count - 2; i++)
            {
                var e2 = _line.GetPosition(i);
                var s2 = _line.GetPosition(i + 1);

                if (IsCrossing(s1, e1, s2, e2))
                {
                    Judge();
                }
            }
        }
    }

    void Judge()
    {
        if (_circleList.Length == 0) return;

        foreach(var c in _circleList)
        {
            var s1 = c.transform.position;
            var e1 = s1 + Vector3.right * _rayLength;

            int count = 0;

            for(int i = 0; i < _line.positionCount - 1; i++)
            {
                var s2 = _line.GetPosition(i + 1);
                var e2 = _line.GetPosition(i);

                if(IsCrossing(s1, e1, s2, e2))
                {
                    count++;
                }
            }

            if(count % 2 == 1)
            {
                c.color = Color.red;
            }
        }
    }

    bool IsCrossing(Vector2 startPos1, Vector2 endPos1, Vector2 startPos2, Vector2 endPos2)
    {
        if(endPos1 == startPos2)
        {
            return false;
        }

        var v1 = (endPos1.x - startPos1.x) * (endPos2.y - startPos2.y) - (endPos1.y - startPos1.y) * (endPos2.x - startPos2.x);

        if(v1 == 0f)
        {
            return false;
        }

        var v2 = ((startPos2.x - startPos1.x) * (endPos2.y - startPos2.y) - (startPos2.y - startPos1.y) * (endPos2.x - startPos2.x)) / v1;
        var v3 = ((startPos2.x - startPos1.x) * (endPos1.y - startPos1.y) - (startPos2.y - startPos1.y) * (endPos1.x - startPos1.x)) / v1;

        if(v2 < 0f || v2 > 1f || v3 < 0f || v3 > 1f)
        {
            return false;
        }

        return true;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(p1.position, e1.position);
    //    Gizmos.DrawLine(p2.position, e2.position);
    //}
}
