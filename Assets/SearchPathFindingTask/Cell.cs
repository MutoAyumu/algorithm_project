using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int NodeId { get; private set; }

    public void SetNodeId(Vector2Int nodeId)
    {
        NodeId = nodeId;
    }
}
