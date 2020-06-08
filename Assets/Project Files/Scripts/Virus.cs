using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]

public class Virus : VirusBehaviour
{
    [Header("Virus Property")]
    [SerializeField, Range (0.2f, 2f)]
    private float Speed;
    [Range(3, 10)]
    public int VirusLife = 3;
    [SerializeField, Range(0.3f,3f)]
    private float Delay;

    
    [Tooltip("this transform hold direct player transform")]
    private Transform MainTarget;
    [Tooltip("that hold every instant sub target point")]
    private Vector3 SubTarget;

    [Tooltip("This property is power of this viru that can be damage to player")]
    public int DamagePower;


    private bool isOutofWall = false;

    private UnityAction IncreseVirusAmount;

    //Initilize when instantiate this object
    public void Initilize(Transform target, WayPoints waypoints, float AreaSize, UnityAction Increase)
    {
        IncreseVirusAmount = null;
        this.IncreseVirusAmount = Increase;

        this.Speed = this.Speed * AreaSize;
        MainTarget = target;
        way_points = waypoints;
        SubTarget = way_points.GetCurrentPath.position;
        if (!isOutofWall)
        {
            isOutofWall = way_points.GetCurrentPath.tag == "out";
        }

        GetComponent<Rigidbody>().useGravity = false;
       // DamagePower = GetVirusPower();
       
        if (Delay == 0)
            Delay = Random.Range(1f,2f);
    }
        
    
    // Update is called once per frame
    private void Update()
    {
        FindPath();
        
       // MakeDecesion();
        
        
        LookTo(this.transform, MainTarget);
        movement(this.transform, SubTarget, Speed);
    }

    //find next path that will going to player direction
    private void FindPath()
    {
        float distance = Vector3.Distance(this.transform.position, SubTarget);
       
        if (distance < 0.1f)
        {
           // Speed = 1;
            Transform NextDirection = way_points.Nextpath();
           
            if (!isOutofWall && NextDirection != null)
            { 
                isOutofWall = NextDirection.tag == "out";
            }
   
            bool PatietisActivated = GameObject.FindObjectsOfType<patient>().Length > 0;

            if (PatietisActivated && isOutofWall)
            {
                MainTarget = GetTargetedPatient();
                SubTarget = MainTarget.position;
            }
            else
            {
                SubTarget = NextDirection != null? NextDirection.position : GameManager.instance.Player.transform.position;
               // MainTarget = 
            }
            
              
            if (NextDirection == null)
            {
                SubTarget = MainTarget.position;
                return;
            }

        }
    }
    private Transform GetTargetedPatient()
    {
        patient[] ActivePatients = GameObject.FindObjectsOfType<patient>();
        int patientno = Random.Range(0, ActivePatients.Length - 1);
        return ActivePatients[patientno].transform;
    }
         
 
    //if this methode return true thats mean virus is dead
    public bool TakeDamage(int amount)
    {
        VirusLife -= amount;
        bool isDead = VirusLife <= 0;
        GetComponent<Animator>().SetBool("Damage", true);
        Invoke("StopAnimation", 0.1f);
        if (isDead && IncreseVirusAmount != null)
        {
            IncreseVirusAmount();
        }
        return isDead;
    }

    //invoke from "TakeDamage" Methode for stop damage animation
    private void  StopAnimation()
    {
        GetComponent<Animator>().SetBool("Damage", true);
    }
}


    
