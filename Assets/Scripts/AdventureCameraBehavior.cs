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

    // Start is called before the first frame update
    void Start()
    {
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
}
