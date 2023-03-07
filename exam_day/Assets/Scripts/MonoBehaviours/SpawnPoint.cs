using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public GameObject prefabToSpawn;
    public float repeatInterval;

    public int poolSize = 2;

    int currentLevel = 1;

    public ScorePoints scorePoints;

    static Dictionary<string, List<GameObject>> objectPool;

    void Awake(){
        if(objectPool == null){
            objectPool = new Dictionary<string, List<GameObject>>();
            Debug.Log("Spawn Point Awake");
        }

        

        if(objectPool.ContainsKey(prefabToSpawn.name) == false){
            objectPool.Add(prefabToSpawn.name, new List<GameObject>());
            for(int i = 0; i < poolSize; i++){
                GameObject newObject = Instantiate(prefabToSpawn);
                newObject.SetActive(false);
                objectPool[prefabToSpawn.name].Add(newObject);
            }
        }else{
            if(objectPool[prefabToSpawn.name].Count < poolSize){
                for(int i = 0; i < poolSize - objectPool[prefabToSpawn.name].Count; i++){
                    GameObject newObject = Instantiate(prefabToSpawn);
                    newObject.SetActive(false);
                    objectPool[prefabToSpawn.name].Add(newObject);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(repeatInterval >0 ){
            InvokeRepeating("SpawnObject", 0, repeatInterval);
        }
    }

    

    public GameObject SpawnObject(){
        
        if(prefabToSpawn != null){
            foreach (GameObject obj in objectPool[prefabToSpawn.name]){
                if(obj.activeSelf == false){
                    obj.transform.position = transform.position;
                    obj.SetActive(true);
                    return obj;
                }
            }
            
            GameObject newObject = Instantiate(prefabToSpawn);
            newObject.transform.position = transform.position;
            objectPool[prefabToSpawn.name].Add(newObject);
            return newObject;

        }
        else return null;
    }

    void OnDestroy(){
        CancelInvoke();
        objectPool = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(repeatInterval != 0){
            if(currentLevel != scorePoints.multiplier){
                currentLevel = scorePoints.multiplier;
                repeatInterval = repeatInterval * 0.7f;
                CancelInvoke();
                InvokeRepeating("SpawnObject", 0, repeatInterval);
            }
        }      
        
    }
}
