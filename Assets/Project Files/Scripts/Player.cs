using UnityEngine;

public class Player
{
    [Tooltip("audio source Reference")]
    private AudioSource Audio;

    //pass audio source from player controller
    public Player(AudioSource audio)
    {
        this.Audio = audio;
    }

    //play audion that will pass from player controller
    public void PlayAudio(AudioClip AudioClip)
    {
        this.Audio.clip = AudioClip;
        this.Audio.Play();
    }

    //spawn bulllet and reutrn "Bullet" Class from this bullet for pass bullet information into player controller
    public Bullet SpawnBullet(GameObject Bullet, Camera cam)
    {
        Vector3 pos = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1));
        
        Quaternion rotation = cam.transform.rotation;
        
        GameObject bullet = MonoBehaviour.Instantiate(Bullet, pos, rotation);
        return bullet.GetComponent<Bullet>();
    }

    //return player gun power W.R.To total kills
    public int GetGunpower()
    {
        ScoreBoard score = GameManager.instance.scoreboard;
        if (score.TotalKills < 20)
            return 1;
        else 
            return 2;


    }

    //it will return true when player is dead
    public bool isDead(int Health)
    {
        return Health <= 0;
      
    }
}