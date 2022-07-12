using UnityEngine;

/// <summary>
/// Astar�Ŏg�p����m�[�h�f�[�^
/// </summary>
public struct Node
{
    /// <summary>
    /// �m�[�h�̃|�W�V����
    /// </summary>
    public Vector2Int NodeId { get; }

    /// <summary>
    /// ���̃m�[�h�ɂ��ǂ蒅���O�̃m�[�h�|�W�V����
    /// </summary>
    public Vector2Int FromNodeId { get; private set; }

    /// <summary>
    /// �o�H�Ƃ��Ďg�p�ł��Ȃ��t���O
    /// </summary>
    public bool IsLock { get; private set; }

    /// <summary>
    /// �m�[�h�̗L��
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// �K�v�R�X�g
    /// </summary>
    public double Cost { get; private set; }

    /// <summary>
    /// ����R�X�g
    /// </summary>
    private double _eCost;

    /// <summary>
    /// ��̃m�[�h�̐���
    /// </summary>
    internal static Node CreateBlankNode(Vector2Int position)
    {
        return new Node(position, new Vector2Int(-1, -1));
    }

    /// <summary>
    /// �m�[�h����
    /// </summary>
    internal static Node CreateNode(Vector2Int position, Vector2Int goalPosition)
    {
        return new Node(position, goalPosition);
    }

    /// <summary>
    /// CreateBlankNode,CreateNode���g�p���Ă�������
    /// </summary>
    internal Node(Vector2Int nodeId, Vector2Int goalNodeId) : this()
    {
        NodeId = nodeId;
        IsLock = false;
        Remove();
        Cost = 0;
        UpdateGoalNodeId(goalNodeId);
    }

    /// <summary>
    /// �S�[���X�V �q���[���X�e�B�b�N�R�X�g�̍X�V
    /// </summary>
    internal void UpdateGoalNodeId(Vector2Int goal)
    {
        // �����������q���[���X�e�B�b�N�R�X�g�Ƃ���
        _eCost = Mathf.Sqrt(
            Mathf.Pow(goal.x - NodeId.x, 2) +
            Mathf.Pow(goal.y - NodeId.y, 2)
        );
    }

    internal double GetScore()
    {
        return Cost + _eCost;
    }

    internal void SetFromNodeId(Vector2Int value)
    {
        FromNodeId = value;
    }

    internal void Remove()
    {
        IsActive = false;
    }

    internal void Add()
    {
        IsActive = true;
    }

    internal void SetMoveCost(double cost)
    {
        Cost = cost;
    }

    internal void SetIsLock(bool isLock)
    {
        IsLock = isLock;
    }

    internal void Clear()
    {
        Remove();
        Cost = 0;
        UpdateGoalNodeId(new Vector2Int(-1, -1));
    }
}