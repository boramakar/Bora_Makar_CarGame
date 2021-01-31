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
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        pauseBtn = gameObject.transform.GetChild(0).GetComponent<Button>();
        overlay = gameObject.transform.GetChild(1).gameObject;
        continueBtn = overlay.transform.GetChild(0).GetComponent<Button>();
        resetLevelBtn = overlay.transform.GetChild(1).GetComponent<Button>();
        mainMenuBtn = overlay.transform.GetChild(2).GetComponent<Button>();

        pauseBtn.onClick.AddListener(PauseGame);
        continueBtn.onClick.AddListener(ContinueGame);
        resetLevelBtn.onClick.AddListener(ResetLevel);
        mainMenuBtn.onClick.AddListener(MainMenu);
    }

    void PauseGame()
    {
        overlay.SetActive(true);
        playerScript.Pause();
    }

    void ContinueGame()
    {
        overlay.SetActive(false);
        StartCoroutine(UnpauseGame());
    }

    IEnumerator UnpauseGame()
    {
        yield return new WaitForSeconds(1);
        playerScript.Play();
    }

    void ResetLevel()
    {
        gameControllerScript.ResetLevel();
    }

    void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
