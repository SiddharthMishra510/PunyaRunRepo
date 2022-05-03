using System.Collections;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class MultiplierSystem : MonoBehaviour
{
    public int GetMultiplier() { return multiplier; }

    [SerializeField]
    private TextMeshProUGUI multiplierField;

    private int multiplier = 1;

    private const float ANIMATION_TIME = 2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseMultiplier();
        }
    }
    public void IncreaseMultiplier()
    {
        // Instantiate(ParticleEffectManager.Instance.MagicalEffect, transform.position, Quaternion.identity);
        DoColorAnimation(multiplierField);
        multiplier++;
        multiplierField.text = "x" + multiplier.ToString();
    }

    private void DoColorAnimation(TextMeshProUGUI multiplierField)
    {
        print("Doing Color Animation on: " + multiplierField.name);
        StartCoroutine(ColorAnimation(multiplierField));
    }

    private IEnumerator ColorAnimation(TextMeshProUGUI textField)
    {
        Color initialColor = textField.color;
        float timeSpent = 0;

        while (timeSpent < ANIMATION_TIME)
        {
            textField.color = GetRandomColor(textField.color);
            yield return new WaitForSeconds(0.1f);
            textField.color = Color.white;
            timeSpent += 0.1f;
            yield return new WaitForSeconds(0.1f);
            timeSpent += 0.1f;
        }

        textField.color = initialColor;
    }
    private Color GetRandomColor(Color colorToIgnore)
    {
        int random = Random.Range(0, 3);
        if (random == 0)
        {
            if (colorToIgnore == Color.red)
            {
                return Color.yellow;
            }
            return (Color.red + new Color(0.0f, 0.4f, 0.4f));
        }
        else if (random == 1)
        {
            if (colorToIgnore == Color.yellow)
            {
                return (Color.blue + new Color(0.4f, 0.4f, 0.0f));
            }
            return Color.yellow;
        }
        else
        {
            if (colorToIgnore == Color.blue)
            {
                return (Color.red + new Color(0.0f,0.4f,0.4f));
            }
            return (Color.blue + new Color(0.4f, 0.4f, 0.0f));
        }
    }
}