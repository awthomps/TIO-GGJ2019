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

    private void FixedUpdate()
    {
        float jumpValue = Input.GetAxis("Jump");
        if (jumpValue > 0.0f)
        {
            // Skip cutscene
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Reached end of CutScene");
        if(collision.gameObject.CompareTag("EndCutScene"))
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }
}
