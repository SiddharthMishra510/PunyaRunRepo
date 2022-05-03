using TMPro;
using UnityEngine;

public class GameOverHighScoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    private void OnEnable()
    {
        SetHighScore();
    }
    private void SetHighScore()
    {
        highScoreText.text = GameManager.Instance.GetCurrentPunya().ToString()
          + " X " + GameManager.Instance.GetMultiplier().ToString() + " = "
          + GameManager.Instance.GetMultipliedPunya().ToString();
    }
}
