using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int ableMoveToSides;
    [SerializeField]
    private int ableMoveToBack;
    [SerializeField]
    private GameObject pl;
    [SerializeField]
    private static int coins;
    [SerializeField]
    private MenuManager menuManager;
    [SerializeField]
    private Text coinsText;
    [SerializeField]
    private Text recordText;
    [SerializeField]
    private Text currentScoreText;
    [SerializeField]
    private LevelGenerator levelGen;
   

    private int currentScore;
    private int record;
    private Player player;
    private int DeathScore;



    void Start()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
            coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
        }
        else
        {
            coins = 0;
            coinsText.text = "0";
        }
        if (PlayerPrefs.HasKey("Record"))
        {
            recordText.text = PlayerPrefs.GetInt("Record").ToString();
            record = PlayerPrefs.GetInt("Record");
        }
        else
        {
            recordText.text = "0";
            record = 0;
        }

    }
    public int GetAbleMove()
    {
        return ableMoveToSides;
    }
    
    public bool AttemptToMove(Vector3 direction)
    {
        return true;
    }
    private void CheckRecord()
    {
        if (currentScore > record)
        {
            record = currentScore;
        }
    }


    public void LoseGame()
    {
        menuManager.LoseGameChange(currentScoreText);
        CheckRecord();
        currentScore = 0;
        SaveAll();
       
    }

    void Update()
    {
        if (pl.transform.position.x > float.Parse(currentScoreText.text))
        {
            currentScoreText.text = ((int)pl.transform.position.x).ToString();
            if (pl.transform.position.x > record)
            {
                
                record = (int)pl.transform.position.x;
                recordText.text = record.ToString();
            }
        }
        //if (Mathf.Abs(pl.transform.position.z - ableMoveToSides) > 0)
        //{
        //    pl.GetComponent<Player>().KillAnimation(Player.DeathAnimation.Simple, 2);
        //}
        //if (Mathf.Abs(pl.transform.position.x - currentPosition.x + ableMoveToBack) > levelGen.minGeneration)
        //{
        //    pl.GetComponent<Player>().KillAnimation(Player.DeathAnimation.Simple, 2);
        //}
    }
    public void ChangeCoins()
    {
        coins++;
        coinsText.text = coins.ToString();
    }
   
    private void SaveAll()
    {
        PlayerPrefs.SetInt("Coins", coins);
        if (PlayerPrefs.HasKey("Record"))
        {
            if (record > PlayerPrefs.GetInt("Record"))
            {
                PlayerPrefs.SetInt("Record", record);
            }
        }
    }
}
