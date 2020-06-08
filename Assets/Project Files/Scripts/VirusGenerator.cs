using System.Collections.Generic;
using UnityEngine;

public class VirusGenerator : MonoBehaviour
{

#pragma warning disable 649
    [SerializeField, Range(1, 3), Tooltip("How many Time wait for spawn next virus")]
    private float DelayTime = 1.5f;
    
    [Header("Levem Controller Setup")]
    [SerializeField, Range(10, 20),Tooltip("How many destroyed virus fraction between previous to next virus increase")]
    private int IncreaseStepFactor = 10;
    [SerializeField, Range(2,5), Tooltip("How many virus increase to maxvirus")]
    private int VirusIncreaseAmount = 2;
    [SerializeField, Tooltip("At a time how many virus active in the game scene")]
    private int maxVirus = 4;


    [Tooltip("control virus spawning")]
    private bool Generate = false;

    [SerializeField, Header("Virus prefab reference"), Tooltip("virus prefabs List")]
    List<GameObject> AllVirus = new List<GameObject>();

    //virus spawn point list where randomly spawn virus 
    //Note: Each element has Path component
    [SerializeField, Space, Header("Spawn positions that hold a set of path")]
    private List<Transform> Points = new List<Transform>();


#pragma warning restore 649

    private void OnEnable()
    {
        Initilize();
    }
    private void Initilize()
    {
        Invoke("Next", DelayTime);
    }

    //invoke with delay time for generate new virus 
    private void Next()
    {
        Generate = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.instance.isGameOver)
            return;
        //check amount of avtivsted virus is less than Maxvirus amount
        if (!ShuldGrow)
            return;


        if (Generate)
        {
            GameObject virus = GetVirus();
            SpawnVirus(virus);

            Generate = false;
            Invoke("Next", DelayTime);
        }
    }

    private GameObject GetVirus()
    {
        int indx = Random.Range(0, AllVirus.Count - 1);
        return AllVirus[indx];
    }


    //spawn virus object and initial target and possible path
    private void SpawnVirus(GameObject virus)
    {
        GameManager.instance.SpawnedVirusNo++;
        int indx = Random.Range(0, Points.Count - 1);
        GameObject _virus = Instantiate(virus, Points[indx].position, Points[indx].rotation);

        float area_volume = GameManager.instance.AraSize;
        _virus.transform.localScale = _virus.transform.localScale * area_volume;
        Transform MainTarget = GameManager.instance.Player.transform;

        Path path = Points[indx].GetComponent<Path>();
        _virus.GetComponent<Virus>().Initilize(MainTarget, path.Points, area_volume, SetActivationAmount);
        
        GetComponent<AudioSource>().Play();
    }

    //this methode control active vieus amount in the game scene
    private bool ShuldGrow
    {
        get
        {
            int AcitvatedVirus = GameObject.FindGameObjectsWithTag("Virus").Length;
            GameManager.instance.ui.UpdateVirus(AcitvatedVirus);
            return AcitvatedVirus < maxVirus;
        }
    }


    //increase activate virus amount with respect player parfomance, how many virus dead player in runtime
    public void  SetActivationAmount()
    {
        int TotalKills = GameManager.instance.scoreboard.TotalKills;
        bool StepDone = TotalKills % IncreaseStepFactor == 0;

        if (TotalKills > 25)
        {
            DelayTime = 1f;
        }
        if (StepDone)
        {
            maxVirus += VirusIncreaseAmount;
            maxVirus = Mathf.Clamp(maxVirus, 1, 15);
        }

    }


}
