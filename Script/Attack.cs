using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject me;
    SpriteRenderer Attack_pt;
    public Transform place;
    float colour = 1f;
    // Start is called before the first frame update
    void Start()
    {
        place = GameObject.Find("attackshow").transform;
        transform.localScale = new Vector2(transform.localScale.x * PlayerController.lookat,transform.localScale.y);
        transform.position = place.position;
        Attack_pt = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack_pt.color = new Color(0,0,0,colour);
        colour -= 0.01f;
        if(colour <= 0)
        {
            Destroy(me);
        }
    }
}
