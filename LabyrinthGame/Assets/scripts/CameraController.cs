using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float rotationSpeed = 1;
    public Transform Target, Player;
    float mouseX;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CamControl();
    }
    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        transform.LookAt(Target);
        Target.rotation = Quaternion.Euler(0, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
