using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum ControlType
{
    touch,
    keyboard
}

public enum Difficulty
{
    easy,
    normal,
    hard
}

public enum GraphicsQuality
{
    low,
    high
}

sealed class DataScript
{
    private static readonly DataScript instance = new DataScript();
    private DataScript() { }
    static DataScript() { }

    public static DataScript Instance { get { return instance; } }

    public float MovementSpeed { get; private set; }
    public float RotationSpeed { get; private set; }
    public bool SFXEnabled { get; set; }
    public bool MusicEnabled { get; set; }
    public Difficulty CurrentDifficulty { get; set; }
    public ControlType CurrentControlType { get; set; }
    public GraphicsQuality GFXQuality { get; set; }
    public float EasyMovementSpeed { get; set; }
    public float EasyRotationSpeed { get; set; }
    public float NormalMovementSpeed { get; set; }
    public float NormalRotationSpeed { get; set; }
    public float HardMovementSpeed { get; set; }
    public float HardRotationSpeed { get; set; }

    public void SetDifficulty()
    {
        switch(CurrentDifficulty)
        {
            case Difficulty.easy:
                MovementSpeed = EasyMovementSpeed;
                RotationSpeed = EasyRotationSpeed * 10f;
                break;
            case Difficulty.normal:
                MovementSpeed = NormalMovementSpeed;
                RotationSpeed = NormalRotationSpeed * 10f;
                break;
            case Difficulty.hard:
                MovementSpeed = HardMovementSpeed;
                RotationSpeed = HardRotationSpeed * 10f;
                break;
        }
    }
}
