using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Close : MonoBehaviour
{
    public GameObject HUD;
    public GameObject shop;


    public void Close()
    {
        shop.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1;
    }
}
