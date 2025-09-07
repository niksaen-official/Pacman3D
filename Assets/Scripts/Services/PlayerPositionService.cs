using UnityEngine;

public class PlayerPositionService : MonoBehaviour, IPositionService
{
    public Vector3 CurrentPosition => transform.position;
}
