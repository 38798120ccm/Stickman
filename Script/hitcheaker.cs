using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitcheaker : MonoBehaviour
{
    public GameObject AI;
    BoxCollider2D hit_box;
    enemy AI_acting;
    // Start is called before the first frame update
    void Start()
    {
        AI_acting = AI.GetComponent<enemy>();
        hit_box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AI_acting.attacking)
        {
            hit_box.enabled = true;
        }
        else
        {
            hit_box.enabled = false;
        }
    }
}
