using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objects : MonoBehaviour
{
    public GameObject use;
    public GameObject me;
    PlayerController player_pc;

    void Start()
    {
        player_pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    internal void Player_check()
    {
        if (PlayerController.canhold)
        {
            if (use.gameObject.tag == "Use")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Instantiate(use);
                    Destroy(me);

                }
            }
        }
        if (use.gameObject.tag == "Get")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(me);
                player_pc.Prefabs = player_pc.AddSkill(use);
            }
        }

    }
}