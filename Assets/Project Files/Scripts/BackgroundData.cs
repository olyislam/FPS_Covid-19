using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BackGround
{
    public class BackgroundData
    {
        [Tooltip("That Wall Spawn")]
        public bool HasEnvironment = false;
        GameObject LineRender_Parent;

        public BackgroundData(GameObject LineRender_parent)
        {
            this.LineRender_Parent = LineRender_parent;
        }

        //it will return a list of vector that actual rectangular position in world space
        public List<Vector3> GetWorldArea(Transform Ratio, Vector3 Center)
        {
            float x = Ratio.localScale.x / 2;
            float y = Ratio.localScale.z / 2;//change axix if you use unty  default cube

            Vector3 TopLeft = new Vector3(Center.x - x, Center.y + y, Center.z);
            Vector3 TopRight = new Vector3(Center.x + x, Center.y + y, Center.z);
            Vector3 BottomLeft = new Vector3(Center.x - x, Center.y - y, Center.z);
            Vector3 BottomRight = new Vector3(Center.x + x, Center.y - y, Center.z);


            List<Vector3> worldarea = new List<Vector3>();
            worldarea.Add(BottomLeft);
            worldarea.Add(BottomRight);
            worldarea.Add(TopRight);
            worldarea.Add(TopLeft);

            return worldarea;
        }


        //it will render a line to list of vector3 position using LineRenderer
        public void DrawWorldArea(List<Vector3> area)
        {
            LineRenderer draw;
            if (LineRender_Parent.GetComponent<LineRenderer>() == null)
                draw = LineRender_Parent.AddComponent<LineRenderer>();
            else
                draw = LineRender_Parent.GetComponent<LineRenderer>();

            if(!draw.enabled)
            {
                draw.enabled = !draw.enabled;
            }

            const int MaxVertex = 5;
            const float Draw_width = 0.02f;

            draw.material = new Material(Shader.Find("Sprites/Default"));
            draw.widthMultiplier = Draw_width;
            draw.positionCount = MaxVertex;


            Gradient gradient = new Gradient();
            gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.cyan, 1.0f) },
                             new GradientAlphaKey[] { new GradientAlphaKey(1, 0.0f), new GradientAlphaKey(1, 1.0f) });
            draw.colorGradient = gradient;

            if (area.Count != MaxVertex - 1)
                Debug.Log("<color=red>Line renderer index not equal with this list that passd into this parameter to draw area</color>");

            for (int x = 0; x < area.Count; x++)
            {
                if (x == area.Count - 1)
                {
                    draw.SetPosition(x, area[x]);
                    draw.SetPosition(draw.positionCount - 1, area[0]);
                }
                draw.SetPosition(x, area[x]);
            }
        }


        //Get Screen Area from World space between two vector3 position
        public Rect GetScreenArea(Camera FPScam,Vector3 BottomLeft, Vector3 TopRight)
        {
            Vector2 topright = FPScam.WorldToScreenPoint(TopRight);
            Vector2 bottomleft = FPScam.WorldToScreenPoint(BottomLeft);

            return new Rect(bottomleft.x, bottomleft.y, topright.x, topright.y);
        }

        //actually it will control a scale of rect transform that show in 2D space
        public void DrawScreenArea(Rect screenarea, RectTransform display)
        {
            display.anchorMax = display.anchorMin = Vector2.zero;
            display.offsetMin = new Vector2(screenarea.x, screenarea.y);
            display.offsetMax = new Vector2(screenarea.width, screenarea.height);
        }


        public bool isValidAre(Rect ScreenArea, ref string information, ref Color frontcolor)
        {
            bool OverHeight = ScreenArea.height > Screen.height;
            bool OverWidth = ScreenArea.width > Screen.width;
            bool LessHeight = ScreenArea.y < 0;
            bool LessWidth = ScreenArea.x < 0;


            //forword and Back
            if (OverHeight && OverWidth && LessHeight && LessWidth)
            {
                frontcolor = Color.red;
                information = "Move to Back direction";
            }
            else if (OverHeight && LessHeight)
            {
                frontcolor = Color.red;
                information = "Move to Back direction";
            }
            else if ( OverWidth && LessWidth)
            {
                frontcolor = Color.red;
                information = "Move to Back direction";
            }


            //Diagonal instructions Feedback
            else if (OverHeight && LessWidth)
            {
                frontcolor = Color.red;
                information = "Move to Left Top direction";
            }
            else if (OverHeight && OverWidth)
            {
                frontcolor = Color.red;
                information = "Move to Right Top direction";
            }
            else if (LessHeight && LessWidth)
            {
                frontcolor = Color.red;
                information = "Move to Left Bottom direction";
            }
            else if (LessHeight && OverWidth)
            {
                frontcolor = Color.red;
                information = "Move to Right Bottom direction";
            }

            //Horizontal instructions Feedback
            else if (LessWidth)
            {
                frontcolor = Color.red;
                information = "Move to Left direction";
            }
            else if (OverWidth)
            {
                frontcolor = Color.red;
                information = "Move to Right direction";
            }
            else if (LessHeight)
            {
                frontcolor = Color.red;
                information = "Move to Bottom direction";
            }
            else if (OverHeight)
            {
                frontcolor = Color.red;
                information = "Move to Top direction";
            }


            bool isValidArea = !OverHeight && !OverWidth && !LessHeight && !OverWidth;
            if (isValidArea)
            {
                frontcolor = Color.blue;
                information = "Click Spawn Button to Start this Game";
            }

            return isValidArea;
        }

        //Capturee Screem Pixels as Texture2D
        public IEnumerator Captutre(Rect CaptureArea,WallRatio Ratio)
        {
            HasEnvironment = true;
            yield return new WaitForEndOfFrame();

            int width = (int)(CaptureArea.width - CaptureArea.x);
            int height = (int)(CaptureArea.height - CaptureArea.y);
            Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, true);
            texture.ReadPixels(CaptureArea, 0, 0);
            texture.Apply();

            Ratio.Active(true);
            Ratio.SetTexture(texture);
           

            yield return new WaitForEndOfFrame();
            GameManager.instance.ReadyToPlay();

        }

        public void ClearPlaneSport()
        {
            GoogleARCore.Examples.Common.DetectedPlaneVisualizer[] Sports = MonoBehaviour.FindObjectsOfType<GoogleARCore.Examples.Common.DetectedPlaneVisualizer>();
            if (Sports.Length > 0)
            {
                foreach (var s in Sports)
                {
                    MonoBehaviour.Destroy(s.gameObject);
                }

            }
        
        }

    }
}
