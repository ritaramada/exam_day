using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnConsumableRules : MonoBehaviour
{   
    public float timeToSpawn = 5.0f;
    private SpawnPoint spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GetComponent<SpawnPoint>();
        SpawnConsumable();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "CanBePickedUp")
        {   
            StartCoroutine(SpawnConsumableAfterTime(timeToSpawn));
        }
    }

    IEnumerator SpawnConsumableAfterTime(float time){
        yield return new WaitForSeconds(time);
        SpawnConsumable();
        yield break;
    }
    

    public void SpawnConsumable(){
        if(spawnPoint != null){
            spawnPoint.SpawnObject();
        }
    }
    // Update is called once per frame
    void Update()
    {

        
    }
}
