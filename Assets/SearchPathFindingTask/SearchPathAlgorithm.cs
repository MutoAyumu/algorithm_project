using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPathAlgorithm : MonoBehaviour
{
    [SerializeField] Map _mapDate;
    [SerializeField] Color _searchPathColor = Color.blue; //�ŒZ�o�H�̐F
    Vector2 _startPath; //�J�n�n�_�̃p�X
    Vector2 _endPath; //�ŏI�n�_�̃p�X
    bool _isSearching = false;
    List<CellDate> _cellDateList = new List<CellDate>(); //�Z�����i�[���Ă����ׂ̃��X�g

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
    /// ���R�X�g
    /// </summary>
    public float Cost { get; set; }
    /// <summary>
    /// ���v�R�X�g
    /// </summary>
    public float SumCost { get; set; }
    /// <summary>
    /// ����R�X�g
    /// </summary>
    public float ECost { get; set; }
    /// <summary>
    /// �I������Ă��邩�̃t���O
    /// </summary>
    public bool IsOpen { get; set; }
    /// <summary>
    /// �Z���̈ʒu
    /// </summary>
    public Vector2 Pos { get; set; }
}
