using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameControllerScript : MonoBehaviour
{
    public enum ControlType
    {
        Keyboard,
        Touch
    }
    public Button startBtn;
    public Button leftBtn;
    public Button rightBtn;
    public GameObject paths;
    public ControlType controlType;
    public float playerSpeed;
    public float playerTurnSpeed;

    private int pathCount;
    private int currentPathIndex;
    private Transform currentPath;
    private GameObject player;
    private PlayerScript playerScript;
    private ControlScheme controls;
    private ArrayList inputArray;

    // Start is called before the first frame update
    void startGame()
    {
        print("Starting the game now!");
        startBtn.interactable = false;
        startBtn.gameObject.SetActive(false);
        playerScript.Play();
    }
    public void LeftTurn(bool start)
    {
        //save time of left turn
        //tell player to turn left
        playerScript.TriggerLeft();

    }
    public void RightTurn(bool start)
    {
        //save time of right turn
        //tell player to turn right
        playerScript.TriggerRight();
    }

    private void Awake()
    {
        //can be used for pathCount initialization but not necessary
        if (controlType == ControlType.Keyboard)
        {
            controls = new KeyboardControls();
        }
        else
        {
            controls = new TouchControls();
        }
    }

    void Start()
    {
        //Set path params
        pathCount = paths.transform.childCount;
        currentPathIndex = 0;
        
        //Disable irrelevant paths
        for (int i = 1; i < pathCount; i++)
        {
            paths.transform.GetChild(i).gameObject.SetActive(false);
        }

        //Set path specific params
        SetParams();

        //Implement "Start on Touch" functionality
        startBtn.onClick.AddListener(startGame);
    }

    private void SetParams()
    {
        currentPath = paths.transform.GetChild(currentPathIndex);
        player = currentPath.GetChild(0).GetChild(0).gameObject;
        playerScript = player.GetComponent<PlayerScript>();
        player.GetComponent<SphereCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        //Input controls
        if (controls.GetLeftDown())
        {
            LeftTurn(true);
        }
        else if (controls.GetLeftUp())
        {
            LeftTurn(false);
        }
        if (controls.GetRightDown())
        {
            RightTurn(true);
        }
        else if (controls.GetRightUp())
        {
            RightTurn(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Reset path
        playerScript.Pause();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerScript.Pause();
        CompletePath();
    }

    void CompletePath()
    {
        if(currentPathIndex < pathCount - 1)
        {
            //Disable the end point for the current
            currentPath.GetChild(2).GetChild(0).gameObject.SetActive(false);
            //send all saved events to 

            //move to next path
            currentPathIndex++;
            currentPath = paths.transform.GetChild(currentPathIndex);
            currentPath.gameObject.SetActive(true);
        }
    }
}
