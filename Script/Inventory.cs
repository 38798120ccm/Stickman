using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    static Image item_pic;
    // Start is called before the first frame update
    void Start()
    {
        item_pic = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void itemshow(Sprite pictrue)
    {
        item_pic.sprite = pictrue;
    }
}
