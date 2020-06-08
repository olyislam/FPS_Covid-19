using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    #region singletone
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        Initilize();
    }
    #endregion singletone

    [Tooltip ("UI referenc for update ui info using GameManager")]
    public UIHandeller ui;
    [Tooltip("Score Board referenc for update Score using GameManager")]
    public ScoreBoard scoreboard;
    
    [Tooltip("This audio will play when a vairus destoy")]
    public AudioSource VirusDestroyAudio;

    [Tooltip("Player reference for access using GameManager")]
    public GameObject Player;

    public bool isGameOver;

    
    //Initial from virus generator and use to virusbehaviour class
    [HideInInspector, Tooltip("How many virus spawned from start moment of this game")]
    public int SpawnedVirusNo;


    public float AraSize
    {
        get
        {
            return ui.AreaResizer.value;
        }
    }


    //Call from Awake from this block
    private void Initilize()
    {
        if (Player == null) { Player = GameObject.FindGameObjectWithTag("Player"); }
        isGameOver = true;
        
    }


    //Game Over Action from Player Controller when player is dead
    public void OnGameover()
    {
        ui.SetActivity(ui.GameOverPanel);
        ui.OnGameOver(scoreboard.GetScore);
        foreach (GameObject virus in GameObject.FindGameObjectsWithTag("Virus"))
        {
            Destroy(virus);
        }

    }

    
    public void ReadyToPlay()
    {
        StartCoroutine(CountDown());
    }
    private IEnumerator CountDown()
    {
        ui.SetActivity(ui.CountDownPanel);

        yield return new WaitForSeconds(1);
        ui.ShowCountDown(1.ToString());
        
        yield return new WaitForSeconds(1);
        ui.ShowCountDown(2.ToString ());
        
        yield return new WaitForSeconds(1);
        ui.ShowCountDown(3.ToString());
        
        yield return new WaitForSeconds(1);
        ui.ShowCountDown("");
        ui.SetActivity(ui.MainPanel);

        yield return new WaitForSeconds(1);
        isGameOver = false;
    }


    public void Rescan()
    {
        int SceneNo = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

       UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNo);
    }

}
