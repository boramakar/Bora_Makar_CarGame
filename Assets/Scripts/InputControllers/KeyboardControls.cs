using System;
using UnityEngine;

public class KeyboardControls : ControlScheme
{
    private KeyCode leftKey, rightKey;
    public KeyboardControls()
    {
        leftKey = KeyCode.LeftArrow;
        rightKey = KeyCode.RightArrow;
    }
    public bool GetLeftDown()
    {
        if (Input.GetKeyDown(leftKey))
            return true;
        else
            return false;
    }
    public bool GetLeftUp()
    {
        if (Input.GetKeyUp(leftKey))
            return true;
        else
            return false;
    }
    public bool GetRightDown()
    {
        if (Input.GetKeyDown(rightKey))
            return true;
        else
            return false;
    }
    public bool GetRightUp()
    {
        if (Input.GetKeyUp(rightKey))
            return true;
        else
            return false;
    }
}
