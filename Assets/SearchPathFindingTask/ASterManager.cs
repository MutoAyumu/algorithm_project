using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASterManager : MonoBehaviour
{
    // 簡易的なシングルトン
    public static ASterManager Instance
    {
        get { return _instance; }
    }

    private static ASterManager _instance;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    private AStar _astar;

    public void Initialize(int tileSize)
    {
        _astar = new AStar();
        _astar.Initialize(tileSize);
    }

    public void SetLock(Vector2Int lockNodeId, bool isLock)
    {
        _astar.SetLock(lockNodeId, isLock);
    }

    public bool ReturnNode(Vector2Int NodeId)
    {
        return _astar.ReturnNode(NodeId);
    }

    public bool SearchRoute(Vector2Int startNodeId, Vector2Int goalNodeId, List<Vector2Int> result)
    {
        return _astar.SearchRoute(startNodeId, goalNodeId, result);
    }
    public List<Vector2Int> ReturnBestNodeList()
    {
        return _astar.ReturnBestNodeList();
    }
}
