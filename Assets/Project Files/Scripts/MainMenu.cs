using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

#pragma warning disable 649
    [SerializeField, Tooltip ("This Reference Only for Pass Audio Volume")]
    private SceneController Scenand_Audio;


    [SerializeField, Tooltip ("Audio volume controller")]
    private Slider Sound_Slider;


    [Header("Activitys Reference")]
    [SerializeField, Tooltip("UI Main Menu")]
    private GameObject MainPage;

    [SerializeField, Tooltip("UI Camera angle Reminder page")]
    private GameObject CameraReminder_Page;

    [SerializeField, Tooltip("UI Camera angle Reminder page")]
    private GameObject GameStory_Page;

#pragma warning restore 649

    //this object hold Activitys data
    private UIActivity activity = new UIActivity();


    // Call from volume slider Listner
    public void ChangeVolume()
    {
        Scenand_Audio.Sound_Volume = Sound_Slider.value;
    }


    private void Start()
    {
        Initilize();
        ShowMainMenu();
    }

    private void Initilize()
    { 
        MainPage.SetActive(false);
        CameraReminder_Page.SetActive(false);
        GameStory_Page.SetActive(false);
    }

    public void ShowMainMenu()
    { 
        SetActivity(MainPage);
    }
    
    public void ShowInstructionPage()
    {
        SetActivity(CameraReminder_Page);
    }
    public void ShowStoryPage()
    {
        SetActivity(GameStory_Page);
    }


    private void SetActivity(GameObject NewActivity)
    {
        activity.SetActivity(NewActivity);
    }


}
