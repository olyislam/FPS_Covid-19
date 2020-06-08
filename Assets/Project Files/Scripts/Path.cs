using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField]
    private List<Transform> points;//path transfrom
#pragma warning restore 649



    public WayPoints Points;
    private void Start()
    {
        Points = new WayPoints(points);
    }

}







public struct WayPoints
{

    public WayPoints(List<Transform> points)
    {
        this.points = points;
        PathCounter = 0;
    }

    private List<Transform> points;//path transfrom
    private int PathCounter;// = 0;//path transfrom



    //return next psth available return next path and not available return Vector zero
    public Transform Nextpath()
    {
        PathCounter++;

        if (PathCounter < points.Count)
        {
            return points[PathCounter];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// return last vector from path that Onprogress
    /// </summary>
    public Transform GetCurrentPath
    {
        get
        {
            if (PathCounter < points.Count)
                return points[PathCounter];
            else
                return points[points.Count - 1];

        }
    }

}
