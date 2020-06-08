using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput 
{

    public bool ClickedOnUI
    {
        get
        {
            foreach (Touch touch in Input.touches)
            {
                int pointerID = touch.fingerId;
                if (EventSystem.current.IsPointerOverGameObject(pointerID))
                {
                    // at least on touch is over a canvas UI
                    return true;
                }
            }
            return false;
        }

    }

    
    public bool Moveing
    {
        get
        {
            if (Input.touchCount <= 0)
                return false;

            Touch touch = new Touch();
            int LastTouch = Input.touchCount - 1;
            touch = Input.GetTouch(LastTouch);
            if (touch.phase == TouchPhase.Began)
            {
                InstantPoint = touch.position;
            }

            return touch.phase == TouchPhase.Moved;
        }
    }

    private Vector2 InstantPoint = new Vector2();
    public Vector2 GetDirection()
    {
        Touch touch = new Touch();
        int LastTouch = Input.touchCount - 1;
        touch = Input.GetTouch(LastTouch);

        Vector2 ClickPos = touch.position;
        float x = 0;
        float y = 0;

        if (ClickPos.x != InstantPoint.x)
        {
            x = ClickPos.x - InstantPoint.x;
        }
        if (ClickPos.y != InstantPoint.y)
        {
            y = ClickPos.y - InstantPoint.y;
        }

        InstantPoint = ClickPos;

        return new Vector2(x, y);

    }



}
