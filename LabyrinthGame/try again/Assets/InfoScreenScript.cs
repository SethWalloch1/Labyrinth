using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoScreenScript : MonoBehaviour{

	public static bool isPaused;
	public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
    	isPaused = false;  
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
        }
    }

    public void Resume(){
    	isPaused = false;
    	pauseMenu.SetActive(false);
    	Time.timeScale = 1f;
    }

    void Pause(){
    	isPaused = true;
    	pauseMenu.SetActive(true);
    	Time.timeScale = 0f;
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
