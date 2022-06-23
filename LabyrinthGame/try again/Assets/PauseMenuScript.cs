using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
	public static bool isPaused;
    public static bool isInfoPaused;
	public GameObject pauseMenu;
    public GameObject infoScreen;
    public GameObject GameOverScreen;
    public string levelName;

    playerInventory Player;

    // Start is called before the first frame update
    void Start()
    {
    	isPaused = false;
        levelName = SceneManager.GetActiveScene().name;
        isInfoPaused = false;  
        Player = FindObjectOfType<playerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
        	if(isPaused == true){
        		Resume();
        	}else{
        		Pause();
        	}
        }else if(Input.GetKeyDown(KeyCode.I)){
            if(isInfoPaused == true){
                InfoResume();
            }else{
                InfoPause();
            }
        }else if(Player.health < 0){
            GameOverScreen.SetActive(true);
            if(Input.GetKeyDown(KeyCode.R)){
                SceneManager.LoadScene(levelName);
            }
        }
    }

    public void Resume(){
    	isPaused = false;
    	pauseMenu.SetActive(false);
    	Time.timeScale = 1f;
    }

    public void Pause(){
    	isPaused = true;
    	pauseMenu.SetActive(true);
    	Time.timeScale = 0f;
    }

    public void InfoPause(){
        isInfoPaused = true;
        infoScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void InfoResume(){
        isInfoPaused = false;
        infoScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BringUpInfoScreenFromPauseScreen(){
        pauseMenu.SetActive(false);
        infoScreen.SetActive(true);
    }

    public void BringUpPauseScreenFromInfoScreen(){
        pauseMenu.SetActive(true);
        infoScreen.SetActive(false);
    }

    public void MainMenu(){
    	SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
