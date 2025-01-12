using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject attack_1;
    public GameObject attack_2;
    public GameObject attack_3;
    public GameObject jumpattack;
    public GameObject me;
    public GameObject arrow;
    public GameObject[] Prefabs;
    public GameObject Skill_2;
    Collider2D objectcheck;
    public Transform enemychecker;
    public Transform Groundcheck;
    public Rigidbody2D Player_rb;
    public Animator Player_ac;
    public LayerMask player_lm;
    public LayerMask untouchable;
    public LayerMask objects;
    public LayerMask cantouch;
    public LayerMask hit_layer;
    public float Groundcheckarena;
    int itemnum = 0;
    Vector2 Playermovement;
    Vector2 spawnpoint = new Vector2(-2, 0);
    bool attack;
    bool jump;
    bool dash;
    bool dashchecing;
    bool hitcheck;
    float dash_cd = 0.5f;
    bool skill;
    bool delayclear = true;
    float skills_cd;
    bool canattack = true;
    int attacknum = 1;
    bool attacking = false;
    bool canjumpattack;
    float jumpforce = 10000f;
    bool OnGround;
    public float speed = 8f;
    public float attack_power = 0;
    public static int player_money = 0;
    public static float player_hp = 100f;
    public bool cannotcontrolle;
    public static bool holding;
    public static bool Using;
    public static bool canhold;
    public static float lookat = 1;
    // Start is called before the first frame update
    void Start()
    {
        Player_rb = GetComponent<Rigidbody2D>();
        Player_ac = GetComponent<Animator>();
        canhold = true;
    }

    // Update is called once perframe
    void Update()
    {
        skills_cd -= Time.deltaTime;
        dash_cd -= Time.deltaTime;
        float Horizontal_input = Input.GetAxisRaw("Horizontal");
        OnGround = Physics2D.OverlapCircle(Groundcheck.position, Groundcheckarena, cantouch);
        objectcheck = Physics2D.OverlapCircle(transform.position, 2, objects);
        hitcheck = Physics2D.OverlapBox(enemychecker.position, new Vector2(2, 6),0,hit_layer);
        Player_ac.SetFloat("Speed", System.Math.Abs(Horizontal_input));
        Returnspawn();
        Objectchecker();
        KeyDown();
        itemdetect();
        Dashchecking();
        if (OnGround)
        {
            Player_ac.SetBool("On_Ground", true);
            if (canjumpattack)
            {
                attacknum = 1;
            }
        }
        else
        {
            Player_ac.SetBool("On_Ground", false);
            if (canjumpattack)
            {
                attacknum = 5;
            }
        }

        if (holding)
        {
            speed = 8f;
            Player_ac.SetBool("Holding", true);
            Combos();
        }
        else
        {
            speed = 10f;
            Player_ac.SetBool("Holding", false);
        }
    }
    private void FixedUpdate()
    {
        if (!attacking && !cannotcontrolle)
        {
            Movement();
        }
        else
        {
            Playermovement = new Vector2(Player_rb.velocity.x, Player_rb.velocity.y);
        }
        if (canattack && !cannotcontrolle)
        {
            Skills();
            Combos();
            Dash();
        }
        Player_rb.velocity = Playermovement;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (delayclear)
        {
            Player_ac.SetBool("Falling", false);
            cannotcontrolle = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.position.x - transform.position.x >= 0.01)
        {
            if (collision.gameObject.tag == "Damage")
            {
                PlayerState.TakeDamage(25);
                destroyedobjecteffect(2, 0.7f, 1);
            }
        }
        if (collision.transform.position.x - transform.position.x <= -0.01)
        {
            if (collision.gameObject.tag == "Damage")
            {
                PlayerState.TakeDamage(25);
                destroyedobjecteffect(2, 0.7f, -1);
            }
        }
        if (collision.gameObject.tag == "Explosion")
        {
            destroyedobjecteffect(1, 1f,1);
            PlayerState.TakeDamage(25);
        }
    }
    void KeyDown()
    {
        if (Input.GetKeyDown(KeyCode.X) && canattack && holding && !cannotcontrolle)
        {
            attack = true;
        }
        if (Input.GetKeyDown(KeyCode.C) && canattack && dash_cd <= 0 && !cannotcontrolle)
        {
            dash = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && OnGround == true && !attacking)
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            changeitem();
        }
        if (Input.GetKeyDown(KeyCode.Z) && skills_cd <= 0&& !cannotcontrolle && !attacking)
        {
            skill = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Using)
        {
            if (!holding)
            {
                holding = true;
            }
            else
            {
                holding = false;
            }

        }
    }
    void Returnspawn()
    {
        if (transform.position.y <= -10)
        {
            transform.position = spawnpoint;
        }
    }
    void Movement()
    {
        float Horizontal_input = Input.GetAxisRaw("Horizontal");
        Playermovement = new Vector2(Horizontal_input * speed, Player_rb.velocity.y);
        changelookat();
        if (jump)
        {
            jump = false;
            Player_rb.AddForce(Vector3.up * jumpforce);
            Player_ac.SetTrigger("Jump");
            OnGround = false;
        }
    }
    void Objectchecker()
    {
        if(objectcheck != null)
        {
            if (objectcheck.gameObject.tag == "Use" || objectcheck.gameObject.tag == "Get")
            {
                arrow.transform.position = new Vector2(objectcheck.bounds.center.x, objectcheck.bounds.max.y + 0.5f);
                objectcheck.gameObject.GetComponent<objects>().Player_check();
            }
            else
            {
                arrow.transform.position = new Vector2(2046, 2046);
            }
        }
        else
        {
            arrow.transform.position = new Vector2(2046, 2046);
        }


    }
    void Dash()
    {
        if (dash)
        {
            dash = false;
            changelookat();
            canattack = false;
            Player_ac.SetTrigger("Dash");
        }
    }
    void Combos()
    {
        if (attack)
        {
            canattack = false;
            attack = false;
            changelookat();
            if (attacknum == 5)
            {
                Player_ac.SetInteger("Attacktype", attacknum);
            }
            else if ((attacknum <= 3))
            {
                Player_ac.SetInteger("Attacktype", attacknum);
            }
            Player_ac.SetTrigger("Attack");
            Player_ac.SetBool("Attacking", true);
            canjumpattack = false;
        }
    }
    public void Skill_2_Start()
    {
        Player_rb.AddForce(Vector3.up * jumpforce * 2);
    }

    void Skills()
    {
        if (skill)
        {
            skill = false;
            if (Prefabs[itemnum].name == "crab")
            {
                Vector2 spawnpoint = new Vector2(transform.position.x + lookat, transform.position.y + 2);
                Instantiate(Prefabs[itemnum], spawnpoint, Prefabs[itemnum].transform.rotation);
                skills_cd = 5f;
            }
            if(Prefabs[itemnum].name == "Skill_2")
            {
                canattack = false;
                Vector2 spawnpoint = new Vector2(transform.position.x, transform.position.y);
                Instantiate(Skill_2, spawnpoint, Skill_2.transform.rotation);
                Player_ac.SetTrigger("Skills");
                Player_ac.SetInteger("Skillstype",2);
                skills_cd = 1f;
            }
        }
    }
    void itemdetect()
    {
         Sprite pictrue = Prefabs[itemnum].GetComponent<SpriteRenderer>().sprite;
         Inventory.itemshow(pictrue);
    }
    void changeitem()
    {
        itemnum += 1;
        if (itemnum >= Prefabs.Length)
        {
            itemnum = 0;
        }
    }
    public GameObject[] AddSkill(GameObject skill)
    {
        GameObject[] newPrefabs = new GameObject[Prefabs.Length + 1];
        for (int i = 0; i <= Prefabs.Length; i++)
        {
            if(i == Prefabs.Length)
            {
                newPrefabs[i] = skill;
            }
            else
            {
                if (Prefabs[i] == skill)
                {
                    return Prefabs;
                }
                else
                {
                    newPrefabs[i] = Prefabs[i];
                }
            }
        }
        return newPrefabs;
    }
    internal IEnumerator CannotControlle(float time)
    {
        delayclear = false;
        canattack = false;
        cannotcontrolle = true;
        Player_ac.SetBool("Attacking", false);
        Player_ac.SetBool("Falling", true);
        yield return new WaitForSeconds(time);
        delayclear = true;
        StartCoroutine(Finish_Combos());

    }
    public void destroyedobjecteffect(int number, float effect, int direction)
    {
        if (number == 1)
        {
            StartCoroutine(CannotControlle(effect));
            Player_rb.gravityScale = 4f;
        }
        if(number == 2)
        {
            StartCoroutine(CannotControlle(effect));
            StartCoroutine(Becannottounch(effect));
            Player_rb.gravityScale = 4f;
            Player_rb.velocity = Vector2.zero;
            Player_rb.AddForce(new Vector2(500f* direction, 7000f));
        }
    }
    IEnumerator Becannottounch(float time)
    {
        me.layer = 12;
        yield return new WaitForSeconds(time);
        me.layer = 6;
    }
    public void StartCombos()
    {
        if (!cannotcontrolle)
        {
            canattack = true;
            if (attacknum < 3)
            {
                attacknum++;
            }
        }
    }
    public void CannotAttack()
    {
        canattack = false;
    }
    IEnumerator Finish_Combos()
    {
        if (!dashchecing)
        {
            attacking = false;
            dashchecing = false;
            attacknum = 1;
            Player_ac.SetBool("Attacking", false);
            yield return new WaitForFixedUpdate();
            canattack = true;
        }
    }    
    public void Attack_Start()
    {
        if (!cannotcontrolle)
        {
            Player_rb.velocity = Vector2.zero;
            attacking = true;
        }
    }
    public void Attack_1()
    {
        Player_rb.AddForce(Vector2.right * lookat * 3000f);
        Instantiate(attack_1,transform.position, attack_1.transform.rotation);
    }
    public void Attack_2()
    {
        Player_rb.AddForce(new Vector2(200f * lookat, 3000f));
        Instantiate(attack_2, transform.position, attack_2.transform.rotation);;
    }
    public void Attack_3()
    {
        Player_rb.AddForce(Vector2.right * lookat * 5000f);
        Instantiate(attack_3, transform.position, attack_3.transform.rotation);
    }

    public void JumpAttack()
    {
        canjumpattack = false;
        Player_rb.gravityScale = 0f;
        Player_rb.velocity = Vector2.zero;
        Player_rb.AddForce(Vector2.right * lookat * 7000f);
        Instantiate(jumpattack, transform.position, jumpattack.transform.rotation);
    }
    public void CanJumpattack()
    {
        canjumpattack = true;
    }
    public void JumpAttackend()
    {
        StartCoroutine(Finish_Combos());
        Player_rb.gravityScale = 4f;
        canjumpattack = false;
    }
    public void DashStart()
    {
        if (!cannotcontrolle)
        {
            dashchecing = true;
            attacking = true;
            Player_rb.gravityScale = 0;
        }
    }
    public void Dashmove()
    {
        Player_rb.velocity = Vector2.zero;
        Player_rb.AddForce(Vector2.right * lookat * 12000f);
    }
    void Dashchecking()
    {
        if (attacking)
        {
            if (dashchecing)
            {
                me.layer = 12;
            }
            else
            {
                if (!hitcheck)
                {
                    me.layer = 6;
                }
            }
        }
    }
    public void Cannotdashchecking()
    {
        dashchecing = false;
    }
    public void Dashend()
    {
        StartCoroutine(Finish_Combos());
        dash_cd = 0.5f;
        me.layer = 6;
        Player_rb.gravityScale = 4;
    }
    public void changelookat()
    {
        float Horizontal_input = Input.GetAxisRaw("Horizontal");
        if (Horizontal_input != 0)
        {
            transform.localScale = new Vector3(Horizontal_input * 0.6f, 0.6f, 1);
        }
        if (transform.localScale.x > 0.1f)
        {
            lookat = 1;
        }
        else
        {
            lookat = -1;
        }
    }
}
