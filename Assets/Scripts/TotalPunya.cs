using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TotalPunya : MonoBehaviour
{
    private TextMeshProUGUI punyaField;
    const string TOTAL_PUNYA_PREF = "TotalPunya";

    private void Start()
    {
        punyaField = GetComponent<TextMeshProUGUI>();
        punyaField.text = "Total Punya: "+ GetPunyaInLifeTime().ToString();
    }
    public int GetPunyaInLifeTime()
    {
        return PlayerPrefs.GetInt(TOTAL_PUNYA_PREF, 0);
    }
}