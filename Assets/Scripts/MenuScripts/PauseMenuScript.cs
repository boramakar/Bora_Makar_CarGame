using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject gameController;

    private Button pauseBtn;
    private Button continueBtn;
    private Button resetLevelBtn;
    private Button mainMenuBtn;
    private GameObject overlay;
    private GameControllerScript gameControllerScript;
    private PlayerScript playerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameControllerScript = gameController.GetComponent<GameControllerScript>();
        pauseBtn = gameObject.transform.GetChild(0).GetComponent<Button>();
        overlay = gameObject.transform.GetChild(1).gameObject;
        continueBtn = overlay.transform.GetChild(0).GetComponent<Button>();
        resetLevelBtn = overlay.transform.GetChild(1).GetComponent<Button>();
        mainMenuBtn = overlay.transform.GetChild(2).GetComponent<Button>();

        pauseBtn.onClick.AddListener(PauseGame);
        continueBtn.onClick.AddListener(ContinueGame);
        resetLevelBtn.onClick.AddListener(ResetLevel);
        mainMenuBtn.onClick.AddListener(MainMenu);
        overlay.SetActive(false);
    }

    void PauseGame()
    {
        overlay.SetActive(true);
        gameControllerScript.Pause();
    }

    void ContinueGame()
    {
        overlay.SetActive(false);
        gameControllerScript.Continue();
    }

    void ResetLevel()
    {
        overlay.SetActive(false);
        gameControllerScript.ResetLevel();
    }

    void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
