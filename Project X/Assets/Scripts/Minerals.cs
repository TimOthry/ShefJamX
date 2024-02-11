using UnityEngine;

public class Minerals : MonoBehaviour
{
    public Loot loot;
    public int Collect()
    {
        Destroy(gameObject);
        return 1000 / loot.dropChance;
    }
}
