    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private bool leftTurn, rightTurn;
    private static bool play; //if any car is playing, then all of them are playing
    private float adjustedSpeed, adjustedRotationSpeed;
    private GameControllerScript canvasScript;
    private bool isPlayer;
    private ArrayList savedPositions;
    private int nextPosition;

    public bool IsPlayer { get => isPlayer; set => isPlayer = value; }

    // Start is called before the first frame update
    void Start()
    {
        //initialize fields
        play = false;
        leftTurn = false;
        rightTurn = false;
        canvasScript = GameObject.Find("Canvas").GetComponent<GameControllerScript>();
        savedPositions = new ArrayList();
        nextPosition = 1;
        
        //adjust speed calculation formula if needed
        adjustedSpeed = canvasScript.playerSpeed / 10f;
        adjustedRotationSpeed = canvasScript.playerTurnSpeed / 10f;
        
        //DEBUG
        /*print("Speed: " + canvasScript.playerSpeed.ToString());
        print("AdjustedSpeed: " + adjustedSpeed.ToString());
        print("RotationSpeed: " + canvasScript.playerTurnSpeed.ToString());
        print("AdjustedRotationSpeed: " + adjustedRotationSpeed.ToString());*/
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            float rotationAmount = 0;
            if(leftTurn)
            {
                rotationAmount -= adjustedRotationSpeed;
            }
            if(rightTurn)
            {
                rotationAmount += adjustedRotationSpeed;
            }

            transform.Translate(new Vector3(0, 0, adjustedSpeed) * Time.deltaTime);
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
                savedPositions.Add(gameObject.transform.position);
            else
                gameObject.transform.position = (Vector3)savedPositions[nextPosition++];
        }
    }

    public void TriggerLeft()
    {
        leftTurn = !leftTurn;
    }

    public void TriggerRight()
    {
        rightTurn = !rightTurn;
    }

    public void Play()
    {
        play = true;
    }

    public void Pause()
    {
        play = false;
    }

    public void Reset()
    {
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        if(isPlayer)
        {
            savedPositions.Clear();
        }
        else
        {
            nextPosition = 1;
        }
    }
}
