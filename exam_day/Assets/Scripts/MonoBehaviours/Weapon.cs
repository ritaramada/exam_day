using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject ammoPrefab;

    public PlayerDirection playerDirection;
    static List<GameObject> ammoPool;
    public int poolSize;

    public float weaponRange = 5.0f;

    public float weaponVelocity = 5.0f;
    // Start is called before the first frame update

    void Awake(){

        if(ammoPool == null){
            ammoPool = new List<GameObject>();
        }

        for(int i = 0; i < poolSize; i++){
            GameObject ammo = Instantiate(ammoPrefab);
            ammo.SetActive(false);
            ammoPool.Add(ammo);
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //When I click space, shoot a bullet
        if(Input.GetKeyDown(KeyCode.Space)){
            print(playerDirection.directionAngle);
            FireAmmo();
        }
        
    }

    GameObject SpawnAmmo(Vector3 location)
    {
        foreach (GameObject ammo in ammoPool)
        {
            if(ammo.activeSelf == false){
                ammo.transform.position = location;
                ammo.SetActive(true);
                return ammo;
            }
        }

        return null;

    }

    void FireAmmo(){
        //Spawn a bullet at the player's position
        GameObject ammo = SpawnAmmo(transform.position);
        if(ammo == null) return;

        //Rotate ammo to face the direction of the player given degree angle
        float playerDirectionAngle = playerDirection.directionAngle;
        ammo.transform.Rotate(new Vector3(0, 0, 1), playerDirectionAngle);
        //From direction
        Vector3 direction = playerDirection.directionVector;
        direction.Normalize();

        //Get the ammo movement component
        AmmoMovement ammoMovement = ammo.GetComponent<AmmoMovement>();
        if(ammoMovement == null) return;

        //Calculate the ending position
        Vector3 endPosition = transform.position + (direction * weaponRange);


        float travelDuration = 1.0f / weaponVelocity;
        print(travelDuration);
        //Start the coroutine

        StartCoroutine(ammoMovement.Travel(endPosition, travelDuration));

    }

    void OnDestroy(){
        ammoPool = null;
    }
}
