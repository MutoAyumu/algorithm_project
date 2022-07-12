using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// A*�A���S���Y��
/// </summary>
public class AStar
{
    private int _fieldSize;
    private Node[,] _nodes;
    private Node[,] _openNodes;
    private Node[,] _closedNodes;

    List<Vector2Int> _bestNodes = new List<Vector2Int>();

    /// <summary>
    /// �΂߈ړ��̏ꍇ�̃R�X�g
    /// </summary>
    private float _diagonalMoveCost;

    /// <summary>
    /// �g�p����O�Ɏ��s���ď��������Ă�������
    /// </summary>
    public void Initialize(int size)
    {
        _fieldSize = size;
        _nodes = new Node[_fieldSize, _fieldSize];
        _openNodes = new Node[_fieldSize, _fieldSize];
        _closedNodes = new Node[_fieldSize, _fieldSize];
        SetDiagonalMoveCost(Mathf.Sqrt(2f));

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                _nodes[x, y] = Node.CreateBlankNode(new Vector2Int(x, y));
                _openNodes[x, y] = Node.CreateBlankNode(new Vector2Int(x, y));
                _closedNodes[x, y] = Node.CreateBlankNode(new Vector2Int(x, y));
            }
        }
    }

    public void SetDiagonalMoveCost(float cost)
    {
        _diagonalMoveCost = cost;
    }


    /// <summary>
    /// ���[�g�����J�n
    /// </summary>
    public bool SearchRoute(Vector2Int startNodeId, Vector2Int goalNodeId, List<Vector2Int> routeList)
    {
        ResetNode();

        // �S�m�[�h�X�V
        for (int x = 0; x < _fieldSize; x++)
        {
            for (int y = 0; y < _fieldSize; y++)
            {
                _nodes[x, y].UpdateGoalNodeId(goalNodeId);
                _openNodes[x, y].UpdateGoalNodeId(goalNodeId);
                _closedNodes[x, y].UpdateGoalNodeId(goalNodeId);
            }
        }

        // �X�^�[�g�n�_�̏�����
        _openNodes[startNodeId.x, startNodeId.y] = Node.CreateNode(startNodeId, goalNodeId);
        _openNodes[startNodeId.x, startNodeId.y].SetFromNodeId(startNodeId);
        _openNodes[startNodeId.x, startNodeId.y].Add();

        while (true)
        {
            var bestScoreNodeId = GetBestScoreNodeId();
            _bestNodes.Add(bestScoreNodeId);
            OpenNode(bestScoreNodeId, goalNodeId);

            // �S�[���ɒH�蒅������I��
            if (bestScoreNodeId == goalNodeId)
            {
                break;
            }
        }

        ResolveRoute(startNodeId, goalNodeId, routeList);
        return true;
    }

    void ResetNode()
    {
        for (int x = 0; x < _fieldSize; x++)
        {
            for (int y = 0; y < _fieldSize; y++)
            {
                _nodes[x, y].Clear();
                _openNodes[x, y].Clear();
                _closedNodes[x, y].Clear();
            }
        }

        _bestNodes.Clear();
    }

    // �m�[�h��W�J����
    void OpenNode(Vector2Int bestNodeId, Vector2Int goalNodeId)
    {
        // 4��������
        for (int dx = -1; dx < 2; dx++)
        {
            for (int dy = -1; dy < 2; dy++)
            {
                int cx = bestNodeId.x + dx;
                int cy = bestNodeId.y + dy;

                if (CheckOutOfRange(dx, dy, bestNodeId.x, bestNodeId.y) == false)
                {
                    continue;
                }

                if (_nodes[cx, cy].IsLock)
                {
                    continue;
                }

                // �c���œ����ꍇ�̓R�X�g : 1
                // �΂߂ɓ����ꍇ�̓R�X�g : _diagonalMoveCost
                var addCost = dx * dy == 0 ? 1 : _diagonalMoveCost;
                _nodes[cx, cy].SetMoveCost(_openNodes[bestNodeId.x, bestNodeId.y].Cost + addCost);
                _nodes[cx, cy].SetFromNodeId(bestNodeId);

                // �m�[�h�̃`�F�b�N
                UpdateNodeList(cx, cy, goalNodeId);
            }
        }

        // �W�J���I������m�[�h�� closed �ɒǉ�����
        _closedNodes[bestNodeId.x, bestNodeId.y] = _openNodes[bestNodeId.x, bestNodeId.y];
        // closedNodes�ɒǉ�
        _closedNodes[bestNodeId.x, bestNodeId.y].Add();
        // openNodes����폜
        _openNodes[bestNodeId.x, bestNodeId.y].Remove();
    }

    /// <summary>
    /// �����͈͓��`�F�b�N
    /// </summary>
    bool CheckOutOfRange(int dx, int dy, int x, int y)
    {
        if (dx == 0 && dy == 0)
        {
            return false;
        }

        int cx = x + dx;
        int cy = y + dy;

        if (cx < 0
            || cx == _fieldSize
            || cy < 0
            || cy == _fieldSize
        )
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// �m�[�h���X�g�̍X�V
    /// </summary>
    void UpdateNodeList(int x, int y, Vector2Int goalNodeId)
    {
        if (_openNodes[x, y].IsActive)
        {
            // ���D�G�ȃX�R�A�ł���Ȃ�MoveCost��from���X�V����
            if (_openNodes[x, y].GetScore() > _nodes[x, y].GetScore())
            {
                // Node���̍X�V
                _openNodes[x, y].SetMoveCost(_nodes[x, y].Cost);
                _openNodes[x, y].SetFromNodeId(_nodes[x, y].FromNodeId);
            }
        }
        else if (_closedNodes[x, y].IsActive)
        {
            // ���D�G�ȃX�R�A�ł���Ȃ� closedNodes���珜�O��openNodes�ɒǉ�����
            if (_closedNodes[x, y].GetScore() > _nodes[x, y].GetScore())
            {
                _closedNodes[x, y].Remove();
                _openNodes[x, y].Add();
                _openNodes[x, y].SetMoveCost(_nodes[x, y].Cost);
                _openNodes[x, y].SetFromNodeId(_nodes[x, y].FromNodeId);
            }
        }
        else
        {
            _openNodes[x, y] = new Node(new Vector2Int(x, y), goalNodeId);
            _openNodes[x, y].SetFromNodeId(_nodes[x, y].FromNodeId);
            _openNodes[x, y].SetMoveCost(_nodes[x, y].Cost);
            _openNodes[x, y].Add();
        }
    }

    void ResolveRoute(Vector2Int startNodeId, Vector2Int goalNodeId, List<Vector2Int> result)
    {
        if (result == null)
        {
            // �{����GC�𔭐������Ȃ����߂ɐ����ς݂̃��X�g��n��
            result = new List<Vector2Int>();
        }
        else
        {
            result.Clear();
        }

        var node = _closedNodes[goalNodeId.x, goalNodeId.y];
        result.Add(goalNodeId);

        int cnt = 0;
        // �{���g���C�񐔂�1000�ƌ��ߑł�(�������[�v�Ή�)
        int tryCount = 1000;
        bool isSuccess = false;
        while (cnt++ < tryCount)
        {
            var beforeNode = result[0];
            if (beforeNode == node.FromNodeId)
            {
                // �����|�W�V�����Ȃ̂ŏI��
                Debug.LogError("�����|�W�V�����Ȃ̂ŏI�����s" + beforeNode + " / " + node.FromNodeId + " / " + goalNodeId);
                break;
            }

            if (node.FromNodeId == startNodeId)
            {
                isSuccess = true;
                break;
            }
            else
            {
                // �J�n���W�͌��ʃ��X�g�ɂ͒ǉ����Ȃ�
                result.Insert(0, node.FromNodeId);
            }

            node = _closedNodes[node.FromNodeId.x, node.FromNodeId.y];
        }

        if (isSuccess == false)
        {
            Debug.LogError("���s" + startNodeId + " / " + node.FromNodeId);
        }
    }

    /// <summary>
    /// �ŗǂ̃m�[�hID��ԋp
    /// </summary>
    Vector2Int GetBestScoreNodeId()
    {
        var result = new Vector2Int(0, 0);
        double min = double.MaxValue;
        for (int x = 0; x < _fieldSize; x++)
        {
            for (int y = 0; y < _fieldSize; y++)
            {
                if (_openNodes[x, y].IsActive == false)
                {
                    continue;
                }

                if (min > _openNodes[x, y].GetScore())
                {
                    // �D�G�ȃR�X�g�̍X�V(�l���Ⴂ�قǗD�G)
                    min = _openNodes[x, y].GetScore();
                    result = _openNodes[x, y].NodeId;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// �m�[�h�̃��b�N�t���O��ύX
    /// </summary>
    public void SetLock(Vector2Int lockNodeId, bool isLock)
    {
        _nodes[lockNodeId.x, lockNodeId.y].SetIsLock(isLock);
    }
    public bool ReturnNode(Vector2Int NodeId)
    {
        return _nodes[NodeId.x, NodeId.y].IsLock;
    }
    public List<Vector2Int> ReturnBestNodeList()
    {
        return _bestNodes;
    }
}