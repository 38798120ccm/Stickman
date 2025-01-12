using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aihp : MonoBehaviour
{
    public enemy me;
    public int hpdiscount;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = transform.localScale;
        Vector2 HP_show = new Vector2(me.HP/hpdiscount, transform.localScale.y);
        transform.localScale = Vector2.Lerp(offset, HP_show, Time.deltaTime);
    }
    internal void AITakeDamage(float damage)
    {
        me.HP -= damage * me.defend;
    }
}
