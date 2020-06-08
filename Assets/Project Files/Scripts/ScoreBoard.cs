using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [Tooltip("Player total score")]
    private int Score = 0;
    [HideInInspector, Tooltip("Total kills use for gun power W.R.To Total kills")]
    public int TotalKills = 0;

    public int GetScore
    {
        get
        {
            return Score;
        }
    }


    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        GameManager.instance.ui.UpdateScore(this.Score);
    }

    //add score and show update score onto GUI
    public void ScoreUpdate(int amount)
    {
        this.Score += amount;
        TotalKills++;
        GameManager.instance.ui.UpdateScore(this.Score);
    }

}
