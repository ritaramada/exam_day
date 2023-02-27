using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    public float startingHitPoints;
    public float maxHitPoints;
    public virtual void KillCharacter(){
        Destroy(gameObject);
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

    
}
