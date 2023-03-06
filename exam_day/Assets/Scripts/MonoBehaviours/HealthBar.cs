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

    public Image powerUpIcon;

    float maxHitPoints;

    [HideInInspector]
    float powerUpDuration = 10f;
    float lastTime = 0f;
    
    [HideInInspector]
    public Sprite powerUpIconSprite;

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



            if(character.activePowerUp != null){
                powerUpIcon.sprite = powerUpIconSprite;
                powerUpIcon.enabled = true;

                lastTime += Time.deltaTime;

                float secondsLeft = (powerUpDuration - lastTime)/powerUpDuration;
                powerUpIcon.fillAmount = secondsLeft;
            }
            else {
                powerUpIcon.fillAmount = 1f;
                powerUpIcon.enabled = false;
                lastTime = 0f;

            }
        }
    }
}