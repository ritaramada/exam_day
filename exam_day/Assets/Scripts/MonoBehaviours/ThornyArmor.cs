using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornyArmor : PowerUp
{
    Player player;
    public void Start(){    
        duration = 10;
        ActivatePowerUp();
        StartCoroutine(DeactivatePowerUpAfterTime(duration));
    }
    // Start is called before the first frame update

    public override void ActivatePowerUp()
    {
        player = GetComponent<Player>();        
    }

    public override void DeactivatePowerUp()
    {
        player.RemovePowerUp();
        Destroy(this);
        
    }

    IEnumerator DeactivatePowerUpAfterTime(float time)
    {   
        yield return new WaitForSeconds(time);
        DeactivatePowerUp();
        yield break;
    }

    public void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.tag == "Enemy"){
            StartCoroutine(other.gameObject.GetComponent<Enemy>().DamageCharacter(10, 0));
        }

    }

    

}
