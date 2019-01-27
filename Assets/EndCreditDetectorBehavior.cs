using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCreditDetectorBehavior : MonoBehaviour
{

    public string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Reached end of CutScene");
        if(collision.gameObject.CompareTag("EndCutScene"))
        {
            //Go back home!:
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }
}
