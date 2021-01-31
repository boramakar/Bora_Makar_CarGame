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
    private bool replayMode;

    private void Awake()
    {
        controls = ControlManager.Instance.controlScheme;
        pathCount = paths.transform.childCount;
        replayMode = false;
    }

    void Start()
    {
        //Implement button functionality
        startBtn = mainCamera.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Button>();
        nextBtn = mainCamera.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Button>();
        startBtn.onClick.AddListener(StartGame);
        nextBtn.onClick.AddListener(NextLevel);
        nextBtn.gameObject.SetActive(false);

        //ResetLevel();
        SetParams();
        DisablePaths(1);
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

    public void Pause()
    {
        print("Paused the game!");
        startBtn.interactable = false;
        //startBtn.gameObject.SetActive(false);
        playerScript.Pause();
    }

    public void Continue()
    {
        print("Continuing the game now!");
        startBtn.interactable = true;
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
            replayMode = true;
            StartCoroutine(EndOfLevelReplay());
            nextBtn.gameObject.SetActive(true);
        }
    }

    public void ResetPath()
    {
        print("Resetting path for new attempt");
        ResetCars(false);
        startBtn.interactable = true;
        //startBtn.gameObject.SetActive(true);
    }

    public void ResetLevel()
    {
        print("Resetting level for new attempt");
        replayMode = false;
        currentPathIndex = 0;
        DisablePaths(1);
        ResetCars(true);
        SetParams();
        startBtn.interactable = true;
        //startBtn.gameObject.SetActive(true);
    }
    public void ResetCars(bool hardReset)
    {
        GameObject currentCar;
        for(int index = 0; index < (hardReset ? paths.transform.childCount : currentPathIndex + 1); index++)
        {
            Transform path = paths.transform.GetChild(index);
            currentCar = path.GetChild(0).GetChild(0).gameObject;
            currentCar.GetComponent<PlayerScript>().Reset(hardReset);
            if(hardReset)
            {
                path.GetChild(1).gameObject.SetActive(true); //enable markers
                currentCar.GetComponent<SphereCollider>().enabled = true; //Re-enable sphere collider for collision with end markers
                if (currentCar.TryGetComponent<CarCollisionScript>(out _) == false)
                {
                    currentCar.AddComponent<CarCollisionScript>(); //add collision script back
                }
                //Disable end marker for the completed path
                currentCar.transform.GetChild(0).gameObject.SetActive(true); //Re-enable lights for ghosts
                foreach (Material mat in currentCar.GetComponent<MeshRenderer>().materials) //Turn car color back to normal
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1f);
                }
                currentCar.GetComponent<PlayerScript>().IsPlayer = false;
                currentCar.GetComponent<PlayerScript>().IsGhost = false;
            }
        }
    }

    public void DisablePaths(int index)
    {
        Transform tempPath;
        //Disable irrelevant paths
        for (int i = index; i < pathCount; i++)
        {
            tempPath = paths.transform.GetChild(i);
            tempPath.gameObject.SetActive(false);
        }
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
        while (replayMode)
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
        pauseMenu.transform.GetChild(1).gameObject.SetActive(true);
    }
}
