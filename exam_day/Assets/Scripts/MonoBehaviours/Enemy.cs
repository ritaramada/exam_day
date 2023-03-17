using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public GameObject coinPrefab;
    public GameObject powerUp1Prefab;
    public GameObject powerUp2Prefab;

    private GameObject gameManager;
    GameManager gameManagerScript;

    public int coinDropChance = 50;
    public int powerUp1DropChance = 20;
    public int powerUp2DropChance = 10;

    public int poolSize;

    public int pointsGiven = 50;

    public static Dictionary<string, List<GameObject>> objectPool;
    float hitPoints;
    EnemyBasicPathing pathing;
    public int damageStrength;
    Coroutine damageCoroutine;

    int currentLevel = 1;

    void Awake(){
        pathing = GetComponent<EnemyBasicPathing>();

        if(objectPool == null){
            objectPool = new Dictionary<string, List<GameObject>>();
        }

        FillPool(coinPrefab);
        FillPool(powerUp1Prefab);
        FillPool(powerUp2Prefab);
    
    }

    void Start(){
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        
    }

    private void FillPool(GameObject prefab){
        if(prefab != null){
            if(!objectPool.ContainsKey(prefab.name)){
                objectPool.Add(prefab.name, new List<GameObject>());
            }
            if(objectPool[prefab.name].Count < poolSize) {
                for(int i = objectPool[prefab.name].Count; i < poolSize; i++){
                    GameObject item = Instantiate(prefab);
                    item.SetActive(false);
                    objectPool[prefab.name].Add(item);
                }
            }
        }
    }
    public override IEnumerator DamageCharacter(float damage, float interval)
    {
        while(true){
            StartCoroutine(FlickerCharacter());

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
        hitPoints = startingHitPoints + currentLevel*3;
        pathing.ResetPathing();

    }

    private void SpawnConsumable(){
        int random = Random.Range(0, 100);
        if(random < coinDropChance && coinPrefab != null){
            SpawnFromPool(coinPrefab );
        }
        else if(random < powerUp1DropChance + coinDropChance && powerUp1Prefab != null){
            SpawnFromPool(powerUp1Prefab);
        }
        else if(random < powerUp2DropChance + powerUp1DropChance + coinDropChance && powerUp2Prefab != null){
            SpawnFromPool(powerUp2Prefab);
        }
        
    }

    private void SpawnFromPool(GameObject prefab){
        if(prefab != null){
            if(objectPool.ContainsKey(prefab.name)){
                for (int i = 0; i < objectPool[prefab.name].Count; i++){
                    if(!objectPool[prefab.name][i].activeInHierarchy){
                        objectPool[prefab.name][i].transform.position = transform.position;
                        objectPool[prefab.name][i].SetActive(true);
                        return;
                    }
                }

                GameObject item = Instantiate(prefab);
                item.SetActive(false);
                item.transform.position = transform.position;
                objectPool[prefab.name].Add(item);
                item.SetActive(true);

            }
        }
    }

    private void GivePoints(){
        if(gameManager != null && gameManagerScript != null){
            gameManagerScript.awardPoints(pointsGiven);
        }
        
    }

    public override void KillCharacter()
    {
        base.KillCharacter();
        if (damageCoroutine != null){
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }

        SpawnConsumable();

        //Give points to the player
        GivePoints();

    }

    void Update(){
        if(gameManagerScript!= null){
            currentLevel = gameManagerScript.scorePoints.multiplier;
        }
    }
    
    void OnDestroy(){
        objectPool = null;
    }
}
