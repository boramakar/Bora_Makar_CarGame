using System;
using UnityEngine;

public interface IControlScheme
{
    bool GetLeftDown();
    bool GetLeftUp();
    bool GetRightDown();
    bool GetRightUp();
    bool GetPause();
}