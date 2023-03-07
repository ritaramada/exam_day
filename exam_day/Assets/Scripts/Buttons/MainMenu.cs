using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public enum Scenes {MAIN_MENU, GAME };

    // Start is called before the first frame update
    public void PlayGame(){
        SceneManager.LoadScene((int)Scenes.GAME);
    }

    public void Menu(){
        SceneManager.LoadScene((int)Scenes.MAIN_MENU);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
