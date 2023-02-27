using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HitPoints hitPoints;

    [HideInInspector]
    public Player character;

    public Image meter;

    float maxHitPoints;

    void Start()
    {
        maxHitPoints = character.maxHitPoints;
    }

    void Update()
    {
        if(character != null){
            meter.fillAmount = hitPoints.value / maxHitPoints;
        }
    }
}