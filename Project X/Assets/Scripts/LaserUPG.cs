using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LaserUPG : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerBehaviour player;
    public Bullet bullet;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI priceText;

    private int cost = 300;
    private int level = 1;



    public void Upgrade()
    {
        player.credits -= cost;
        cost += level * 150;
        level += 1;
        bullet.damage += 10;
        levelText.text = "Lvl. " + level.ToString();
        priceText.text = "$" + cost.ToString();
    }
}
