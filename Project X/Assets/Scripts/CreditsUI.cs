using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsUI : MonoBehaviour
{
    public PlayerBehaviour player;
    public TextMeshProUGUI creditText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        creditText.text = "$" + player.credits.ToString();
    }
}
