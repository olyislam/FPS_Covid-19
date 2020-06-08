using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackGround;
using GoogleARCore;
using GoogleARCore.Examples.ObjectManipulation;
using UnityEngine.UI;

public class WallGenerator : Manipulator
{

    private BackgroundData background;

    public Camera FPScam;

    public WallRatio Wall_Ratio;
    private Vector3 Initialposition;
    private Vector3 InitialSize;


    private Rect CaptureArea;
    private bool isValidArea;
    public static bool isDetectWall;//initial from sdk


    private void Start()
    {
        background = new BackgroundData(this.gameObject);
        Initialposition = Wall_Ratio.GetRatio.position;
        InitialSize = Wall_Ratio.Size;
        isDetectWall = false;
        isValidArea = false;

#if UNITY_EDITOR
        Wall_Ratio.GetRatio.position  = Vector3.forward * 3;
        isDetectWall = true;
#endif
    }

    //resize game area
    //call from slider event
    public void OnChangeArea()
    {
        Wall_Ratio.Size = InitialSize * GameManager.instance.AraSize;
    }

    protected override void Update()
    {
        base.Update();

        if (!isDetectWall || background.HasEnvironment || Wall_Ratio.GetRatio.position == Initialposition)
        {

            if (GetComponent<LineRenderer>() != null &&
                GetComponent<LineRenderer>().enabled)
            {
                GetComponent<LineRenderer>().enabled = false;
            }

            if (!isDetectWall)
            {
                GameManager.instance.ui.DetecteWall.text = "Scan the wall";
                GameManager.instance.ui.DetecteWall.color = Color.red;
            }
            else if (Wall_Ratio.GetRatio.position == Initialposition)
            {
                GameManager.instance.ui.DetecteWall.text = "Tap On Screen";
                GameManager.instance.ui.DetecteWall.color = Color.green;
            }

            return;
        }

 
        List<Vector3> worldarea = background.GetWorldArea(Wall_Ratio.GetRatio, Wall_Ratio.GetRatio.position);
        background.DrawWorldArea(worldarea);
        CaptureArea = background.GetScreenArea(FPScam, worldarea[0], worldarea[2]);


        string Captureinfo = "";
        Color front_color = new Color();
        isValidArea = background.isValidAre(CaptureArea, ref Captureinfo, ref front_color);


        GameManager.instance.ui.DetecteWall.color = front_color;
        GameManager.instance.ui.DetecteWall.text = Captureinfo;
        GameManager.instance.ui.SpawnButton.interactable = isValidArea;

       
       

#if UNITY_EDITOR
        if (!isValidArea)
            return;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Capture();
        }
#endif

    }


    public void Capture()
    {
        if (!isValidArea)
            return;

        GameManager.instance.ui.SetActive_ALL(false);
        if (GetComponent<LineRenderer>() != null &&
            GetComponent<LineRenderer>().enabled == true)
        {
            GetComponent<LineRenderer>().enabled = false;
        }
        background.ClearPlaneSport();

        StartCoroutine(background.Captutre(CaptureArea, Wall_Ratio));
     
    }


    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if (gesture.TargetObject == null)
        {
            return true;
        }

        return false;
    }


    /// <summary>
    /// Function called when the manipulation is ended.
    /// </summary>
    /// <param name="gesture">The current gesture.</param>
    protected override void OnEndManipulation(TapGesture gesture)
    {
       
        if (background.HasEnvironment) { return; }
        if (gesture.WasCancelled){return; }
        if (gesture.TargetObject != null) {return;}

        // Raycast against the location the player touched to search for planes.
        TrackableHit hit1;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon;

        if (Frame.Raycast(gesture.StartPosition.x, gesture.StartPosition.y, raycastFilter, out hit1))
        {
            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if ((hit1.Trackable is DetectedPlane) &&
                Vector3.Dot(FPScam.transform.position - hit1.Pose.position,
                    hit1.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                float TouchArea = (Screen.width / 4) * 3;
                if (hit1.Pose.position.x > TouchArea) { return; }

                Wall_Ratio.Set_Position(hit1.Pose.position);
            }
        }
    }


    private TouchInput input = new TouchInput();
    private void OnGUI()
    {
        if (input.ClickedOnUI)
            return;


        //  if (background.HasEnvironment)
        //   return;
        bool isMoveed = input.Moveing;
        if (isMoveed)
        {
            Vector2 dir = input.GetDirection() / 20;

            Wall_Ratio.GetRatio.position += new Vector3(dir.x, dir.y, 0) * Time.deltaTime;
        }
    
    }

}

