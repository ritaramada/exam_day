using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    public int damageInflicted = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision is BoxCollider2D)
        {   
            // Verify if object is of tag Enemy
            if(collision.gameObject.tag == "Enemy")
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();

                StartCoroutine(enemy.DamageCharacter(damageInflicted, 0.0f));

                gameObject.SetActive(false);
            }
            else if(collision.gameObject.tag == "Player")
            {
                Player player = collision.gameObject.GetComponent<Player>();

                StartCoroutine(player.DamageCharacter(damageInflicted, 0.0f));

                gameObject.SetActive(false);

            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
