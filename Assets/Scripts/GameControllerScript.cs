using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameControllerScript : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject paths;
    public GameObject pauseMenu;

    private int pathCount;
    private int currentPathIndex;
    private Transform currentPath;
    private GameObject player;
    private PlayerScript playerScript;
    private IControlScheme controls;
    private Button startBtn;
    private Button nextBtn;
    private DataScript dataScript;

    private void Awake()
    {
        dataScript = DataScript.Instance;
        controls = ControlManager.Instance.controlScheme;
    }

    void Start()
    {
        //Set path params
        pathCount = paths.transform.childCount;
        currentPathIndex = 0;
        SetParams();

        Transform tempPath;
        //Disable irrelevant paths
        for (int i = 1; i < pathCount; i++)
        {
            tempPath = paths.transform.GetChild(i);
            tempPath.gameObject.SetActive(false);
        }

        //Implement "Start on Touch" functionality
        startBtn = mainCamera.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Button>();
        nextBtn = mainCamera.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Button>();
        startBtn.onClick.AddListener(StartGame);
        nextBtn.onClick.AddListener(NextLevel);
        nextBtn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Input controls
        if (controls.GetLeftDown())
        {
            playerScript.TurnLeft(true);
        }
        if (controls.GetLeftUp())
        {
            playerScript.StopLeft(true);
        }
        if (controls.GetRightDown())
        {
            playerScript.TurnRight(true);
        }
        if (controls.GetRightUp())
        {
            playerScript.StopRight(true);
        }
        if (controls.GetPause())
        {
            playerScript.Pause();
            DisplayMenu();
        }
    }

    private void SetParams()
    {
        currentPath = paths.transform.GetChild(currentPathIndex);
        player = currentPath.GetChild(0).GetChild(0).gameObject;
        playerScript = player.GetComponent<PlayerScript>();
        playerScript.IsPlayer = true;
    }

    void StartGame()
    {
        print("Starting the game now!");
        startBtn.interactable = false;
        //startBtn.gameObject.SetActive(false);
        playerScript.Play();
    }

    public void CompletePath()
    {
        if (currentPathIndex < pathCount - 1) //Finished the current path but there are more
        {
            print("Completed path: " + currentPath.ToString());
            //Handle last played car
            MakeGhost();
            //move to next path
            currentPathIndex++;
            SetParams();
            currentPath.gameObject.SetActive(true);
            ResetPath();
        }
        else //Finished the level
        {
            MakeGhost();
            ResetPath();
            StartCoroutine(EndOfLevelReplay());
            nextBtn.gameObject.SetActive(true);
        }
    }

    public void ResetPath()
    {
        print("Resetting path for new attempt");
        foreach (Transform path in paths.transform)
        {
            path.GetChild(0).GetChild(0).GetComponent<PlayerScript>().Reset();
        }
        startBtn.interactable = true;
        //startBtn.gameObject.SetActive(true);
    }

    void MakeGhost()
    {
        player.GetComponent<SphereCollider>().enabled = false; //Disable sphere collider to stop collision with end markers
                                                               //! Destroy collision script to ignore collisions between ghosts or a ghost and end marker
                                                               //! This might need to change if reset level is implemented or the script needs to get attached again to all paths before current during reset
        Destroy(player.GetComponent<CarCollisionScript>());
        currentPath.GetChild(1).gameObject.SetActive(false); //Disable end marker for the completed path
        player.transform.GetChild(0).gameObject.SetActive(false); //Disable lights for ghosts
                                                                  //Make ghosts more ghost like
        foreach (Material mat in player.GetComponent<MeshRenderer>().materials)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.8f);
        }
        playerScript.IsPlayer = false;
        playerScript.IsGhost = true;
    }

    IEnumerator EndOfLevelReplay()
    {
        while (true)
        {
            playerScript.Play();
            yield return new WaitForSeconds(5); //Duration of replay
            ResetPath();
        }
    }

    void NextLevel()
    {
        //Scene Animation
        if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Load next scene
        }
        else
        {
            print("End of demo!");
        }
    }

    void DisplayMenu()
    {
        pauseMenu.SetActive(true);
    }
}
