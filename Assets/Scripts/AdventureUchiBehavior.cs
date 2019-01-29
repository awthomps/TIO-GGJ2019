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

    public AudioSource AdventureOneTheme;
    public AudioSource CoinPickupSound;
    public AudioSource VictoryDoorSound;

    // Private Fields
    private Rigidbody2D rb;
    private Transform trans;
    private bool groundContact;
    private float restartY;
    private int yellowCoins;
    private int greenCoins;
    private int blueCoins;
    private int pinkCoins;

    // Start is called before the first frame update
    void Start()
    {
        AdventureOneTheme.loop = true;
        AdventureOneTheme.Play();
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        restartY = 10.0f;
        groundContact = true;
        yellowCoins = 0;
        greenCoins = 0;
        blueCoins = 0;
        pinkCoins = 0;
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
            //Go back home!:
            SceneManager.LoadScene(homeSceneName, LoadSceneMode.Single);

            return true;
        }
        else
        {
            return false;
        }
    }


    
    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject colliderObject = collider.gameObject;
        if (colliderObject.CompareTag("Collectable"))
        {
            CoinPickupSound.Play();
            if (colliderObject.name.Contains("YellowCoin"))
                yellowCoins++;
            else if (colliderObject.name.Contains("GreenCoin"))
                greenCoins++;
            else if (colliderObject.name.Contains("BlueCoin"))
                blueCoins++;
            else if (colliderObject.name.Contains("PinkCoin"))
                pinkCoins++;
            
            Destroy(collider.gameObject);
        }

        didUchiReachTheEnd(collider.gameObject);
    }

    void didUchiReachTheEnd(GameObject testObject)
    {
        //Perform Check for level completion
        if (testObject.CompareTag("LevelEnd"))
        {
            VictoryDoorSound.Play();
            // Debug.Log("You finished the level!");
            int storeYellowCoins = PlayerPrefs.GetInt("YellowCoins") + yellowCoins;
            int storeGreenCoins = PlayerPrefs.GetInt("GreenCoins") + greenCoins;
            int storeBlueCoins = PlayerPrefs.GetInt("BlueCoins") + blueCoins;
            int storePinkCoins = PlayerPrefs.GetInt("PinkCoins") + pinkCoins;
            
            PlayerPrefs.SetInt("YellowCoins", storeYellowCoins);
            PlayerPrefs.SetInt("GreenCoins", storeGreenCoins);
            PlayerPrefs.SetInt("BlueCoins", storeBlueCoins);
            PlayerPrefs.SetInt("PinkCoins", storePinkCoins);

            //Go back home!:
            SceneManager.LoadScene(homeSceneName, LoadSceneMode.Single);

        }

    }
}
