using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [Tooltip("Bullet power that will apply to virus life")]
    private int DamagePower;


    //initial when spaen this bullet
    public void GoTo(Vector3 Dir, float Speed, int damagepower)
    {
        this.DamagePower = damagepower;
        Destroy(this.gameObject, 4);

        Rigidbody bullet = GetComponent<Rigidbody>();
        bullet.useGravity = false;
        bullet.AddForce(Dir * Speed);

    }

    private void OnCollisionEnter(Collision col)
    {
        GameObject Obj = col.gameObject;
        string tag = Obj.tag;
        switch (tag)
        {

            case "Bullet":
                Destroy(this.gameObject);
                break;

            case "Virus":

                Virus virus = Obj.GetComponent<Virus>();
                bool isdead = virus.TakeDamage(this.DamagePower);
                
                //after shoot virus is still active or dead
                if (isdead)
                {
                    GameManager.instance.VirusDestroyAudio.Play();

                    int score = (int)virus.DamagePower / 2;
                    GameManager.instance.scoreboard.ScoreUpdate(score);
                    Destroy(Obj);

                }

                Destroy(this.gameObject);
                break;
        }
        Destroy(this.gameObject);
     
    }

}
