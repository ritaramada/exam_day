using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int waveDurationTime = 5000;

    public static GameManager sharedInstance = null;
    public GameTime gameTime;
    public ScorePoints scorePoints;
    public SpawnPoint playerSpawnPoint;
    public CameraManager cameraManager;

    public GameObject gameOverScreen;
    bool isGameOver = false;

    GameObject player;

    void Awake(){
        Debug.Log("Game Manager Awake");
        if(sharedInstance != null && sharedInstance != this){
            Destroy(this.gameObject);
        }else{
            sharedInstance = this;
        }


        scorePoints.multiplier = 1;
        scorePoints.score = 0;

        gameTime.currentTime = 0;
        gameTime.timeScale = 1;
        gameTime.isPaused = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        SetupScene();    
        InvokeRepeating("UpdateScoreMultiplier", waveDurationTime, waveDurationTime);

    }

    void UpdateScoreMultiplier(){
        scorePoints.multiplier +=1;
        Debug.Log("Multiplier increased");

    }



    public void SpawnPlayer(){

        if(playerSpawnPoint != null){
            player = playerSpawnPoint.SpawnObject();

            cameraManager.virtualCamera.Follow = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameTime.currentTime = Time.time;
        if(player!= null && player.active == false && isGameOver == false){
            isGameOver = true;
            EndGame();
        }

        if(isGameOver == true && Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(isGameOver == true && Input.GetKeyDown(KeyCode.M)){
            SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }

        
    }

    void EndGame(){
        Instantiate(gameOverScreen);
        CancelInvoke("UpdateScoreMultiplier");
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
