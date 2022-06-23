using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class yellowKeyTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    playerInventory pi;
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.Find("Player").GetComponent<playerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            pi.hasYellowKey = true;
            audio.Play();
            Destroy(gameObject);
            SceneManager.LoadScene("lvl2");
        }
    }
}
