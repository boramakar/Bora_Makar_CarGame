using System;
using UnityEngine;

public class KeyboardControls : IControlScheme
{
    private static readonly KeyboardControls instance = new KeyboardControls();
    private KeyboardControls() {
        leftKey = KeyCode.LeftArrow;
        rightKey = KeyCode.RightArrow;
    }
    static KeyboardControls() { }

    public static KeyboardControls Instance { get { return instance; } }

    private readonly KeyCode leftKey, rightKey;

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

    public bool GetPause()
    {
        return false;
    }
}
