using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UchiBehavior : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    public string LevelOneSceneName;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Start the level associated with the entry portal you just entered
        // for now, just start level one
        if (otherCollider.gameObject.CompareTag("LevelStart"))
            SceneManager.LoadScene(LevelOneSceneName, LoadSceneMode.Single);
    }
}
