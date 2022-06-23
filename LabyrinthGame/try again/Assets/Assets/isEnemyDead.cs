using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isEnemyDead : MonoBehaviour
{
    private SkeletonScript skeleton;
    // Start is called before the first frame update
    void Start()
    {
        skeleton = GameObject.Find("SKELETON").GetComponent<SkeletonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!skeleton.isAlive)
        {
            Destroy(gameObject);
        }
    }
}
