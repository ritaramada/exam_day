using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    public int damageInflicted = 10;

    [HideInInspector]
    public int damageBonus = 0;

    public WeaponUpgrades weaponUpgrades = null;

    public Explosion explosionPrefab = null;

    void OnTriggerEnter2D(Collider2D collision)
    {
        

        if(collision is BoxCollider2D)
        {   
            // Verify if object is of tag Enemy
            if(collision.gameObject.tag == "Enemy")
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();

                StartCoroutine(enemy.DamageCharacter(damageInflicted + damageBonus, 0.0f));

                gameObject.SetActive(false);

                if(weaponUpgrades != null)
                {
                    if(weaponUpgrades.upgrades[WeaponUpgrades.UpgradeType.WEAPON_EXPLOSIVE]>0){
                        if(explosionPrefab != null)
                        {
                            Explosion explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                            explosion.SetDamage(damageInflicted);
                            explosion.SetDamageBonus(weaponUpgrades.upgrades[WeaponUpgrades.UpgradeType.WEAPON_EXPLOSIVE] * 2);
                            explosion.SetLevel(weaponUpgrades.upgrades[WeaponUpgrades.UpgradeType.WEAPON_EXPLOSIVE]);

                        }
                    }
                }

                
            }
            else if(collision.gameObject.tag == "Player")
            {
                Player player = collision.gameObject.GetComponent<Player>();

                StartCoroutine(player.DamageCharacter(damageInflicted + damageBonus, 0.0f));

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
