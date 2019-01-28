using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{

    public bool isGirl = false;
    public Sprite boySprite;
    public Sprite girlSprite;
    public string introCutscene = "IntroCutscene";

    // Start is called before the first frame update
    void Start()
    {
        // Clear all data on new game
        PlayerPrefs.DeleteAll();

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(isGirl)
        {
            // Flip Girl Sprite
            sr.sprite = girlSprite;
            transform.localScale = new Vector3(-transform.localScale.y, transform.localScale.y, transform.localScale.z);
        } else
        {
            sr.sprite = boySprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        if(moveHorizontal != 0.0f)
        {
            string gender = "boy";
            if(moveHorizontal < 0.0f && isGirl)
            {
                gender = "girl";
            }
            PlayerPrefs.SetString("Gender", gender);


            SceneManager.LoadScene(introCutscene, LoadSceneMode.Single);
        }

    }
}
