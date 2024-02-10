using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FuelBarUI : MonoBehaviour
{
    public PlayerBehaviour player;

    public Slider fuelBar;

    // Start is called before the first frame update
    void Start()
    {
        fuelBar.value = player.fuel;
        fuelBar.maxValue = player.fuel;
    }

    // Update is called once per frame
    void Update()
    {
        fuelBar.maxValue = player.maxFuel;
        fuelBar.value = player.fuel;
    }
}
