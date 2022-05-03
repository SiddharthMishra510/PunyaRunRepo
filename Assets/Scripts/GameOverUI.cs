using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentScoreText;
    [SerializeField]
    private TextMeshProUGUI highScoreText;

    private void OnEnable()
    {
        SetCurrentScore();
        SetHighScore();
    }
    private void SetCurrentScore()
    {
        currentScoreText.text = GameManager.Instance.GetCurrentPunya().ToString()
           + " X " + GameManager.Instance.GetMultiplier().ToString() + " = "
           + GameManager.Instance.GetMultipliedPunya().ToString();
    }    
    private void SetHighScore()
    {
        highScoreText.text = GameManager.Instance.GetHighScore().ToString();
    }
}
