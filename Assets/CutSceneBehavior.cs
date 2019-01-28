using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneBehavior : MonoBehaviour
{
    public int scrollForce = 60;

    public AudioSource CutSceneTheme;

    // Start is called before the first frame update
    void Start()
    {
        CutSceneTheme.loop = true;
        CutSceneTheme.Play();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, scrollForce));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
