using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public HitPoints hitPoints;
    public CoinCounter coinCollector;
    public PlayerMovement playerMovement;
    public GameObject playerUIPrefab;
    GameObject playerUI;
    HealthBar healthBar;

    [HideInInspector]
    public Vector2 currentPos;

    [HideInInspector]
    public PowerUp activePowerUp = null;

    [HideInInspector]
    public Vector2? posMask = null;


    public void Start()
    {
        hitPoints.value = startingHitPoints;
        playerMovement = GetComponent<PlayerMovement>();

        coinCollector.value = 0;
    }

    public void Update()
    {
        if(posMask == null){
            currentPos = transform.position;    
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
            if(hitObject !=null){

                bool shouldDisappear = false;

                switch(hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        shouldDisappear = true;
                        coinCollector.value += 1;
                        break;
                    case Item.ItemType.HEALTH:
                        // Add health to player
                        shouldDisappear =  AdjustHitPoints(hitObject.quantity);
                        break;
                    case Item.ItemType.POWER_UP:
                        // Add powerup to player
                        if(activePowerUp == null){
                            healthBar.powerUpIconSprite = hitObject.sprite;
                            shouldDisappear = true;
                            AddPowerUp(hitObject.objectName);
                        }

                        
                        break;
                    default:
                        break;
                }



                if (shouldDisappear) collision.gameObject.SetActive(false);
                
            }

        }
    }

    public bool AdjustHitPoints(int amount)
    {
        if(hitPoints.value < maxHitPoints)
        {
            hitPoints.value += amount;
            if(hitPoints.value > maxHitPoints)
            {
                hitPoints.value = maxHitPoints;
            }

            return true;
        }

        return false;


    }

    public override IEnumerator DamageCharacter(float damage, float interval)
    {
        while (true)
        {

            StartCoroutine(FlickerCharacter());

            hitPoints.value -= damage;
            if (hitPoints.value <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);

            }
            else
            {
                break;
            }

        }
    }

    public void AddPowerUp(string powerUpName)
    {
        Debug.Log("Adding powerup: " + powerUpName);
        if (activePowerUp != null)
        {
            return;
        }

        switch (powerUpName){
            case "invisibility cloak":
                activePowerUp = gameObject.AddComponent<InvisibilityCloak>();
                break;
            case "thorny armor":
                activePowerUp = gameObject.AddComponent<ThornyArmor>();
                break;
            default:
                break;
        }

    }

    public void RemovePowerUp()
    {
        Debug.Log("Removing powerup");
        activePowerUp = null;

    }

    public override void KillCharacter()
    {
        base.KillCharacter();

        Destroy(healthBar.gameObject);
    }

    public override void ResetCharacter()
    {
        StartUI();
        hitPoints.value = startingHitPoints;

    }

    void StartUI()
    {
        if(playerUI != null)
        {
            Destroy(playerUI);
        }
        playerUI = Instantiate(playerUIPrefab);
        healthBar = playerUI.GetComponent<HealthBar>();


        healthBar.character = this;
    }

    private void OnEnable()
    {
        Debug.Log("Player enabled");
        ResetCharacter();
    }
}