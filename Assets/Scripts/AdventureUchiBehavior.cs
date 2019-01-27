using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureUchiBehavior : MonoBehaviour
{
    public float speed;
    public float jumpMagnitude;
    private Rigidbody2D rb;
    private int groundContact;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundContact = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * speed;
        float jumpValue = Input.GetAxis("Jump");
        float moveVertical = 0.0f;
        if (jumpValue > 0.0f && groundContact > 0)
        {
            moveVertical = jumpMagnitude;
            Debug.Log(jumpMagnitude);
            groundContact = 0;
        }


        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.AddForce(movement);
    }

    void OnCollisionEnter2D(Collision2D collided)
    {
        if (collided.gameObject.CompareTag("Ground"))
        {
            Debug.Log(collided.gameObject.tag);
            groundContact++;
        }
    }

    void OnCollisionExit2D(Collision2D collided)
    {
        if (collided.gameObject.CompareTag("Ground") && groundContact > 0)
        {
            Debug.Log(collided.gameObject.tag);
            groundContact++;
        }
    }
}
