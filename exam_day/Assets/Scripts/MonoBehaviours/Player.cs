using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public HitPoints hitPoints;

    public PlayerDirection playerDirection;
    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    public void Start()
    {
        hitPoints.value = startingHitPoints;
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
                        // Add coin to inventory
                        break;
                    case Item.ItemType.HEALTH:
                        // Add health to player
                        shouldDisappear =  AdjustHitPoints(hitObject.quantity);
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
                print("Player hit points: " + hitPoints);
            }

            return true;
        }

        return false;


    }

    public override IEnumerator DamageCharacter(float damage, float interval)
    {
        while (true)
        {

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

    public override void KillCharacter()
    {
        base.KillCharacter();

        Destroy(healthBar.gameObject);
    }

    public override void ResetCharacter()
    {
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
        hitPoints.value = startingHitPoints;

        // Reset player direction to [1,0]
        playerDirection.directionVector = Vector2.right;
        playerDirection.directionAngle = 0.0f;
    }

    private void OnEnable()
    {
        ResetCharacter();
    }
}