using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField] 
    private TextMeshProUGUI timeText;
    [SerializeField] 
    private GameObject gameOverMenu;

    private int timeLeft = 0;
    private Coroutine coroutine;

    public void StartTimer(int time)
    {
        timeLeft = time;
        timeText.color = Color.white;
        coroutine = StartCoroutine(StartTime());
    }

    public void StartTimer()
    {
        timeLeft = GameManager.Instance.GetMaxTime();
        timeText.color = Color.white;
        coroutine = StartCoroutine(StartTime());
    }

    public void StopTimer()
    {
        StopCoroutine(coroutine);
        // timeText.text = "0:00";
        timeText.color = Color.green;
    }

    private IEnumerator StartTime()
    {
        while (timeLeft > 0)
        {
            // Debug.Log(timer);
            yield return new WaitForSecondsRealtime(1);
            timeLeft--;

            timeText.text = ConvertSecondsToStandardTimeFormat(timeLeft);
        }

        if (timeLeft <= 0)
        {
            timeText.text = "0:00";
            // Debug.Log("Game Over #############");
            GameManager.Instance.GameLose();
        }
        else
        {
            timeLeft = 0;
        }
    }

    private static string ConvertSecondsToStandardTimeFormat(int seconds)
    {
        // Standard format is "mm:ss"

        int mins = (seconds % 3600) / 60; // or (int)(seconds / 60))
        int secs = seconds % 60;

        string minuteString = mins.ToString();
        string secsString = secs.ToString();

        if(mins < 10)
        {
            minuteString = "0" + mins;
        }
        if(secs < 10)
        {
            secsString = "0" + secs;
        }

        return minuteString + ":" + secsString;
    }
}