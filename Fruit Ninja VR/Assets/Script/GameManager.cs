﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public Level currentLevel = null;

    [HideInInspector]
    public PlayerInfo playerInfo;

    [HideInInspector]
    public enum BoardMode { BoardWin, BoardLoose };

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
    }


    // Use this for initialization
    void Start()
    {
        SetupFirstLevel();
        SetupBoomInterval();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel == null)
            return;

        currentLevel.levelTime -= Time.deltaTime;
        if (currentLevel.levelTime <= 0)
        {
            // end level, user win, load new level
            // play win sound
            SoundManager.Instance.PlaySoundOneShotOnSoundManager(SoundManager.Instance.clipWinSound);

            //todo: show board, score, start button
            ShowBoard(BoardMode.BoardWin);

            // level up
            Level nextLevel = GetNextLevel();
            SetNextLevel(nextLevel);
        }

        if (playerInfo.userLife <= 0)
        {
            // loose game
            // play loose sound
            SoundManager.Instance.PlaySoundOneShotOnSoundManager(SoundManager.Instance.clipLooseSound);

            //todo: show board, score, restart button
            ShowBoard(BoardMode.BoardLoose);

            // set the levelTime again to start new level
            ResetCurrentLevel();
        }

        SpawnerManager.Instance.SpawnFruit(currentLevel);
        SpawnBoom();
    }

    private void SpawnBoom()
    {
        currentLevel.currentIntervalForSpawningBoom -= Time.deltaTime;
        if (currentLevel.currentIntervalForSpawningBoom <= 0)
        {
            SpawnerManager.Instance.SpawnBoom(currentLevel);

            // reset the time interval
            SetupBoomInterval();
        }
    }

    private void SetupBoomInterval()
    {
        currentLevel.currentIntervalForSpawningBoom = Random.Range(currentLevel.minRandomIntervalForSpawningBoom, currentLevel.maxRandomIntervalForSpawningBoom);
    }


    private void ShowBoard(BoardMode mode)
    {
        if (mode == BoardMode.BoardWin)
        {
            //todo

            // if click button Level Up, then go to next level

            // if click main menu, then go to menu
        }

        if (mode == BoardMode.BoardLoose)
        {
            //todo:

            // if click button Restart, then reset the level

            // if click main menu, then go to menu
        }
    }

    //public void increasePoint()
    //{
    //    userPoint++;
    //}

    //public void decreaseLife()
    //{
    //    userLife--;
    //}

    public void ResetCurrentLevel()
    {
        currentLevel = LevelManager.Instance.ResetCurrentLevel(currentLevel);
    }

    public Level GetNextLevel()
    {
        return LevelManager.Instance.GetNextLevel(currentLevel);
    }

    private void SetupFirstLevel()
    {
        currentLevel = LevelManager.Instance.GetFirstLevel();
    }

    public void SetNextLevel(Level level)
    {
        // freeze the scene a bit
        FreezeScene();

        // then start new level
        currentLevel = level;

        // un-freeze the scene, let user continue to play
        UnFreezeSceneAndStartNewLevel();
    }


    private void FreezeScene()
    {

    }

    private void UnFreezeSceneAndStartNewLevel()
    {

    }
}
