﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static GameplayController instance;              //Static instance of GameManager which allows it to be accessed by any other script.
    public string twitchName;
    public bool paused;
    public float transitTime;

    //private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
    private int levelIndex;
    private int maxIndex = 11;
    private float localTimer;                              //Current level build index

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        localTimer = transitTime;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown("escape") && !paused)
        {
            Pause();
        }
        if (SceneManager.GetActiveScene().buildIndex > 6)
        {
            levelIndex = SceneManager.GetActiveScene().buildIndex;
        }
        if (SceneManager.GetActiveScene().name == "LvCompleteScene")
        {
            localTimer -= Time.deltaTime;
        }
        if (localTimer <= 0)
        {
            if (levelIndex < maxIndex)
                SceneManager.LoadScene(levelIndex + 1);
            else
            {
                SceneManager.LoadScene("MenuScene");
            }
            localTimer = transitTime;
        }
    }

    public void Pause()
    {
        if (SceneManager.GetActiveScene().buildIndex > 6)
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);
            paused = true;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadScene("PauseScene");
        paused = false;
    }


}
