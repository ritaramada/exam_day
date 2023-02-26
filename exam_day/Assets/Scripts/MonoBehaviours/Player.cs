using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    public void Start()
    {
        
        hitPoints.value = startingHitPoints;
        healthBar = Instantiate(healthBarPrefab);

        healthBar.character = this;

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
}