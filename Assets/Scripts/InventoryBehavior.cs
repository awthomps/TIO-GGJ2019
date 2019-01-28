using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehavior : MonoBehaviour
{
    /* Constants */
    // four items from each of the four categories
    private readonly int NumItems = 16;

    /* Objects */
    // the first item (a prefab)
    public GameObject prefab;
    // the items visible in the inventory
    private GameObject[] inventory;
    // the canvas on which the items are drawn
    private Canvas canvas;

    /* Resources */
    // item0 is the prefab we'll be making copies of
    public Texture2D item1;
    public Texture2D item2;
    public Texture2D item3;
    //public Texture2D item4;
    /*public Texture2D item5;
    public Texture2D item6;
    public Texture2D item7;
    public Texture2D item8;
    public Texture2D item9;
    public Texture2D item10;
    public Texture2D item11;
    public Texture2D item12;
    public Texture2D item13;
    public Texture2D item14;
    public Texture2D item15;*/

    // Start is called before the first frame update
    void Start()
    {
        canvas = this.GetComponent<Canvas>();
        canvas.enabled = false;
        CreateInventory();
    }

    void CreateInventory()
    {
        inventory = new GameObject[NumItems];
        inventory[0] = prefab;
        inventory[0].name = "Inventory_0";

        Vector3 initPos = prefab.transform.position;
        int counter = 1;

        Rect newRect = new Rect(0.0f, 0.0f, item1.width, item1.height);
        Sprite newSprite = Sprite.Create(item1, newRect, new Vector2(0.5f, 0.5f));

        for (int i = 1; i < NumItems; i++)
        {
            inventory[i] = GameObject.Instantiate(prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            // must set the canvas as the parent of each object so that the images will show
            inventory[i].transform.SetParent(prefab.GetComponentInParent<Canvas>().transform);
            // set the scale back to the prefab's scale because SetParent alters it
            inventory[i].transform.localScale = prefab.transform.localScale;
            // name the object
            inventory[i].name = "Inventory_" + i;
            // move to it's position in our grid of objects
            inventory[i].transform.position = initPos + new Vector3((float)counter * 2.0f, 0.0f, 0.0f);
            counter++;

            if (counter % 2 == 0)
            {
                inventory[i].GetComponent<UnityEngine.UI.Image>().sprite = newSprite;
            }
            if (i == NumItems/2)
            {
                initPos = initPos + new Vector3(0.0f, -3.0f, 0.0f);
                counter = 0;
            }
        }
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
        {
            Camera.main.cullingMask = 1 << LayerMask.NameToLayer("Menu");
            EnableAllButtons();
        }
        else
        {
            Camera.main.cullingMask = 1 << LayerMask.NameToLayer("Default");
            DisableAllButtons();
        }
    }

    void EnableAllButtons ()
    {
        SetButtonStatus(true);
    }

    void DisableAllButtons ()
    {
        SetButtonStatus(false);
    }

    void SetButtonStatus (bool status)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            UnityEngine.UI.Button[] buttons = inventory[i].GetComponentsInChildren<UnityEngine.UI.Button>();
            for (int j = 0; j < buttons.Length; j++)
            {
                buttons[j].enabled = status;
                // even if we want buttons to be enabled, we still want to turn off the red ones
                if (status == true)
                {
                    Color color = buttons[j].GetComponent<UnityEngine.UI.Image>().color;
                    if (color.Equals(Color.red)) { buttons[j].enabled = false; }
                }

            }
        }
    }

}
