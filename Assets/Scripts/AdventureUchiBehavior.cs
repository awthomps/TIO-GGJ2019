using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdventureUchiBehavior : MonoBehaviour
{
    // Public Fields
    public float speed = 30.0f;
    public float maxSpeed = 10.0f;
    public float tooFar = -50.0f;
    public float jumpMagnitude = 600.0f;
    public string homeSceneName;
    

    // Private Fields
    private Rigidbody2D rb;
    private Transform trans;
    private bool groundContact;
    private float restartY;
    private int collectionScore;


    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        restartY = 10.0f;
        groundContact = true;
        collectionScore = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //Fix Uchi's Position? 
        if(!didUchiFallTooFar())
        {
            // Determine terminal velocity
            float currentVx = rb.velocity.x;
            float moveHorizontal = currentVx < maxSpeed ? speed : 0.0f;

            // Apply Jump Force:
            float jumpValue = Input.GetAxis("Jump");
            float moveVertical = 0.0f;
            // Can Uchi Jump?
            if (jumpValue > 0.0f && groundContact)
            {
                moveVertical = jumpMagnitude;
                // Debug.Log(jumpMagnitude);
                groundContact = false;

            }
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            rb.AddForce(movement);
        }        
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        
        // Deterimne if Uchi has landed
        if (collider.gameObject.CompareTag("Ground") && !groundContact)
        {
            // Debug.Log(collider.gameObject.tag);
            groundContact = true;
        }
        //Debug.Log("Collision Occured," + collider.gameObject.tag + " is " + (groundContact ? "in contact." : "not in contact."));
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        
        // Determine if ground has been left
        if (collider.gameObject.CompareTag("Ground") && groundContact)
        {
            // Debug.Log(collider.gameObject.tag);
            groundContact = false;
        }
        // Debug.Log("Collision finished," + collider.gameObject.tag + " is " + (groundContact ? "in contact." : "not in contact."));
    }

    bool didUchiFallTooFar()
    {
        // Debug.Log(trans.position);
        if(trans.position.y < tooFar)
        {
            // Uchi fell too far
            // Set Uchi back to y = 0.0f for now
            trans.position = new Vector3(trans.position.x, restartY, trans.position.y);
            rb.velocity = new Vector2(0.0f, 0.0f);


            // LOSE CONDITION
            Scene scene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(scene.name, LoadSceneMode.Single);



            return true;
        }
        else
        {
            return false;
        }
    }


    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Collectable"))
        {
            collectionScore++;
            Destroy(collider.gameObject);
            // Debug.Log(collectionScore);
        }

        didUchiReachTheEnd(collider.gameObject);
    }

    void didUchiReachTheEnd(GameObject testObject)
    {
        //Perform Check for level completion
        if (testObject.CompareTag("LevelEnd"))
        {
            Debug.Log("You finished the level!");
            int money = PlayerPrefs.GetInt("Money");
            money += collectionScore;
            PlayerPrefs.SetInt("Money", money);
            // TODO: display level finish

            //Go back home!:
            SceneManager.LoadScene(homeSceneName, LoadSceneMode.Single);

        }

    }
}
