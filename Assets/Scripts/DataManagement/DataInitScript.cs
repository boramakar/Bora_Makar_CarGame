using System;
using UnityEngine;

public class DataInitScript : MonoBehaviour
{
    public bool sfxEnabled;
    public bool musicEnabled;
    public ControlType controlType;
    public Difficulty difficulty;
    public GraphicsQuality graphicsQuality;
    public float easyMovementSpeed;
    public float easyRotationSpeed;
    public float normalMovementSpeed;
    public float normalRotationSpeed;
    public float hardMovementSpeed;
    public float hardRotationSpeed;

    private void Start()
    {
        DataScript instance = DataScript.Instance;

        instance.SFXEnabled = sfxEnabled;
        instance.MusicEnabled = musicEnabled;
        instance.CurrentControlType = controlType;
        instance.CurrentDifficulty = difficulty;
        instance.GFXQuality = graphicsQuality;
        instance.EasyMovementSpeed = easyMovementSpeed;
        instance.EasyRotationSpeed = easyRotationSpeed;
        instance.NormalMovementSpeed = normalMovementSpeed;
        instance.NormalRotationSpeed = normalRotationSpeed;
        instance.HardMovementSpeed = hardMovementSpeed;
        instance.HardRotationSpeed = hardRotationSpeed;
        instance.SetDifficulty();
    }
}