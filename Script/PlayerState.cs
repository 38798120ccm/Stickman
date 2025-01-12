using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    RectTransform HP_rt;
    Image HP_im;
    Text Money_show;
    public GameObject Money;
    public Sprite[] HP_col;

    // Start is called before the first frame update
    void Start()
    {
        Money_show = Money.GetComponent<Text>();
        HP_im = GetComponent<Image>();
        HP_rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = HP_rt.sizeDelta;
        Vector2 Player_hpshow = new Vector2(PlayerController.player_hp * 5, HP_rt.sizeDelta.y);
        HP_rt.sizeDelta =Vector2.Lerp(offset,Player_hpshow,Time.deltaTime * 5f);
        Money_show.text = ":" + PlayerController.player_money;
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(20);
        }
        if (HP_rt.sizeDelta.x < 200)
        {
            HP_im.sprite = HP_col[1];
        }
        else
        {
            HP_im.sprite = HP_col[0];
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerController.player_hp = 100f;
        }
    }
    public static void TakeDamage(float damage)
    {
        PlayerController.player_hp = PlayerController.player_hp - damage;
    }
    public static void GetMoney(int money_get)
    {
        PlayerController.player_money += money_get;
    }
}
