using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    int damageInflicted = 5;
    int damageBonus = 0;

    int level = 0;
    CircleCollider2D circleCollider;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        StartCoroutine(Explode());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision is BoxCollider2D)
        {   
            // Verify if object is of tag Enemy
            if(collision.gameObject.tag == "Enemy")
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();

                StartCoroutine(enemy.DamageCharacter(damageInflicted + damageBonus, 0.0f));

            }
        }
    }

 
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(0.583f);
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        //multiply the object scale by the level        
    }

    public void SetDamage(int damage)
    {
        damageInflicted = damage;
    }

    public void SetDamageBonus(int bonus)
    {
        damageBonus = bonus;
    }

    public void SetLevel(int level)
    {
        this.level = level;
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius *= level ;
        transform.localScale += new Vector3(transform.localScale[0]*(level-1), transform.localScale[1]*(level-1), transform.localScale[2]);
    }



    public int GetDamage(){
        return damageInflicted;
    }

    public int GetDamageBonus(){
        return damageBonus;
    }

    public int GetLevel(){
        return level;
    }
}
