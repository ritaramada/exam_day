using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int waveDurationTime = 5000;

    public static GameManager sharedInstance = null;
    public GameTime gameTime;
    public ScorePoints scorePoints;
    public SpawnPoint playerSpawnPoint;
    public CameraManager cameraManager;

    void Awake(){
        if(sharedInstance != null && sharedInstance != this){
            Destroy(this.gameObject);
        }else{
            sharedInstance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        SetupScene();    
        InvokeRepeating("UpdateScoreMultiplier", 0, waveDurationTime);

    }

    void UpdateScoreMultiplier(){
        scorePoints.multiplier +=1;
        Debug.Log("Multiplier increased");

    }



    public void SpawnPlayer(){

        if(playerSpawnPoint != null){
            GameObject player = playerSpawnPoint.SpawnObject();

            cameraManager.virtualCamera.Follow = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameTime.currentTime = Time.time;
        
    }

    void PauseGame(){
        if(gameTime.isPaused == false){
            gameTime.isPaused = true;
            gameTime.timeScale = 0;
            Time.timeScale = 0;
        }else{
            Debug.Log("Game is already paused");
        }

    }

    void ResetScore(){
        scorePoints.score = 0;
    }

    void UnPauseGame(){
        if(gameTime.isPaused == true){
            gameTime.isPaused = false;
            gameTime.timeScale = 1;
            Time.timeScale = 1;
        }else{
            Debug.Log("Game unpaused");
        }
    }

    public void awardPoints(int points){
        scorePoints.score += points * scorePoints.multiplier;
        Debug.Log("Current points are:" + scorePoints.score );
    }


    public void SetupScene(){
        SpawnPlayer();

    }
}
