using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
    * InvisibilityCloak
    * 
    * This class is a child of PowerUp. It is used to make the player the zombies not follow the player.
    *
    */
public class InvisibilityCloak : PowerUp
{
    Player player;
    public void Start(){    
        duration = 10;
        ActivatePowerUp();
        StartCoroutine(DeactivatePowerUpAfterTime(duration));
    }
    public override void ActivatePowerUp()
    {
        player = GetComponent<Player>();        
        Vector2 posMask = player.currentPos;
        player.posMask = posMask;
    }


    public override void DeactivatePowerUp()
    {
        player.posMask = null;
        player.RemovePowerUp();

        Destroy(this);
        
    }

    IEnumerator DeactivatePowerUpAfterTime(float time)
    {   
        
        yield return new WaitForSeconds(time);
        DeactivatePowerUp();
        yield break;
    }


}
