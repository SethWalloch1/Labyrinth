using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool hasKey = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        //Had to switch the horizontal and vertical input due to camera issues
        Vector3 movement = new Vector3(moveVertical  * -1.0f, 0f, moveHorizontal);
        transform.Translate(movement *Time.deltaTime * moveSpeed);
        //To prevent the capsule from falling over when it collides with something
        transform.rotation = Quaternion.identity;
    }
    public void giveKey() {
        hasKey = true;
    }

}
