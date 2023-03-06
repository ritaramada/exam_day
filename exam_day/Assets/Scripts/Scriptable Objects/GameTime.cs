using UnityEngine;

[CreateAssetMenu(menuName = "GameTime")]
public class GameTime : ScriptableObject
{
    public float timeScale = 1.0f;
    public bool isPaused = false;
    public float currentTime = 0.0f;
}
