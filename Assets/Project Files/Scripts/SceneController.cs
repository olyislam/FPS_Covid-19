using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }



    public float Sound_Volume;
    private void AudioController()
    {
        AudioSource[] Audios = GameObject.FindObjectsOfType<AudioSource>();
        if (Audios.Length <= 0)
            return;


        foreach (AudioSource audio in Audios)
        {
            audio.volume = Sound_Volume;
        }
    }

    public void Retry()
    {
        Play(0);
    }

    private void Update()
    {
        AudioController();
        UserControl();
    }
    //back when game over and exit from game playe using back button
    private void UserControl()
    {
        /************this bolock will remove**************/
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.isGameOver)
        {
            Retry();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.instance.isGameOver)
        {
            Retry();
            //Quit();
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
            {
                Debug.Log("<color=red>OK</color>");
                Destroy(this.gameObject);
            }
        }
        /************this bolock will remove**************/
    }


    public void Play(int indx)
    {
        SceneManager.LoadScene(indx);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
