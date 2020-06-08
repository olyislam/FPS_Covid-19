using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class patient : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField, Tooltip("Patient Health")]
    private int Health;
    [SerializeField, Range(0.02f, 1f), Tooltip("Control Patient Speed")]
    private float Speed;


    [SerializeField, Tooltip("This clip will destroy when attached with patient")]
    private AudioClip VirusDestroclip;
#pragma warning restore 649

    PlayerController PlayerShield;
    public void SetInformation(PlayerController ShieldData)
    {
        this.PlayerShield = ShieldData;
    }


    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Time.deltaTime * Speed, 0, 0); 
    }

    private void OnTriggerEnter(Collider col)
    {
        
        GameObject obj = col.gameObject;
        if (obj.tag == "Finish")
        {
            PlayerShield.AddShield();
            obj.GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
        }

    }

    //Decress Health amount when attached with virus object
    private void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter(Collision col)
    {
        GameObject obj = col.gameObject;
        if (obj.tag == "Virus")
        {

            Virus _virus = obj.GetComponent<Virus>();
            int damage_amount = _virus.DamagePower;
           // _virus.path.Reset();
            TakeDamage(damage_amount);
            Destroy(obj);
            GetComponent<AudioSource>().clip = VirusDestroclip;
            GetComponent<AudioSource>().Play();
        }
    }
}
