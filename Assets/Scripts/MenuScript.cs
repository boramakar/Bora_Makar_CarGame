using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject views;
    public GameObject underlinePrefab;

    private GameObject menuView;
    private GameObject helpView;
    private GameObject settingsView;
    private Button startBtn;
    private Button settingsBtn;
    private Button helpBtn;
    private Button closeHelpBtn;
    private Button closeSettingsBtn;
    private Button easyBtn;
    private Button normalBtn;
    private Button hardBtn;
    private Button touchBtn;
    private Button keyboardBtn;
    private DataScript dataScript;
    private GameObject inputUnderline, difficultyUnderline;
    private int levelID;

    void Awake()
    {
        //initialize variables for easy access in the future
        dataScript = DataScript.Instance;
        menuView = views.transform.GetChild(0).gameObject;
        settingsView = views.transform.GetChild(1).gameObject;
        helpView = views.transform.GetChild(2).gameObject;
        startBtn = menuView.transform.GetChild(0).gameObject.GetComponent<Button>();
        settingsBtn = menuView.transform.GetChild(1).gameObject.GetComponent<Button>();
        helpBtn = menuView.transform.GetChild(2).gameObject.GetComponent<Button>();
        closeHelpBtn = helpView.transform.GetChild(0).gameObject.GetComponent<Button>();
        touchBtn = settingsView.transform.GetChild(1).gameObject.GetComponent<Button>();
        keyboardBtn = settingsView.transform.GetChild(2).gameObject.GetComponent<Button>();
        easyBtn = settingsView.transform.GetChild(3).gameObject.GetComponent<Button>();
        normalBtn = settingsView.transform.GetChild(4).gameObject.GetComponent<Button>();
        hardBtn = settingsView.transform.GetChild(5).gameObject.GetComponent<Button>();
        closeSettingsBtn = settingsView.transform.GetChild(6).gameObject.GetComponent<Button>();
        //Make sure MenuView is the first visible UI
        Hide();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Attach button functionalities
        startBtn.onClick.AddListener(StartGame);
        settingsBtn.onClick.AddListener(() => { Show(0); });
        helpBtn.onClick.AddListener(() => { Show(1); });
        closeHelpBtn.onClick.AddListener(Hide);
        closeSettingsBtn.onClick.AddListener(Hide);
        touchBtn.onClick.AddListener(() => { dataScript.CurrentControlType = ControlType.touch; inputUnderline.transform.position = touchBtn.transform.position; });
        keyboardBtn.onClick.AddListener(() => { dataScript.CurrentControlType = ControlType.keyboard; inputUnderline.transform.position = keyboardBtn.transform.position; });
        easyBtn.onClick.AddListener(() => { dataScript.CurrentDifficulty = Difficulty.easy; dataScript.SetDifficulty(); difficultyUnderline.transform.position = easyBtn.transform.position; });
        normalBtn.onClick.AddListener(() => { dataScript.CurrentDifficulty = Difficulty.normal; dataScript.SetDifficulty(); difficultyUnderline.transform.position = normalBtn.transform.position; });
        hardBtn.onClick.AddListener(() => { dataScript.CurrentDifficulty = Difficulty.hard; dataScript.SetDifficulty(); difficultyUnderline.transform.position = hardBtn.transform.position; });

        //Underline the active settings
        Vector3 inputUnderlinePosition, difficultyUnderlinePosition;
        //
        if (dataScript.CurrentControlType == ControlType.touch)
            inputUnderlinePosition = touchBtn.transform.position;
        else
            inputUnderlinePosition = keyboardBtn.transform.position;

        if (dataScript.CurrentDifficulty == Difficulty.easy)
            difficultyUnderlinePosition = easyBtn.transform.position;
        else if (dataScript.CurrentDifficulty == Difficulty.normal)
            difficultyUnderlinePosition = normalBtn.transform.position;
        else
            difficultyUnderlinePosition = hardBtn.transform.position;

        inputUnderline = Instantiate(underlinePrefab, inputUnderlinePosition, Quaternion.identity, settingsView.transform);
        difficultyUnderline = Instantiate(underlinePrefab, difficultyUnderlinePosition, Quaternion.identity, settingsView.transform);

        levelID = 1;
    }

    private void StartGame()
    {
        //For a level selector, implement a button grid and set levelNumber according to the selected level before loading scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + levelID);
    }

    private void Show(int index)
    {
        menuView.SetActive(false);
        helpView.SetActive(false);
        settingsView.SetActive(false);
        switch(index)
        {
            case 0:
                settingsView.SetActive(true);
                break;
            case 1:
                helpView.SetActive(true);
                break;
        }
    }

    private void Hide()
    {
        menuView.SetActive(true);
        helpView.SetActive(false);
        settingsView.SetActive(false);
    }
}
