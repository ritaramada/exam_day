using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager sharedInstance = null;

    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;

    void Awake(){
        if(sharedInstance != null && sharedInstance != this){
            Destroy(this.gameObject);
        }else{
            sharedInstance = this;
        }

        GameObject vCamGameObject = GameObject.FindWithTag("VirtualCamera");
        print(vCamGameObject);
        virtualCamera = vCamGameObject.GetComponent<CinemachineVirtualCamera>();

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
