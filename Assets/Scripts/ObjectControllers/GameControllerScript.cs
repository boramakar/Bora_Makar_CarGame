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
    private IControlScheme controls;

    // Start is called before the first frame update
    void StartGame()
    {
        print("Starting the game now!");
        startBtn.interactable = false;
        startBtn.gameObject.SetActive(false);
        playerScript.Play();
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

        Transform tempPath;
        //Disable irrelevant paths
        for (int i = 1; i < pathCount; i++)
        {
            tempPath = paths.transform.GetChild(i);
            tempPath.gameObject.SetActive(false);
            tempPath.GetChild(0).GetChild(0).gameObject.GetComponent<PlayerScript>().IsPlayer = false;
        }

        //Set path specific params
        SetParams();

        //Implement "Start on Touch" functionality
        startBtn.onClick.AddListener(StartGame);
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
            playerScript.TriggerLeft(true);
        }
        else if (controls.GetLeftUp())
        {
            playerScript.TriggerLeft(true);
        }
        if (controls.GetRightDown())
        {
            playerScript.TriggerRight(true);
        }
        else if (controls.GetRightUp())
        {
            playerScript.TriggerRight(true);
        }
    }

    public void CompletePath()
    {
        if(currentPathIndex < pathCount - 1) //Finished the current path but there are more
        {
            //Handle last played car
            player.GetComponent<SphereCollider>().gameObject.SetActive(false); //Disable sphere collider to stop collision with end markers
            currentPath.GetChild(1).gameObject.SetActive(false); //Disable end marker for the completed path
            playerScript.IsPlayer = false;
            playerScript.IsGhost = true;
            //move to next path
            currentPathIndex++;
            currentPath = paths.transform.GetChild(currentPathIndex);
            currentPath.gameObject.SetActive(true);
            player = currentPath.GetChild(0).GetChild(0).gameObject;
            playerScript = player.GetComponent<PlayerScript>();
            startBtn.interactable = true;
            startBtn.gameObject.SetActive(true);
        }
        else //Finished the level
        {

        }
    }

    public void ResetPath()
    {
        print("Resetting path for new attempt");
        foreach(Transform path in paths.transform)
        {
            path.GetChild(0).GetChild(0).GetComponent<PlayerScript>().Reset();
        }
        startBtn.interactable = true;
        startBtn.gameObject.SetActive(true);
    }
}
