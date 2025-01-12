using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2 : MonoBehaviour
{
    float player;
    public GameObject hitcheaker;
    public GameObject me;
    public LayerMask cheak;
    Vector2 direction;
    Animator skill_2_ac;
    Rigidbody2D me_rb;
    float live_time = 5;
    float looking;
    bool enemyhere;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform.localScale.x;
        if(player >= 0.6)
        {
            looking = 1f;
        }
        if (player <= -0.6)
        {
            looking = -1f;
        }
        me_rb = GetComponent<Rigidbody2D>();
        skill_2_ac = GetComponent<Animator>();
        skill_2_ac.SetBool("Attacking", true);
        direction = new Vector2(looking * 20f, me_rb.velocity.y);
        transform.localScale = new Vector2(looking * -0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        enemyhere = Physics2D.OverlapCircle(hitcheaker.transform.position, 0.1f, cheak);
        me_rb.velocity = direction;
        live_time -= Time.deltaTime;
        if(live_time <= 0 || enemyhere)
        {
            Destroy(me);
        }
    }
}
