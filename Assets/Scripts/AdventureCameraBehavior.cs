using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureCameraBehavior : MonoBehaviour
{
    // Public Fields
    public GameObject subject;
    public float cameraDistance = -10.0f;
    public float yOffset = 1.0f;
    public float xOffset = 7.0f;
    public bool zoomiesEnabled = true;
    public float zoomyRange = 20.0f;
    public GameObject zoomyRenderer;
    public float zoomySpeed = 300.0f;

    public Sprite zoomy0;
    public Sprite zoomy1;
    public Sprite zoomy2;
    public Sprite zoomy3;
    public Sprite zoomy4;

    private Sprite[] zoomies;
    private SpriteRenderer sr;
    private int currentZoomy = 0;


    // Start is called before the first frame update
    void Start()
    {
        if(zoomiesEnabled)
        {
            Rigidbody2D rb = zoomyRenderer.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(-zoomySpeed, 0.0f));
            sr = zoomyRenderer.GetComponent<SpriteRenderer>();
            zoomies = new Sprite[5];
            zoomies[0] = zoomy0;
            zoomies[1] = zoomy1;
            zoomies[2] = zoomy2;
            zoomies[3] = zoomy3;
            zoomies[4] = zoomy4;
            sr.sprite = zoomies[currentZoomy];
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Follow Uchi Around
        float x = subject.transform.position.x + xOffset;
        float y = subject.transform.position.y + yOffset;
        float z = cameraDistance;
        transform.position = new Vector3(x, y, z);
        // Debug.Log(transform.position);
    }

    private void FixedUpdate()
    {
        if (zoomiesEnabled)
        {
            Transform zoomyTransform = zoomyRenderer.GetComponent<Transform>();
            if(transform.position.x - zoomyTransform.position.x > zoomyRange)
            {
                // Reset and use next zoomy
                zoomyTransform.position = new Vector3(transform.position.x + zoomyRange, zoomyTransform.position.y, zoomyTransform.position.z);
                currentZoomy++;
                currentZoomy %= zoomies.Length;
                sr.sprite = zoomies[currentZoomy];
            }
        }
    }
}
