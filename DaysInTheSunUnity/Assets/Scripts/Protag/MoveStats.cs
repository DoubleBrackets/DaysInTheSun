using UnityEngine;

[CreateAssetMenu(fileName = "MoveStats", menuName = "Protag/MoveStats", order = 1)]
public class MoveStats : ScriptableObject
{
    [field: SerializeField]
    public float MoveSpeed { get; private set; }

    [field: SerializeField]
    public float MoveAccel { get; private set; }

    [field: SerializeField]
    public float MoveFriction { get; private set; }

    [field: SerializeField]
    public float JumpVelocity { get; private set; }
}