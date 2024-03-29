using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject ammoPrefab;

    public CoinCounter coinCounter;
    static Dictionary<string, List<GameObject>> ammoPool;
    public int poolSize;
    public WeaponUpgrades weaponUpgrades;
    public float weaponRange = 5.0f;
    public float weaponFireRate = 3;

    private GameObject lastAmmo = null;

    PlayerMovement playerDirection;

    EnemyBasicPathing enemyBasicPathing;
 
    Vector2 directionVector;
    float directionAngle;

    public float weaponVelocity = 3.0f;

    [HideInInspector]
    public float baseWeaponVelocity;
    // Start is called before the first frame update

    void Awake(){
        if(ammoPool == null){
            ammoPool = new Dictionary<string, List<GameObject>>();
        }

        if(ammoPool.ContainsKey(ammoPrefab.name) == false){
            ammoPool.Add(ammoPrefab.name, new List<GameObject>());
            for(int i = 0; i < poolSize; i++){
                GameObject ammo = Instantiate(ammoPrefab);
                ammo.SetActive(false);
                ammoPool[ammoPrefab.name].Add(ammo);
            }
        }else{
            if(ammoPool[ammoPrefab.name].Count < poolSize){
                for(int i = 0; i < poolSize - ammoPool[ammoPrefab.name].Count; i++){
                    GameObject ammo = Instantiate(ammoPrefab);
                    ammo.SetActive(false);
                    ammoPool[ammoPrefab.name].Add(ammo);
                }
            }
        }

        
    }
    void Start()
    {
        baseWeaponVelocity = weaponVelocity;
    }

    private void OnEnable(){
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

            weaponVelocity = baseWeaponVelocity + weaponUpgrades.upgrades[WeaponUpgrades.UpgradeType.AMMO_SPEED] * 0.5f;

            if(Input.GetKeyDown(KeyCode.Space)){
                FireAmmo();
            }

            if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Alpha1)){
                UpgradeWeapon(WeaponUpgrades.UpgradeType.DAMAGE);
            }
            if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Alpha2)){
                UpgradeWeapon(WeaponUpgrades.UpgradeType.AMMO_SPEED);
            }
            if(Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Alpha3)){
                UpgradeWeapon(WeaponUpgrades.UpgradeType.WEAPON_SPREAD);
            }
            if(Input.GetKeyDown(KeyCode.V)|| Input.GetKeyDown(KeyCode.Alpha4)){
                UpgradeWeapon(WeaponUpgrades.UpgradeType.WEAPON_EXPLOSIVE);
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

    void UpgradeWeapon(WeaponUpgrades.UpgradeType upgrade){
        if(weaponUpgrades.upgrades[upgrade] < weaponUpgrades.upgradeMax[upgrade]){
            if(coinCounter.value >= weaponUpgrades.upgradeCost[upgrade]*(weaponUpgrades.upgrades[upgrade]+1))
            {
                weaponUpgrades.upgrades[upgrade] += 1;
                coinCounter.value -= weaponUpgrades.upgradeCost[upgrade]*weaponUpgrades.upgrades[upgrade];
            }
        }
    }

    GameObject SpawnAmmo(Vector3 location)
    {

        foreach (GameObject ammo in ammoPool[ammoPrefab.name])
        {

            if(ammo.activeSelf == false){
                ammo.transform.position = location;
                ammo.SetActive(true);
                return ammo;
            }
        }

        //If no ammo is available, create one, add to pool and return it
        GameObject newAmmo = Instantiate(ammoPrefab);
        newAmmo.transform.position = location;
        ammoPool[ammoPrefab.name].Add(newAmmo);
        return newAmmo;


    }

    void FireAmmo(){
        //Spawn a bullet at the player's position
        GameObject ammo = SpawnAmmo(transform.position);
        if(gameObject.tag == "Player"){
            int damageBonus = weaponUpgrades.upgrades[WeaponUpgrades.UpgradeType.DAMAGE]*5;
            ammo.GetComponent<Ammo>().damageBonus = damageBonus;
        }
        lastAmmo = ammo;
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

        if(gameObject.tag == "Player"){
            int ammoAngle = 10;
            for(int i = 0; i< weaponUpgrades.upgrades[WeaponUpgrades.UpgradeType.WEAPON_SPREAD]; i++){
                FireAmmo(ammoAngle);
                FireAmmo(-ammoAngle);
                ammoAngle += 10;
            }
        }

    }

    void FireAmmo(int angle){
        //Spawn a bullet at the player's position
        GameObject ammo = SpawnAmmo(transform.position);
        if(gameObject.tag == "Player"){
            int damageBonus = weaponUpgrades.upgrades[WeaponUpgrades.UpgradeType.DAMAGE];
            ammo.GetComponent<Ammo>().damageBonus = damageBonus;
        }
        lastAmmo = ammo;
        if(ammo == null) return;

        //Rotate ammo to face the direction of the player given degree angle
        ammo.transform.Rotate(new Vector3(0, 0, 1), directionAngle + angle);
        //From direction
        directionVector.Normalize();
        Vector3 direc = new Vector3(directionVector[0], directionVector[1], 0);

        direc = Quaternion.Euler(0, 0, angle) * direc;
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

    private void OnDisable(){
        StopAllCoroutines();

        if(lastAmmo != null){
            AmmoMovement ammoMovement = lastAmmo.GetComponent<AmmoMovement>();
            if(ammoMovement == null) return;
            ammoMovement.StopAllCoroutines();
            lastAmmo.SetActive(false);
            lastAmmo.transform.rotation = Quaternion.identity;
        }

        if(gameObject.tag == "Player"){
            foreach ( WeaponUpgrades.UpgradeType upgrade in System.Enum.GetValues(typeof(WeaponUpgrades.UpgradeType)) )
            {
                weaponUpgrades.upgrades[upgrade] = 0;
            }

        }
        

    }


    void OnDestroy(){
        ammoPool = null;
    }
}
