using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialUPG : MonoBehaviour
{
    public PlayerBehaviour player;
    public Button button;

    public TextMeshProUGUI priceText;

    private int cost = 9999999;



    public void Upgrade()
    {
        player.asteriodBreaker = true;
        player.credits -= cost;
        priceText.text = "BOUGHT";
        button.interactable = false;
    }
}
