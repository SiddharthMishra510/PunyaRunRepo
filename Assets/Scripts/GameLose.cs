using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLose : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI bonusTimeText;
    [SerializeField]
    private int timeToWait;

    private void OnEnable()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        SetBonusTime();
        StartCoroutine(Timer()); 
    }

    private void SetBonusTime()
    {
        int extraTime = GameManager.Instance.GetExtraTimeAsReward();
        bonusTimeText.text = extraTime.ToString();
    }

    private IEnumerator Timer()
    {
        int time = timeToWait;
        while(time >= 0)
        {
            timeText.text = time.ToString();
            if(time == 0)
            {
                GameManager.Instance.GameOver();
                break;
            }
            yield return new WaitForSecondsRealtime(1);
            time--;
        }
    }
}