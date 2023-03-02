using UnityEngine;
[CreateAssetMenu(menuName = "Player Direction")]
public class PlayerDirection : ScriptableObject
{
    public float directionAngle = 0.0f;
    public Vector2 directionVector = Vector2.right;
}