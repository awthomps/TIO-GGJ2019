using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureBehavior : MonoBehaviour
{
    public UnityEngine.UI.Button addButton;
    public UnityEngine.UI.Button removeButton;
    private GameObject placedObject;

    public GameObject Inventory;

    // Start is called before the first frame update
    void Start()
    {
        addButton.onClick.AddListener(AddToHouse);
        removeButton.onClick.AddListener(RemoveFromHouse);
        addButton.enabled = true;
        removeButton.enabled = false;
    }

    void AddToHouse()
    {
        placedObject = new GameObject();
        placedObject.name = this.name + "_IN_HOME";
        placedObject.AddComponent<Canvas>();
        placedObject.AddComponent<UnityEngine.UI.Image>();
        Sprite currSprite = this.GetComponent<UnityEngine.UI.Image>().sprite;
        placedObject.GetComponent<UnityEngine.UI.Image>().sprite = currSprite;
        RectTransform currRT = this.GetComponent<RectTransform>();
        RectTransform newRT = placedObject.GetComponent<RectTransform>();
        newRT.SetParent(GameObject.Find("FurnitureCanvas").transform);
        float adjustWidth = currRT.rect.width / newRT.rect.width;
        float adjustHeight = currRT.rect.height / newRT.rect.height;
        newRT.sizeDelta = new Vector2(2.0f, 2.0f);
        placedObject.transform.localScale = new Vector2(adjustWidth, adjustHeight);
        newRT.Translate(-1 * newRT.position);
        addButton.enabled = false;
        removeButton.enabled = true;
        Inventory.SendMessage("ToggleCanvas");
    }

    void RemoveFromHouse()
    {
        Object.Destroy(placedObject);
        addButton.enabled = true;
        removeButton.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
