using UnityEngine;

public class VirusBehaviour : MonoBehaviour
{
    //this path hold possible way to player
    public WayPoints way_points;
    

    //get virus color W.R.to virus life
    protected Color GetColor(int CurrenHealth)
    {
        switch (CurrenHealth)
        {
            case 1:
                return Color.red;
               
            case 2:
                return Color.yellow;
               
            case 3:
                return Color.blue;
            default:
                return Color.green;
        }
    }


    //return true when virus detected bullet from her front side
    protected bool DetectedBullet(Transform origin)
    {
        RaycastHit hit;
        if (Physics.Raycast(origin.position, origin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, -1))
        {
            Debug.DrawRay(origin.position, origin.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            return  hit.collider.tag == "Bullet";
        }
        else
        {
            Debug.DrawRay(origin.position, origin.TransformDirection(Vector3.forward) * 1000, Color.green);
            return false;
        }
    }



    //return random derection to generate a save zone
    protected Vector3 GetDirection(Transform origin, Vector3 targrt)
    {
        Vector3 direction = origin.position - targrt;
        float diagonal = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2) + Mathf.Pow(direction.z, 2));
        
        direction = direction.normalized;
        direction += origin.position;

        int Scale = Random.Range(2, 3);
        int Indx = Random.Range(1, 4);

        switch (Indx)
        {
            case 1:
               return direction + (origin.forward + origin.right) * Scale;
       
            case 2:
                return direction + (origin.forward + -origin.right )* Scale;
   
            case 3:
                return direction + (origin.forward + origin.up) * Scale;
          
            case 4:
                return direction + (origin.forward + -origin.up )* Scale;

            default:
                return (origin.forward + origin.up + origin.right) * Scale;
        }
    }

    /*
    //generate virus damage power that apply to player health
    protected int GetVirusPower()
    {
        int spawned = GameManager.instance.SpawnedVirusNo;
        if (spawned <= 10)
            return 5;
        else if (spawned <= 20)
            return 6;
        else if (spawned <= 30)
            return 7;
        else if (spawned <= 40)
            return 8;
        else if (spawned <= 50)
            return 9;
        else return 10;
    }

    */
    //ar-anik
    //Origin transform look to Target
    protected void LookTo(Transform Origin, Transform Target)
    {

        Origin.LookAt( Target);
    }
    //virus movement
    protected void movement(Transform Origin, Vector3 Target, float Speed)
    {
        Origin.position = Vector3.MoveTowards(Origin.position, Target, Time.deltaTime *0.5f* Speed);
    }

    protected bool OnDecession;
    //invoke with delay time from "virus" class to make next decession
    protected void StopProcess()
    {
        OnDecession = false;
    }

}

   
   
