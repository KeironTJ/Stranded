using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject mainMenuPanel;

    // Currency UI elements
    [SerializeField] private TMPro.TextMeshProUGUI primaryCurrencyText;
    [SerializeField] private TMPro.TextMeshProUGUI secondaryCurrencyText;
    [SerializeField] private TMPro.TextMeshProUGUI thirdCurrencyText;
    [SerializeField] private TMPro.TextMeshProUGUI forthCurrencyText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void UpdateCurrencyUI(int primary, int secondary, int third, int forth)
    {
        primaryCurrencyText.text = primary.ToString();
        secondaryCurrencyText.text = secondary.ToString();
        thirdCurrencyText.text = third.ToString();
        forthCurrencyText.text = forth.ToString();
    }
}
