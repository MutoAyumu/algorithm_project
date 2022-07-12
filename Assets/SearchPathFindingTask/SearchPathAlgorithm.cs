using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPathAlgorithm : MonoBehaviour
{
    [SerializeField] Map _mapDate;
    [SerializeField] Color _searchPathColor = Color.blue; //最短経路の色
    Vector2 _startPath; //開始地点のパス
    Vector2 _endPath; //最終地点のパス
    bool _isSearching = false;
    List<CellDate> _cellDateList = new List<CellDate>(); //セルを格納しておく為のリスト

    private void Start()
    {
        if (!_mapDate) return;
        SearchPath();
    }

    void SearchPath()
    {
        //_startPath = 
    }
}
public class CellDate
{
    /// <summary>
    /// 実コスト
    /// </summary>
    public float Cost { get; set; }
    /// <summary>
    /// 合計コスト
    /// </summary>
    public float SumCost { get; set; }
    /// <summary>
    /// 推定コスト
    /// </summary>
    public float ECost { get; set; }
    /// <summary>
    /// 選択されているかのフラグ
    /// </summary>
    public bool IsOpen { get; set; }
    /// <summary>
    /// セルの位置
    /// </summary>
    public Vector2 Pos { get; set; }
}
