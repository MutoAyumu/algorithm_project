using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchPathAlgorithm : MonoBehaviour
{
    [SerializeField] int _cellSize = 15;

    [SerializeField] Cell _cell;
    Cell[,] _cells;

    [SerializeField] RectTransform _parent;

    List<Vector2Int> _routeList = new List<Vector2Int>();

    [SerializeField] Color _normalCellColor;

    [SerializeField] Color _lockCellColor;

    [SerializeField] Color _routeColor;

    //Vector2Int _startPos;

    [SerializeField] Vector2Int[] _lockCells;
    [SerializeField] Vector2Int _startCell;
    [SerializeField] Vector2Int _goalCell;

    void Start()
    {
        _cells = new Cell[_cellSize, _cellSize];
        // Astarèâä˙âª
        ASterManager.Instance.Initialize(_cellSize);

        for (int x = 0; x < _cellSize; x++)
        {
            for (int y = 0; y < _cellSize; y++)
            {
                var cell = Instantiate(_cell, _parent);
                cell.name = $"Cell[{x},{y}]";
                _cells[x, y] = cell;
                cell.SetNodeId(new Vector2Int(x, y));

                var image = cell.GetComponent<Image>();
                image.color = _normalCellColor;

                //cell.transform.localPosition = new Vector3(x, cell.transform.localPosition.y, y);
            }
        }

        foreach(var c in _lockCells)
        {
            SetLock(c, true);
        }
        //SetLock(new Vector2Int(0, 3), true);
        //SetLock(new Vector2Int(1, 3), true);
        //SetLock(new Vector2Int(5, 0), true);
        //SetLock(new Vector2Int(5, 1), true);
        //SetLock(new Vector2Int(5, 2), true);
        //SetLock(new Vector2Int(5, 3), true);
        //SetLock(new Vector2Int(5, 4), true);

        SetupStartPos();
    }

    private void SetupStartPos()
    {
        var image = _cells[_startCell.x, _startCell.y].GetComponent<Image>();
        image.color = _routeColor;

        //var r = Random.Range(0, _cellSize);
        //var c = Random.Range(0, _cellSize);

        //if (!ASterManager.Instance.ReturnNode(new Vector2Int(r, c)))
        //{
        //    _startPos = new Vector2Int(r,c);
        //    var image = _cells[r, c].GetComponent<Image>();
        //    image.color = _routeColor;
        //}
        //else
        //{
        //    SetupStartPos();
        //}
    }

    private void SetLock(Vector2Int nodeId, bool isLock)
    {
        ASterManager.Instance.SetLock(nodeId, isLock);
        _cells[nodeId.x, nodeId.y].GetComponent<Image>().color = _lockCellColor;
    }

    /// <summary>
    /// É{É^ÉìÇ≈égÇ§
    /// </summary>
    public void StartSearchPath()
    {
        Goto(_goalCell);

        //var x = Random.Range(0, _cellSize);
        //var y = Random.Range(0, _cellSize);

        //if (ASterManager.Instance.ReturnNode(new Vector2Int(x, y)))
        //{
        //    StartSearchPath();
        //}
        //else if(_startPos == new Vector2(x,y))
        //{
        //    StartSearchPath();
        //}
        //else
        //{
        //    var cell = _cells[x, y];
        //    Goto(cell.NodeId);
        //}
    }

    void Goto(Vector2Int goalNodeId)
    {
        if (ASterManager.Instance.SearchRoute(_startCell, goalNodeId, _routeList))
        {
            var list = ASterManager.Instance.ReturnBestNodeList();

            foreach (var l in list)
            {
                _cells[l.x, l.y].GetComponent<Image>().color = _routeColor;
                Debug.Log(_cells[l.x, l.y].name);
                //_cells[goalNodeId.x, goalNodeId.y].GetComponent<Image>().color = _routeColor;
            }
        }
    }
}
