using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    float hitPoints;

    public int damageStrength;
    Coroutine damageCoroutine;

    public override IEnumerator DamageCharacter(float damage, float interval)
    {
        while(true){

            hitPoints -= damage;
            if(hitPoints <= float.Epsilon){
                KillCharacter();
                break;
            }
            if(interval > float.Epsilon){
                yield return new WaitForSeconds(interval);

            }
            else {
                break;
            }

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                if (damageCoroutine == null){
                    damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
                }
            }  
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                if (damageCoroutine != null){
                    StopCoroutine(damageCoroutine);
                    damageCoroutine = null;
                }
            }  
        }
    }

    private void OnEnable()
    {
        ResetCharacter();
    }

    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }
    
}
