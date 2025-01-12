using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objects_Playeruse : MonoBehaviour
{
    public float attack_power;
    public float speed;
    public float hp;
    public GameObject me;
    public GameObject sword_1;
    public Transform Player_tf;
    GameObject player;
    PlayerController player_pc;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player_pc = player.GetComponent<PlayerController>();
        transform.localScale = new Vector3(transform.localScale.x * PlayerController.lookat,transform.localScale.y);
        player_pc.attack_power = player_pc.attack_power + attack_power;
        PlayerController.canhold = false;
        PlayerController.Using = true;
        PlayerController.holding = false;
    }

    // Update is called once per frame
    void Update()
    {
        player_pc.speed = player_pc.speed + speed;
        if (PlayerController.holding)
        {
            Player_tf = GameObject.Find("holding").transform;
            transform.localRotation = Quaternion.Euler(0, 0, 360);
            transform.position = Player_tf.position;
            me.transform.SetParent(Player_tf);
        }
        else
        {
            Player_tf = GameObject.Find("unholding").transform;
            transform.localRotation = Quaternion.Euler(0, 0, -340);
            transform.position = Player_tf.position;
            me.transform.SetParent(Player_tf);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(sword_1,player.transform.position,sword_1.transform.rotation);
            player_pc.attack_power = player_pc.attack_power - attack_power;
            player_pc.speed = player_pc.speed - speed;
            PlayerController.player_hp = PlayerController.player_hp + hp;
            Destroy(me);
            PlayerController.Using = false;
            PlayerController.canhold = true;
            PlayerController.holding = false;
        }
    }
}
