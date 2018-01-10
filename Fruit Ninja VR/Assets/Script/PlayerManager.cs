using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public PlayerInfo playerInfo;

    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        initTestData();
    }

    private void initTestData()
    {
        playerInfo = new PlayerInfo();
        playerInfo.userName = "Test player";
        playerInfo.userLife = 5;
        playerInfo.userCurrentLevelPoint = 0;
        playerInfo.userAccumulatedPoint = 0;
        playerInfo.userCurrentLevel = 0;
        playerInfo.userSword = 0;

    }

    public void increasePoint()
    {
        playerInfo.userCurrentLevelPoint++;
    }

    public void decreasePoint()
    {
        playerInfo.userCurrentLevelPoint--;
    }

    public void increaseLife()
    {
        playerInfo.userLife++;
    }

    public void decreaseLife()
    {
        playerInfo.userLife--;
    }

    public void addCurrentPointToAccumulatedPoint()
    {
        playerInfo.userAccumulatedPoint += playerInfo.userCurrentLevelPoint;
    }
}
