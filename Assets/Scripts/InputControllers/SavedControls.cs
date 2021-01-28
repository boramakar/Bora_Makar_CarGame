using System;
using UnityEngine;

public class UserInput
{
    enum direction
    {
        left,
        right
    }

    enum action
    {
        start,
        stop
    }

    float delta;
}

public class SavedControls <ControlType>
{
    ControlType controls;
    SavedControls()
    {
    }
}
