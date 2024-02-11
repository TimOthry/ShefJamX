using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuelUPG : MonoBehaviour
{
    public PlayerBehaviour player;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI priceText;

    private int cost = 200;
    private int level = 1;



    public void Upgrade()
    {
        player.credits -= cost;
        cost += level * 100;
        level += 1;
        player.maxFuel += 200;
        levelText.text = "Lvl. " + level.ToString();
        priceText.text = "$" + cost.ToString();
    }
}
