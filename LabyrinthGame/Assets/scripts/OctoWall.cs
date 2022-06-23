using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoWall : MonoBehaviour
{
    public int leversPulled = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leversPulled == 2)
            Destroy(gameObject);
    }
}
