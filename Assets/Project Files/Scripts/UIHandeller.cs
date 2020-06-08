using UnityEngine;
using UnityEngine.UI;

public class UIHandeller : MonoBehaviour
{
#pragma warning disable 649


    #region Private Member

    [SerializeField,Tooltip("Intigate Player Health status on this GUI Field")]
    private Slider HealthBar;
    [SerializeField, Tooltip("Show Player socre status on this GUI Field")]
    private Text Score;
    [SerializeField, Tooltip("Show number of available shield status on this GUI Field")]
    private Text shield;

    [SerializeField, Tooltip("Show number of activated virus on this GUI Field")]
    private Text NO_ofVirus;
    [SerializeField, Tooltip("Show Count Down Time When will Start this Game")]
    private Text CountDown;
    [SerializeField, Tooltip("Show Count Down Time When will Start this Game")]
    private Text GameOverScore;



    private UIActivity activity = new UIActivity();
    #endregion Private Member

    [Space]
 

    #region Public Member
    [Header("Capturing Debug Reference")]
    [Tooltip("Show area on Screen, how many area will capture to Create wall Texture")]
    public RectTransform CaptureArea;
    [Tooltip ("BUtton reference for active visible or not visible mode Note: use when capture area is right position")]
    public Button SpawnButton;


    [Space ]
    [Header ("Detected wall info")]
    [SerializeField, Tooltip("show information when detected wall")]
    public Text DetecteWall;
    public Slider AreaResizer;


    [Space]

    [Header("Reference for UI On Off Reference")]
    [SerializeField]
    public GameObject MainPanel;
    [SerializeField]
    public GameObject CptureAreaPanel;   
    [SerializeField]
    public GameObject CountDownPanel;
    [SerializeField]
    public GameObject GameOverPanel;
    #endregion Public Member

#pragma warning restore 649


    private void Start()
    {

        Iniilize();
    }

    private void Iniilize()
    {
        MainPanel.SetActive(false);

        SetActivity(CptureAreaPanel);
        SpawnButton.interactable = false;
    }




    //Update Player Health status on GUI
    public void UpdateHealth(float health)
    {
        this.HealthBar.value = health;
    }

    //Update Player Score on GUI
    public void UpdateScore(float score)
    {
        this.Score.text = "Score : "+ score;
    }

    public void UpdateShield(float shieldNo)
    {
        this.shield.text = "Shield : " + shieldNo;
    }

    //Show how many avivated virus in this game scene
    public void UpdateVirus(float virusNO)
    {
        NO_ofVirus.text = "activated virus : " + virusNO;
    }


    //Control All UI element on and off on this Screen
    //use when capture Screen
    public void SetActive_ALL(bool on)
    {
        this.MainPanel.SetActive(on);
        this.GameOverPanel.SetActive(on);
        this.CountDownPanel.SetActive(on);
        this.CptureAreaPanel.SetActive(on);
        SpawnButton.interactable = on;//button interectable disable from start to control game spawn and capture area
    }

    public void ShowCountDown(string count)
    {
        this.CountDown.text = count;
    }



   // [Tooltip("Hold Current Activity that is still active on the screen")]
   // private GameObject CurrentActivity;/***********/
    //Show new Activity page on the screen Change any where using GameManager
    public void SetActivity(GameObject current)
    {

        activity.SetActivity(current);
       /* if (this.CurrentActivity == null)
        {
            this.CurrentActivity = current;
            this.CurrentActivity.SetActive(true);
            return;
        }

        this.CurrentActivity.SetActive(false);
        this.CurrentActivity = current;
        this.CurrentActivity.SetActive(true);
        */
    }


    public void OnGameOver(int score)
    {
        this.GameOverScore.text = "Score " + score;

    }

}
