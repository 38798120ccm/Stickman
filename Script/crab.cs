using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crab : MonoBehaviour
{
    float player;
    float starttime = 2.5f;
    public GameObject Player;
    public GameObject me;
    public GameObject Explosion;
    Rigidbody2D Crab_rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform.localScale.x;
        Crab_rb = GetComponent<Rigidbody2D>();
        if(player >= 0.6)
        {
            Crab_rb.AddForce(new Vector2 (50000f,25000f));
        }
        if(player <= -0.6)
        {
            Crab_rb.AddForce(new Vector2(-50000f, 25000f));
        }

    }

    // Update is called once per frame
    void Update()
    { 
        starttime -= Time.deltaTime;
        if (starttime <= 0)
        {
            explode();
        }
    }
    void explode()
    {
        Vector2 Explosion_pt = new Vector2(transform.position.x, transform.position.y - 1);
        Destroy(me);
        Instantiate(Explosion,Explosion_pt, Explosion.transform.rotation);
    }
}
