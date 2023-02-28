using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject ammoPrefab;
    private List<GameObject> ammoPool;
    public int poolSize;
    public float weaponRange = 5.0f;
    public float weaponFireRate = 3;

    PlayerMovement playerDirection;

    EnemyBasicPathing enemyBasicPathing;
 
    Vector2 directionVector;
    float directionAngle;

    public float weaponVelocity = 5.0f;
    // Start is called before the first frame update

    void Awake(){
     
        if(ammoPool == null){
            ammoPool = new List<GameObject>();
        }

        for(int i = 0; i < poolSize; i++){
            print("Creating ammo; pool size:" + ammoPool.Count);
            GameObject ammo = Instantiate(ammoPrefab);
            ammo.SetActive(false);
            ammoPool.Add(ammo);
        }
        
    }
    void Start()
    {
        //Verify if object is of tag Enemy
        if(gameObject.tag == "Player"){
            playerDirection = GetComponent<PlayerMovement>();
            if(playerDirection == null) return;
            directionAngle = playerDirection.directionAngle;
            directionVector = playerDirection.directionVector;

        }

        if(gameObject.tag == "Enemy"){
            enemyBasicPathing = GetComponent<EnemyBasicPathing>();
            if(enemyBasicPathing == null) return;
            directionAngle = enemyBasicPathing.directionAngle;
            directionVector = enemyBasicPathing.directionVector;

            StartCoroutine(this.PeriodicFire());
        }
    }

    // Update is called once per frame
    void Update()
    {   

        if(gameObject.tag == "Player"){
            if(playerDirection == null) return;
            
            directionAngle = playerDirection.directionAngle;
            directionVector = playerDirection.directionVector;

            if(Input.GetKeyDown(KeyCode.Space)){
                FireAmmo();
            }

        }else if(gameObject.tag == "Enemy"){

            if(enemyBasicPathing == null) return;
            directionAngle = enemyBasicPathing.directionAngle;
            directionVector = enemyBasicPathing.directionVector;

        }else{
            print("Error in object");
            return;
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
        ammo.transform.Rotate(new Vector3(0, 0, 1), directionAngle);
        //From direction
        directionVector.Normalize();
        Vector3 direc = new Vector3(directionVector[0], directionVector[1], 0);

        //Get the ammo movement component
        AmmoMovement ammoMovement = ammo.GetComponent<AmmoMovement>();
        if(ammoMovement == null) return;

        //Calculate the ending position
        Vector3 endPosition = transform.position + (direc * weaponRange);


        float travelDuration = 1.0f / weaponVelocity;
        //Start the coroutine

        StartCoroutine(ammoMovement.Travel(endPosition, travelDuration));

    }

    private IEnumerator PeriodicFire(){
        while(gameObject.activeSelf == true){
            FireAmmo();
            yield return new WaitForSeconds(weaponFireRate);
        }
    }


    void OnDestroy(){
        ammoPool = null;
    }
}
