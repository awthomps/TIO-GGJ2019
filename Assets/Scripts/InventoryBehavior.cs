using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehavior : MonoBehaviour
{
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        GUI.backgroundColor = Color.yellow;
        canvas = this.GetComponent<Canvas>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleCanvas();
        }
    }

    void ToggleCanvas ()
    {
        canvas.enabled = !canvas.enabled;
        if (canvas.enabled)
            Camera.main.cullingMask = 1 << LayerMask.NameToLayer("Menu");
        else
            Camera.main.cullingMask = 1 << LayerMask.NameToLayer("Default");
    }

}
