using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMovement : MonoBehaviour
{
    public float lifeTime = 2.0f;
    public float distance = 5.0f;

    private Vector3 startPosition;

    // Start is called before the first frame update

    // Update is called once per frame
    public IEnumerator Travel(Vector3 destination, float duration){
        //From normalized direction, calculate the end position
        startPosition = transform.position;

        var percentComplete = 0.0f;

        while(percentComplete < 1.0f){
            percentComplete += Time.deltaTime/ duration;
            transform.position = Vector3.Lerp(startPosition, destination, percentComplete);


            yield return null;
            if(gameObject.activeSelf == false){
                gameObject.transform.rotation = Quaternion.identity;
                yield break;
            }
        }
        gameObject.SetActive(false);
        gameObject.transform.rotation = Quaternion.identity;

    }
    
}