using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        GameObject Obj = col.gameObject;

        if (Obj.tag == "Virus")
        {
            Virus virus = Obj.GetComponent<Virus>();
            int score = (int)virus.DamagePower / 2;
            GameManager.instance.scoreboard.ScoreUpdate(score);
            GetComponent<AudioSource>().Play();


            bool VirusIsDead = virus.TakeDamage(virus.VirusLife);
            if (VirusIsDead)
            { 
                Destroy(Obj);
            }

        }

      
    }
}
