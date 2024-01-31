using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    bool carCheck;

    [HideInInspector]
    public GameObject[] obs;
    [Header("Texts")]
    public TMP_Text HEADER_scoreText;
    public TMP_Text GAMEOVER_PANEL_gameOverScoreText;
    public TMP_Text PANEL_pauseText;
    public TMP_Text PANEL_pause_HİGHScoreText;
    public TMP_Text youWinText;
    public TMP_Text youWinCoinText;
    [Header("Trophy Score")]
    [SerializeField] private int TROPHY_highScore_ınt;
    [HideInInspector]
    public Image[] g;
    public GameObject heartEffect;
    private int count;
    //public Text gameover;
    public GameObject gameOverUI;
    public GameObject pausedUI;
    public GameObject PauseBtn;
    public GameObject ScoreObject;
    public GameObject InfoObject;
    //public Button tapButton;
    public float spawnInterval;
    public int AdCount;

    [SerializeField] private GameObject mainHeader;
    [SerializeField] private string isTutorial = "";
    [SerializeField] public int coin;
    [Header("Scene Settings")]
    [SerializeField] private string TROPHY_NAME = "HighScore";
    [SerializeField] private string TROPHY_EASY_STRING;
    [SerializeField] public int levelPassed_Or_NotPassedInt;
    [SerializeField] public int ıncreaseInt;
    [Header("Countdown")]
    [SerializeField] private GameObject countdownInt;
    [SerializeField] private GameObject countdownText;
    [SerializeField] private TMP_Text countdownINT_TEXT;
    [SerializeField] private float ball_countdownInt;
    public bool isCountdown = true;
    public bool isTutorialOn = false;
    private bool saveFile;

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        
        Application.targetFrameRate = 60;
        instance = this;
        
        //gameoverScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

    }
    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        saveFile = SaveManager.i.checkSaveInt(levelPassed_Or_NotPassedInt);
        StartCoroutine(countdownToStart());
        conTextChecker();



        AdCount = PlayerPrefs.GetInt("AdCount", 0);
        count = 0;
        carCheck = false;

    }

    private IEnumerator countdownToStart()
    {
        isCountdown = true;
        checkTutorial();
        yield return new WaitUntil(() => isTutorialOn == true);
        countdownInt.SetActive(true);
        PauseBtn.GetComponent<Button>().enabled = false;
        int i = 3;
        while (i > 0)
        {
            countdownINT_TEXT.text = i.ToString();
            yield return new WaitForSeconds(1f);
            i--;
        }
        countdownInt.SetActive(false);
        countdownText.SetActive(true);

        yield return new WaitForSeconds(.5f);
        countdownText.SetActive(false);
        isCountdown = false;
        PauseBtn.GetComponent<Button>().enabled = true;

    }
    public void checkTutorial()
    {

        string getString = PlayerPrefs.GetString("tutorialStr", "");

        if (getString == "tutorialPassed")
        {
            isTutorialOn = true;
            return;
        }
        Time.timeScale = 0;
        isTutorial = "tutorialPassed";
        PlayerPrefs.SetString("tutorialStr", isTutorial);
        InfoObject.SetActive(true);
    }

    private void conTextChecker()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
        switch (saveFile)
        {
            case false:
                {
                    if (coin >= 10000)
                    {
                        HEADER_scoreText.text = (coin / 10000).ToString("n0") + "K" + " / " + LevelManager.i._levelStringPrice;
                        break;
                    }
                    else if (coin >= 1000)
                    {
                        HEADER_scoreText.text = (coin / 1000).ToString("n1") + "K" + " / " + LevelManager.i._levelStringPrice;
                        break;
                    }
                    HEADER_scoreText.text = coin.ToString() + " / " + LevelManager.i._levelStringPrice;
                    break;
                }
            case true:
                {
                    if (coin >= 1000)
                    {
                        HEADER_scoreText.text = (coin / 1000).ToString("n0") + "K";
                        break;
                    }
                    else if (coin >= 10000)
                    {
                        HEADER_scoreText.text = (coin / 10000).ToString("n1") + "K";
                        break;
                    }
                    HEADER_scoreText.text = coin.ToString();
                    break;
                }

        }

    }
    public void youWinPause()
    {
        StopCoroutine(nameof(waitToAccess));
        Time.timeScale = 0.95f;


        PauseBtn.SetActive(false);
        foreach (GameObject item in obs)
        {

            Destroy(item);
        }
        obs = null;

        PlayerPrefs.SetInt("coin", coin);
        youWinCoinText.text = coin.ToString();

        if (TROPHY_highScore_ınt > PlayerPrefs.GetInt(TROPHY_EASY_STRING, 0))
        {
            PlayerPrefs.SetInt(TROPHY_EASY_STRING, TROPHY_highScore_ınt);

        }

        youWinText.text = PlayerPrefs.GetInt(TROPHY_EASY_STRING, TROPHY_highScore_ınt).ToString();

        LevelManager.i.finishGamePanel();
    }
    public void Pause()
    {


        StopCoroutine(nameof(waitToAccess));
        Time.timeScale = 0.95f;


        PauseBtn.SetActive(false);
        foreach (GameObject item in obs)
        {

            Destroy(item);
        }
        obs = null;


        PlayerPrefs.SetInt("coin", coin);
        PANEL_pauseText.text = coin.ToString();

        if (TROPHY_highScore_ınt > PlayerPrefs.GetInt(TROPHY_EASY_STRING, 0))
        {
            PlayerPrefs.SetInt(TROPHY_EASY_STRING, TROPHY_highScore_ınt);

        }

        GAMEOVER_PANEL_gameOverScoreText.text = PlayerPrefs.GetInt(TROPHY_EASY_STRING, TROPHY_highScore_ınt).ToString();
        ScoreObject.SetActive(false);


        gameOverUI.SetActive(true);

        // if (AdCount == 3)
        // {
        //     Time.timeScale = 0.95f;
        //     FindObjectOfType<AdManager>().CallVideoAd();
        //     AdCount = -1;
        //     PlayerPrefs.SetInt("AdCount", AdCount);
        //     Debug.Log("Ad Count S�f�rland� -> " + AdCount);
        //     return;
        // }
        // AdCount++;
        // Debug.Log("Ad Count Artt� -> " + AdCount);
        // PlayerPrefs.SetInt("AdCount", AdCount);

    }

    public void Play()
    {
        count = 0;
        coin = PlayerPrefs.GetInt("coin", 0);
        HEADER_scoreText.text = coin.ToString();
        Time.timeScale = 1;
    }
    public void PauseButton()
    {
        Sounds.instance.playOneSound(3);
        Time.timeScale = 0.95f;
        PANEL_pause_HİGHScoreText.text = PlayerPrefs.GetInt(TROPHY_EASY_STRING, TROPHY_highScore_ınt).ToString();
        PauseBtn.SetActive(false);
        pausedUI.SetActive(true);
        Sounds.instance.UpdateIconButton();
    }
    public void CloseButton()
    {
        Sounds.instance.playOneSound(3);
        pausedUI.SetActive(false);
        PauseBtn.SetActive(true);

        Time.timeScale = 1;
    }

    public void InfoButton()
    {
        gameOverUI.SetActive(false);
        InfoObject.SetActive(true);
    }

    public void InfoPlayButton()
    {

        InfoObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Play();
    }

    public void IncreaseScore()
    {
        TROPHY_highScore_ınt += Random.Range(35, 65);

        coin += ıncreaseInt;

        switch (saveFile)
        {
            case true:
                {
                    if (coin > 1000)
                    {
                        HEADER_scoreText.text = (coin / 1000).ToString("n0") + "K";
                        break;
                    }
                    else if (coin > 10000)
                    {
                        HEADER_scoreText.text = (coin / 10000).ToString("n1") + "K";
                        break;
                    }
                    HEADER_scoreText.text = coin.ToString();
                    break;
                }
            case false:
                {
                    if (coin >= LevelManager.i._levelPrice)
                    {
                        youWinPause();
                    }
                    if (coin > 1000)
                    {
                        HEADER_scoreText.text = (coin / 1000).ToString("n0") + "K" + " / " + LevelManager.i._levelStringPrice;
                        break;
                    }
                    else if (coin > 10000)
                    {
                        HEADER_scoreText.text = (coin / 10000).ToString("n1") + "K" + " / " + LevelManager.i._levelStringPrice;
                        break;
                    }
                    HEADER_scoreText.text = coin.ToString() + " / " + LevelManager.i._levelStringPrice;
                    break;
                }
        }

    }
    private void Update()
    {

        obs = GameObject.FindGameObjectsWithTag("obs");

        if (!carCheck)
        {
            StartCoroutine(waitToAccess());
            carCheck = true;
        }

        //if (isDecrease) decreaseTimeFadeOut();
    }

    private void decreaseTimeFadeOut()
    {
        foreach (var item in obs)
        {
            if (item.GetComponent<obstacle>() == null)
                return;

            item.GetComponent<obstacle>().timeToFade = 1.5f;

        }
    }

    IEnumerator waitToAccess()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("bekle");
        yield return new WaitUntil(() => isCountdown == false);
        yield return new WaitForSeconds(.5f);
        while (true)
        {
            if (obs.Length == 0) { Debug.Log("Obs is Empty"); yield break; }

            GameObject obj = obs[Random.Range(0, obs.Length)];

            if (!obj) { yield return new WaitForSeconds(0.1f); }



            if (obj.GetComponent<obstacle>() != null && obj != null)
            {

                if (obj.transform.GetComponent<obstacle>().isAwake == true)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                else
                {
                    // İlk Bölüm 3f
                    // isShortTime 2f
                    // isThird 1.5f
                    // isFour 1.25f
                    // isFive 1.1f
                    obj.GetComponent<obstacle>().timeToFade = ball_countdownInt;

                    obj.tag = "ObsOut";
                    StartCoroutine(obj.GetComponent<obstacle>().fadeIn());
                    yield return new WaitForSeconds(spawnInterval);
                }
            }
            else if (obj.GetComponent<NotObstacle>() != null)
            {
                if (obj.GetComponent<NotObstacle>().isNotAwake == true)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                else
                {
                    obj.tag = "ObsOut";
                    StartCoroutine(obj.GetComponent<NotObstacle>().fadeIn());
                    yield return new WaitForSeconds(spawnInterval);

                }
            }
        }
    }


    public void hideHeart()
    {
        if (count == 2)
        {

            Debug.Log("GAME OVER");
            g[2].GetComponent<Image>().color = new Color32(109, 109, 109, 255);

            mainHeader.SetActive(false);
            StartCoroutine(gameOverHeart());
            //Pause();
            return;
        }
        g[count].GetComponent<Image>().color = new Color32(109, 109, 109, 255);
        StartCoroutine(redHeart());
        count++;


    }
    private IEnumerator gameOverHeart()
    {

        Sounds.instance.playOneSound(2);

        heartEffect.SetActive(true);
        yield return new WaitForSeconds(.3f);
        heartEffect.SetActive(false);
        Pause();
    }
    private IEnumerator redHeart()
    {
        Sounds.instance.playOneSound(2);
        heartEffect.SetActive(true);
        yield return new WaitForSeconds(.3f);
        heartEffect.SetActive(false);
    }
    public void homeButton()
    {
        Sounds.instance.playOneSound(3);
        SceneManager.LoadScene(0);
    }
    public void TryAgain()
    {
        Sounds.instance.playOneSound(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Play();
    }



}

