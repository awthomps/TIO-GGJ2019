using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CutSceneTwoFrameBehavior : MonoBehaviour
{
    public float secondsDelay = 5.0f;
    public bool loadNextScene = false;
    public string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PerformAction());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PerformAction()
    {
        yield return new WaitForSeconds(secondsDelay);
        if(loadNextScene)
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        } else
        {
            gameObject.transform.Translate(new Vector3(0.0f, 0.0f, 50.0f));
        }
    }

    private void FixedUpdate()
    {
        if(loadNextScene)
        {
            float jumpValue = Input.GetAxis("Jump");
            if (jumpValue > 0.0f)
            {
                // Skip cutscene
                SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
            }
        }
    }
}
