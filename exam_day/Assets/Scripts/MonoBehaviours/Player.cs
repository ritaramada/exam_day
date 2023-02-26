using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
            if(hitObject !=null){

                            switch(hitObject.itemType)
            {
                case Item.ItemType.COIN:
                    // Add coin to inventory
                    break;
                case Item.ItemType.HEALTH:
                    // Add health to player
                    AdjustHitPoints(hitObject.quantity);
                    break;
            }

            collision.gameObject.SetActive(false);
                
            }

        }
    }

    public void AdjustHitPoints(int amount)
    {
        hitPoints += amount;
        if (hitPoints > maxHitPoints)
        {
            hitPoints = maxHitPoints;
        }

        print("Player hit points: " + hitPoints);
    }
}