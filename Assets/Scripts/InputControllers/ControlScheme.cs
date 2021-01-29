using System;
using UnityEngine;

interface IControlScheme
{
    bool GetLeftDown();
    bool GetLeftUp();
    bool GetRightDown();
    bool GetRightUp();
    bool GetPause();
}
