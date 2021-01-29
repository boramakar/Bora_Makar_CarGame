using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private bool leftTurn, rightTurn;
    private static bool play; //if any car is playing, then all of them are playing
    private float adjustedSpeed, adjustedRotationSpeed;
    private GameControllerScript gameControllerScript;
    private List<Tuple<Vector3, Quaternion>> savedPositions;
    private SaveAndReplay savedControls;
    private int nextPosition;
    private bool isPlayer;
    private bool isGhost;
    private bool movable;
    private float lastActionTime;
    private float rotationAmount;

    public bool IsPlayer { get => isPlayer; set => isPlayer = value; }
    public bool IsGhost { get => isGhost; set => isGhost = value; }

    // Start is called before the first frame update
    void Start()
    {
        //initialize fields
        play = false;
        leftTurn = false;
        rightTurn = false;
        isPlayer = true;
        movable = true;
        gameControllerScript = GameObject.Find("GameSettings").GetComponent<GameControllerScript>();
        savedPositions = new List<Tuple<Vector3, Quaternion>>();
        savedControls = gameObject.GetComponent<SaveAndReplay>();
        nextPosition = 1;

        //adjust speed calculation formula if needed
        adjustedSpeed = gameControllerScript.playerSpeed / 10f;
        adjustedRotationSpeed = gameControllerScript.playerTurnSpeed / 10f;

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
                rotationAmount -= adjustedRotationSpeed;
            }
            if (rightTurn)
            {
                rotationAmount += adjustedRotationSpeed;
            }

            transform.Translate(new Vector3(0, 0, adjustedSpeed) * Time.deltaTime * (movable ? 1 : 0));
            transform.Rotate(0, rotationAmount, 0);
        }
    }

    //Used to record the player position for playback - Method 1
    //This might be very memory intensive, in that case switch to Method 2
    private void FixedUpdate()
    {
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

    public void TriggerLeft(bool record)
    {
        float time = Time.time;
        if (record)
        {
            savedControls.Add(new UserInput(UserInput.Direction.left, lastActionTime - time));
            lastActionTime = time;
        }
        leftTurn = !leftTurn;
    }

    public void TriggerRight(bool record)
    {
        float time = Time.time;
        if (record)
        {
            savedControls.Add(new UserInput(UserInput.Direction.right, lastActionTime - time));
            lastActionTime = time;
        }
        rightTurn = !rightTurn;
    }

    public void Play()
    {
        if (isPlayer)
            lastActionTime = Time.time; //record current time as Epoch
        /*else if (isGhost)
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

    public void Reset()
    {
        leftTurn = false;
        rightTurn = false;
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        if (isPlayer)
        {
            savedPositions.Clear();
        }
        else
        {
            nextPosition = 1;
        }
    }
}
