﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureBehavior : MonoBehaviour
{
    public UnityEngine.UI.Button addButton;
    public UnityEngine.UI.Button removeButton;
    private GameObject furniture;

    public GameObject Inventory;

    private bool placing;

    // Start is called before the first frame update
    void Start()
    {
        addButton.onClick.AddListener(AddToHouse);
        removeButton.onClick.AddListener(RemoveFromHouse);
        placing = false;
        // these colors will be used to control clickability (also see inventory behavior)
        SetButtonColors(Color.green, Color.red);
    }

    void AddToHouse()
    {
        // actually create the furniture object
        CreateFurniture();
        // update the button colors
        SetButtonColors(Color.red, Color.green);
        // hide the inventory canvas and toggle its state
        Inventory.SendMessage("ToggleCanvas");
    }

    void CreateFurniture ()
    {
        // create a new game object and give it a canvas so that it can have an image
        furniture = new GameObject();
        furniture.name = this.name + "_IN_HOME";
        furniture.AddComponent<Canvas>();
        // add the image component and give it the same sprite as this inventory item
        furniture.AddComponent<UnityEngine.UI.Image>();
        Sprite currSprite = this.GetComponent<UnityEngine.UI.Image>().sprite;
        furniture.GetComponent<UnityEngine.UI.Image>().sprite = currSprite;
        // add a collider
        furniture.AddComponent<BoxCollider2D>();
        // get the inventory item's rect transform and the new object's rect transform
        RectTransform currRT = this.GetComponent<RectTransform>();
        RectTransform newRT = furniture.GetComponent<RectTransform>();
        // assign a parent so that the new object is in the correct hierarchy position
        newRT.SetParent(GameObject.Find("FurnitureCanvas").transform);
        // adjust the width, height, and size delta to make sure the furniture fits properly
        float adjustWidth = currRT.rect.width / newRT.rect.width;
        float adjustHeight = currRT.rect.height / newRT.rect.height;
        newRT.sizeDelta = new Vector2(2.0f, 2.0f);
        furniture.transform.localScale = new Vector2(adjustWidth, adjustHeight);
        // move the object by the negative of its current position in order to center it
        newRT.Translate(-1 * newRT.position);
        // move the object to where the mouse currently is
        FollowMouse();
        // until we have decided where the object should be, we are in the process of placing it
        placing = true;
    }


    void RemoveFromHouse()
    {
        Object.Destroy(furniture);
        SetButtonColors(Color.green, Color.red);
    }

    void SetButtonColors (Color addColor, Color removeColor)
    {
        addButton.GetComponent<UnityEngine.UI.Image>().color = addColor;
        removeButton.GetComponent<UnityEngine.UI.Image>().color = removeColor;
    }

    Ray GetCamToMouse ()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    void FollowMouse ()
    {
        Ray ray = GetCamToMouse();
        RectTransform rt = furniture.GetComponent<RectTransform>();
        Vector3 translation = new Vector3(ray.origin.x, ray.origin.y, 0.0f) - rt.position;
        rt.Translate(translation);
    }

    bool CanPlace ()
    {
        // public static int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, Collider2D[] results);
        RectTransform rt = furniture.GetComponent<RectTransform>();
        Vector2 size = new Vector2(rt.rect.width, rt.rect.height);
        float radius = (size.x / 2.0f);
        if (size.x < size.y) { radius = (size.y / 2.0f);  }
        LayerMask layerMask = ~(1 << 2);
        furniture.layer = 2;
        Vector3 start = new Vector3(furniture.transform.position.x - radius, furniture.transform.position.y - radius, 0.0f);
        Vector3 end = new Vector3(furniture.transform.position.x + radius, furniture.transform.position.y + radius, 0.0f);
        Debug.DrawLine(start, end, Color.red, 1000);
        Collider2D overlap = Physics2D.OverlapCircle(furniture.transform.position, radius, layerMask);
        furniture.layer = 0;
        if (overlap)
            return false;
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (placing)
        {
            FollowMouse();
        }

        // 0 corresponds to the main mouse button
        if (furniture && Input.GetMouseButtonDown(0))
        {
            if (CanPlace())
            {
                placing = false;
            }
            else
                Debug.Log("You can't place the object there!");
        }
    }
}
