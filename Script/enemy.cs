using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float HP = 100;
    public float defend;
    public Animator enemy_ac;
    public Aihp AI_hp;
    public Rigidbody2D enemy_rb;
    public GameObject deaditem;
    public GameObject me;
    public GameObject Playercheaker;
    public GameObject Wallscheaker;
    public GameObject Groundcheaker;
    public GameObject hitcheaker;
    public LayerMask player_lm;
    public LayerMask ground;
    PlayerController player_pc;
    GameObject player;
    bool playerhere;
    bool onground;
    bool wallhere;
    int lookat = -1;
    int jumpnum = 1;
    int drop_money = 100;
    public bool attacking;
    bool canattack = true;
    bool canjump = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player_pc = player.GetComponent<PlayerController>();
        enemy_ac = GetComponent<Animator>();
        enemy_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Dead();
        wallhere = Physics2D.OverlapBox(Wallscheaker.transform.position, new Vector2(3f, 0.5f), 180f, ground);
        onground = Physics2D.OverlapCircle(Groundcheaker.transform.position,0.5f,ground);
        playerhere = Physics2D.OverlapCircle(Playercheaker.transform.position,30,player_lm);
        jump();
        if (playerhere)
        {
            if (canattack)
            {
                StartCoroutine(Attack());
            }
        }
        transform.localScale = new Vector3(-lookat, 1, 1);
        if(enemy_rb.velocity.x != 0)
        {
            enemy_ac.SetBool("Attacking", true);
        }
        else
        {
            enemy_ac.SetBool("Attacking", false);
        }
    }
    IEnumerator Attack()
    {
        jumpnum = 1;
        canattack = false;
        if (player.transform.position.x - transform.position.x >= 1)
        {
            lookat = 1;
        }
        else
        {
            lookat = -1;
        }
        canjump = true;
        yield return new WaitForSeconds(0.5f);
        enemy_rb.AddForce(Vector2.right * lookat * 180000f);
        attacking = true;
        yield return new WaitForSeconds(0.7f);
        canjump = false;
        attacking = false;
        yield return new WaitForSeconds(Random.Range(2f,3f));
        canattack = true;
    }
    public void jump()
    {
        if (wallhere)
        {
            if (onground && canjump)
            {
                if (jumpnum == 1)
                {
                        enemy_rb.AddForce(Vector2.up * 120000f);
                        jumpnum -= 1;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (collision.gameObject.name == "Player_Attack_1(Clone)")
            {
                AI_hp.AITakeDamage(player_pc.attack_power / defend);
            }
            if (collision.gameObject.name == "Player_Attack_2(Clone)")
            {
                AI_hp.AITakeDamage((player_pc.attack_power * 2) / defend);
            }
            if (collision.gameObject.name == "Player_Attack_3(Clone)")
            {
                AI_hp.AITakeDamage((player_pc.attack_power * 4) / defend);
            }
            if (collision.gameObject.name == "Player_JumpAttack(Clone)")
            {
                AI_hp.AITakeDamage((player_pc.attack_power * 3.5f) / defend);
            }
            if (collision.gameObject.tag == "Explosion")
            {
                AI_hp.AITakeDamage(35 / defend);
            }
        }
        if (collision.gameObject.name == "Skill_2_hitbox")
        {
            AI_hp.AITakeDamage(35 / defend);
        }
    }
    void Dead()
    {
        if (HP <= 0)
        {
            PlayerState.GetMoney(drop_money);
            Instantiate(deaditem, transform.position, deaditem.transform.rotation);
            Destroy(me);
        }
        if (transform.position.y < -50)
        {
            Destroy(me);
        }
    }
}
