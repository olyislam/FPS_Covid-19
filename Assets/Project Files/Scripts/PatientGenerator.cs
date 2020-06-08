using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatientGenerator : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField, Tooltip("How ,many time delay time to spawn next patient")]
    private int MinDelatTime, MaxDelayTime;
    [SerializeField, Tooltip("Spawn position")]
    private Transform SpawnPos;


    [SerializeField, Tooltip("Patient Prefat that will spawn")]
    private GameObject PatientObj;
#pragma warning restore 649

    private void OnEnable()
    {

        StartCoroutine(start());
    }

    //delay for start game 
    private IEnumerator start()
    {
        while (GameManager.instance.isGameOver)
        {
            yield return new WaitForSeconds(1);
        }
        Invoke("Spawn", GetDelay);
    }

    private void Spawn()
    {
       // if (GameManager.instance.isGameOver)
         //   return;

        GameObject _patient = Instantiate(PatientObj, SpawnPos.position, SpawnPos.rotation);
        PlayerController AddShieldToPlayer = GameManager.instance.Player.GetComponent<PlayerController>();
        _patient.GetComponent<patient>().SetInformation(AddShieldToPlayer);
        _patient.transform.localScale = _patient.transform.localScale * GameManager.instance.AraSize;


        Debug.Log("sPAWNED");
        Invoke("Spawn", GetDelay);
    }

    private int GetDelay
    {
        get { return Random.Range(MinDelatTime, MinDelatTime); }
    }

}
