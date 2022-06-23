using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isEnemyDead : MonoBehaviour
{
    private SkeletonScript skeleton;
    private AudioSource audioSource;
    public List<GameObject> skeleList = new List<GameObject>();
    public Animator doorAnim;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void KilledOpponent(GameObject opponent)
    {
        if (skeleList.Contains(opponent))
        {
            skeleList.Remove(opponent);
        }
    }
    // Update is called once per frame
    void Update()
    {
      Debug.Log(skeleList.Count);
      if(skeleList.Count == 0)
        {
            Destroy(gameObject);
            doorAnim.SetTrigger("WallMove");
            audioSource.Play();
        }
       
    }
}
