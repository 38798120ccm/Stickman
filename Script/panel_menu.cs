using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerDate
{
    public int money;
    public float hp;
    public int getmoney()
    {
        return money;
    }
    public float gethp()
    {
        return hp;
    }
}
public class panel_menu : MonoBehaviour
{
    public static bool stopgame = false;
    public GameObject Panel_menu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!stopgame)
            {
                Stopgame();
            }
            else
            {
                Resume();
            }
        }
    }
    public void Resume()
    {
        Panel_menu.SetActive(false);
        Time.timeScale = 1f;
        stopgame = false;
    }
    public static void Save()
    {
        PlayerDate playerDate = new PlayerDate() { money = PlayerController.player_money, hp = PlayerController.player_hp };
        var SerializedDate = JsonUtility.ToJson(playerDate);
        var filePath = Application.persistentDataPath + "/" + "save.dat";
        File.WriteAllText(filePath, SerializedDate);
        Debug.Log(SerializedDate);
    }
    public void Load()
    {
        var filePath = Application.persistentDataPath + "/" + "save.dat";
        var SerializedDate = (string)(null);
        try
        {
            SerializedDate = File.ReadAllText(filePath);
            Debug.Log(SerializedDate);
            PlayerDate playerDate = JsonUtility.FromJson<PlayerDate>(SerializedDate);
            PlayerController.player_money = playerDate.getmoney();
            PlayerController.player_hp = playerDate.gethp();
        }
        catch (System.IO.FileNotFoundException)
        {
            Debug.Log("no");
        }

    }
    public void Leavegame()
    {
        Debug.Log("leavegame_ok");
    }
    void Stopgame()
    {
        Panel_menu.SetActive(true);
        Time.timeScale = 0f;
        stopgame = true;
    }
}
