using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    const string HIGH_SCORE_PLAYER_PREF = "HighScore";
    const string TOTAL_PUNYA_PREF = "TotalPunya";

    public int GetMaxTime() { return maxTime; }
    public int GetExtraTimeAsReward() { return extraTimeAsReward; }
    [SerializeField]
    private int extraTimeAsReward = 30;
    [SerializeField]
    private int maxTime = 120;
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private GameObject gameLoseMenu;
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private GameObject highScoreGameOverMenu;
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private AdManager adManager;
    [SerializeField]
    private Player player;

    private DeliveryBoy deliveryBoy;
    private bool onWatchAdButtonClicked = false;

    public bool WasWatchAdButtonClicked()
    {
        return onWatchAdButtonClicked;
    }

    public void ResetWatchAdButtonClickedBool()
    {
        onWatchAdButtonClicked = false;
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        if(player != null)
        {
            deliveryBoy = player.GetComponent<DeliveryBoy>();
        }
    }

    public int GetNumOfOrdersForSuccessNum(int successNum)
    {
        if(successNum == 0)
        {
            return 1;
        }

        int numofOrders = Mathf.Clamp(successNum, 0, 7);

        return Random.Range(numofOrders, numofOrders + 1);
    }

    public void GameLose()
    {
        player.scooterAudioManager.StopBackgroundSound();
        player.enabled = false;
        // GameOver(); // Disable it for game lose
        gameLoseMenu.SetActive(true); // Enable it for game lose
        GameAnalytics.Instance.GameLoseEvent();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        print("GameOver...");

        print("gameLoseMenu.SetActive(false);");
        yield return new WaitForSeconds(0.2f);
        gameLoseMenu.SetActive(false);

        // print("IsHighScore()");
        // yield return new WaitForSeconds(3);
        if(IsHighScore())
        {
            // print("highScoreGameOverMenu.SetActive(true);");
            // yield return new WaitForSeconds(3);
            highScoreGameOverMenu.SetActive(true);
        }
        else
        {
            // print("gameOverMenu.SetActive(true);");
            // yield return new WaitForSeconds(3);
            gameOverMenu.SetActive(true);
        }

        // print("SetPunyaInLifeTime();");
        // yield return new WaitForSeconds(3);
        SetPunyaInLifeTime();

        // print("GameAnalytics.Instance.GameOverEvent();");
        // yield return new WaitForSeconds(3);
        GameAnalytics.Instance.GameOverEvent();
    }

    public void GameOver(string reason)
    {
        gameLoseMenu.SetActive(false);
        if(IsHighScore())
        {
            highScoreGameOverMenu.SetActive(true);
        }
        else
        {
            gameOverMenu.SetActive(true);
        }
        SetPunyaInLifeTime();

        GameAnalytics.Instance.GameOverEvent();
    }

    private bool IsHighScore()
    {
        if(GetMultipliedPunya() > GetHighScore())
        {
            print("Score :" + GetMultipliedPunya() + " > " + GetHighScore() + " = " + (GetMultipliedPunya() > GetHighScore()));
            SetHighScore(GetMultipliedPunya());
            return true;
        }
        return false;
    }

    // public void ResumeAsAReward(int time) // to be called by ad manager
    public void ResumeAsAReward()
    {
        Invoke("ResumeSound", 0.2f);

        joystick.enabled = true;
        player.enabled = true;
        timer.StartTimer(extraTimeAsReward);
        DecreaseTimeReward();
    }

    private void ResumeSound()
    {
        player.scooterAudioManager.ResumeBackgroundSound();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OnWatchAdClick()
    {
        onWatchAdButtonClicked = true;
        GameAnalytics.Instance.OnAdClickEvent();
        gameLoseMenu.SetActive(false);
        // call ad in ad manager
        adManager.ShowRewardAd();
    }

    private void DecreaseTimeReward()
    {
        extraTimeAsReward = Mathf.Clamp(extraTimeAsReward - 5, 10, 30);
    }

    public int GetMultiplier()
    {
        return FindObjectOfType<MultiplierSystem>().GetMultiplier();
    }

    public int GetCurrentPunya()
    {
        return deliveryBoy.GetCurrentPunya();
    }

    public int GetMultipliedPunya()
    {
        return GetCurrentPunya() * GetMultiplier();
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE_PLAYER_PREF, 0);
    }

    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE_PLAYER_PREF, score);
    }

    public int GetPunyaInLifeTime()
    {
        return PlayerPrefs.GetInt(TOTAL_PUNYA_PREF, 0);
    }

    public void SetPunyaInLifeTime()
    {
        int currentPunya = GetCurrentPunya();
        int lifetimePunya = GetPunyaInLifeTime();
        PlayerPrefs.SetInt(TOTAL_PUNYA_PREF, lifetimePunya + currentPunya);
    }
}