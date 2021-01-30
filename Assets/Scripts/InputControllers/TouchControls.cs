using System;
using UnityEngine;

public class TouchControls : IControlScheme
{
    private static readonly TouchControls instance = new TouchControls();
    private TouchControls() { }
    static TouchControls() { }

    public static TouchControls Instance { get { return instance; } }
    public bool GetLeftDown()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began && touch.position.x < Screen.width / 2)
                    return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    public bool GetLeftUp()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Ended && touch.position.x < Screen.width / 2)
                    return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    public bool GetRightDown()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2)
                    return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    public bool GetRightUp()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Ended && touch.position.x > Screen.width / 2)
                    return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public bool GetPause()
    {
        return false;
    }
}

