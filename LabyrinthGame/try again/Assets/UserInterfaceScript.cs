using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterfaceScript : MonoBehaviour
{
	public GameObject redKey;
    public GameObject yellowKey;
    public static bool hasYellowKey;
    public static bool hasRedKey;
    // Start is called before the first frame update
    void Start()
    {
    	hasRedKey = false;
    	hasYellowKey = false;   
    }

    // Update is called once per frame
    void Update()
    {
        //Replace with if(hasRedKey) and just set that true whenever the player 
        //picks up that color key
    	if(Input.GetKeyDown(KeyCode.Y)){
    		hasYellowKey = true;
    		yellowKey.SetActive(true);
    	}

    	if(Input.GetKeyDown(KeyCode.R)){
    		hasRedKey = true;
    		redKey.SetActive(true);
    	}
        
    }
}
