using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    private float smooth_speed = 6f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 offset = transform.position;
        Vector3 Player_Position = new Vector3(Player.transform.position.x, Player.transform.position.y, -15);
        transform.position = Vector3.Lerp(offset,Player_Position, Time.deltaTime * smooth_speed);
    }
}
