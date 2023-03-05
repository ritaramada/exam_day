using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public HitPoints hitPoints;
    public CoinCounter coinCollector;

    public ScorePoints scorePoints;

    [HideInInspector]
    public Player character;

    public Image meter;
    public TMP_Text counter;

    public TMP_Text scoreCounter;

    float maxHitPoints;

    void Start()
    {
        maxHitPoints = character.maxHitPoints;
    }

    void Update()
    {
        if(character != null){
            meter.fillAmount = hitPoints.value / maxHitPoints;
            counter.text = coinCollector.value.ToString(); 
            scoreCounter.text = scorePoints.score.ToString();
        }
    }
}