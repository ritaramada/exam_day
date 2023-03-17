using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    public float startingHitPoints;
    public float maxHitPoints;
    public virtual void KillCharacter(){
        gameObject.SetActive(false);
    } 

    /**
    * Resets the character to its starting state
    */  
    public abstract void ResetCharacter();

    /**
    * @param damage The amount of damage to deal to the character
    * @param interval The interval at which to deal damage
    */
    public abstract IEnumerator DamageCharacter(float damage, float interval);

    public virtual IEnumerator FlickerCharacter(){
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = Color.white;
    }
    

    
}
