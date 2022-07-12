using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField] int _row = 10;
    [SerializeField] int _courum = 10;
    Image[,] _mapData;
    [SerializeField] Transform _pearent;

    public Image[,] MapData { get => _mapData;}

    private void Awake()
    {
        if(!_pearent)
        {
            _pearent = this.transform;
        }

        _mapData = new Image[_row, _courum];
    }
    private void Start()
    {
        for(int r = 0; r < _row; r++)
        {
            for(int c = 0; c < _courum; c++)
            {
                var cell = new GameObject();
                cell.transform.SetParent(_pearent);
                cell.name = $"Cell[{r},{c}]";

                var image = cell.AddComponent<Image>();
                _mapData[r,c] = image;
            }
        }
    }
}
