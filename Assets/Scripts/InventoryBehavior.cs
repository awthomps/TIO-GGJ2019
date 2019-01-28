using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehavior : MonoBehaviour
{
    /* Constants */
    // four items from each of the four categories
    private readonly int NumItems = 8;

    /* Objects */
    // the first item (a prefab)
    public GameObject prefab;
    // the items visible in the inventory
    private GameObject[] inventory;
    // storing all the sprites
    private Sprite[] sprites;
    // the canvas on which the items are drawn
    private Canvas canvas;

    /* Resources */
    // item0 is the prefab we'll be making copies of
    public Texture2D item1;
    public Texture2D item2;
    public Texture2D item3;
    public Texture2D item4;
    public Texture2D item5;
    public Texture2D item6;
    public Texture2D item7;

    public string name0, name1, name2, name3, name4, name5, name6;

    // Start is called before the first frame update
    void Start()
    {
        canvas = this.GetComponent<Canvas>();
        canvas.enabled = false;
        CreateSprites();
        CreateInventory();
    }

    void CreateSprites()
    {
        sprites = new Sprite[NumItems - 1];
        Sprite newSprite;
        Rect newRect = new Rect(0.0f, 0.0f, item1.width, item1.height);
        sprites[0] = Sprite.Create(item1, newRect, new Vector2(0.5f, 0.5f));
        sprites[1] = Sprite.Create(item2, newRect, new Vector2(0.5f, 0.5f));
        sprites[2] = Sprite.Create(item3, newRect, new Vector2(0.5f, 0.5f));
        sprites[3] = Sprite.Create(item4, newRect, new Vector2(0.5f, 0.5f));
        sprites[4] = Sprite.Create(item5, newRect, new Vector2(0.5f, 0.5f));
        sprites[5] = Sprite.Create(item6, newRect, new Vector2(0.5f, 0.5f));
        sprites[6] = Sprite.Create(item7, newRect, new Vector2(0.5f, 0.5f));
        sprites[0].name = name0;
        sprites[1].name = name1;
        sprites[2].name = name2;
        sprites[3].name = name3;
        sprites[4].name = name4;
        sprites[5].name = name5;
        sprites[6].name = name6;
    }

    void CreateInventory()
    {
        inventory = new GameObject[NumItems];
        inventory[0] = prefab;
        inventory[0].name = "Inventory_0";

        Vector3 initPos = prefab.transform.position;

        for (int i = 1; i < NumItems; i++)
        {
            inventory[i] = GameObject.Instantiate(prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            inventory[i].GetComponent<UnityEngine.UI.Image>().sprite = sprites[i - 1];
            // must set the canvas as the parent of each object so that the images will show
            inventory[i].transform.SetParent(prefab.GetComponentInParent<Canvas>().transform);
            // set the scale back to the prefab's scale because SetParent alters it
            inventory[i].transform.localScale = prefab.transform.localScale;
            // name the object
            inventory[i].name = "Inventory_" + i;
            // move to it's position in our grid of objects
            inventory[i].transform.position = initPos + new Vector3((float)i * 2.0f, 0.0f, 0.0f);
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
