using UnityEngine;
using TMPro;

public class LaserUPG : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerBehaviour player;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI priceText;

    private int cost = 300;
    private int level = 1;

    private Weapon weapon;

    private void Start()
    {
        weapon = player.GetComponent<Weapon>();
    }

    public void Upgrade()
    {
        player.credits -= cost;
        cost += level * 150;
        level += 1;
        weapon.bulletDamage += 10;
        levelText.text = "Lvl. " + level.ToString();
        priceText.text = "$" + cost.ToString();
    }
}
