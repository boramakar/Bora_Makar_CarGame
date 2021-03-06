﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private bool leftTurn, rightTurn;
    private static bool play; //if any car is playing, then all of them are playing
    private List<Tuple<Vector3, Quaternion>> savedPositions;
    private SaveAndReplay savedControls;
    private int nextPosition;
    private bool isPlayer;
    private bool isGhost;
    private bool movable;
    private float lastActionTime;
    private float rotationAmount;
    private DataScript dataScript;

    public bool IsPlayer { get => isPlayer; set => isPlayer = value; }
    public bool IsGhost { get => isGhost; set => isGhost = value; }

    private void Awake()
    {
        //initialize fields
        play = false;
        leftTurn = false;
        rightTurn = false;
        movable = true;
        savedPositions = new List<Tuple<Vector3, Quaternion>>();
        savedControls = gameObject.GetComponent<SaveAndReplay>();
        nextPosition = 1;
        isPlayer = false;
        dataScript = DataScript.Instance;

        //make transparent/fade materials display properly
        foreach (Material mat in gameObject.GetComponent<MeshRenderer>().materials)
        {
            mat.SetInt("_ZWrite", 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //DEBUG
        /*print("Speed: " + canvasScript.playerSpeed.ToString());
        print("AdjustedSpeed: " + adjustedSpeed.ToString());
        print("RotationSpeed: " + canvasScript.playerTurnSpeed.ToString());
        print("AdjustedRotationSpeed: " + adjustedRotationSpeed.ToString());*/
    }

    // Update is called once per frame
    void Update()
    {
        if (play && isPlayer)
        {
            rotationAmount = 0;
            if (leftTurn)
            {
                rotationAmount -= dataScript.RotationSpeed;
            }
            if (rightTurn)
            {
                rotationAmount += dataScript.RotationSpeed;
            }

            transform.Translate(new Vector3(0, 0, dataScript.MovementSpeed) * Time.deltaTime * (movable ? 1 : 0));
            transform.Rotate(0, rotationAmount * Time.deltaTime, 0);
            //print("Total rotation: " + (rotationAmount * Time.deltaTime).ToString("0.000000"));
        }
    }

    //Used to record the player position for playback - Method 1
    //This might be very memory intensive, in that case switch to Method 2
    private void FixedUpdate()
    {
        //Method 1 - Working
        if (play)
        {
            if (isPlayer)
                savedPositions.Add(new Tuple<Vector3, Quaternion>(gameObject.transform.position, gameObject.transform.rotation));
            else if (nextPosition < savedPositions.Count)
            {
                gameObject.transform.position = (Vector3)savedPositions[nextPosition].Item1;
                gameObject.transform.rotation = (Quaternion)savedPositions[nextPosition++].Item2;
            }
        }
    }

    public void TurnLeft(bool record)
    {
        float time = Time.time;
        if (record)
        {
            savedControls.Add(new UserInput("TurnLeft", lastActionTime - time));
            lastActionTime = time;
        }
        leftTurn = true;
    }

    public void StopLeft(bool record)
    {
        float time = Time.time;
        if (record)
        {
            savedControls.Add(new UserInput("StopLeft", lastActionTime - time));
            lastActionTime = time;
        }
        leftTurn = false;
    }

    public void TurnRight(bool record)
    {
        float time = Time.time;
        if (record)
        {
            savedControls.Add(new UserInput("TurnRight", lastActionTime - time));
            lastActionTime = time;
        }
        rightTurn = true;
    }

    public void StopRight(bool record)
    {
        float time = Time.time;
        if (record)
        {
            savedControls.Add(new UserInput("StopRight", lastActionTime - time));
            lastActionTime = time;
        }
        rightTurn = false;
    }

    public void Play()
    {
        //Method 2 - Never tested
        /*if (isPlayer)
            lastActionTime = Time.time; //record current time as Epoch
        else if (isGhost)
        {
            savedControls.PlayInputs();
        }*/
        play = true;
    }

    public void Pause()
    {
        play = false;
    }

    public void Freeze()
    {
        movable = false;
    }

    public void Reset(bool force)
    {
        leftTurn = false;
        rightTurn = false;
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        if ((isPlayer || force) && savedPositions != null)
        {
            savedPositions.Clear();
        }
        else
        {
            nextPosition = 1;
        }
    }
}
