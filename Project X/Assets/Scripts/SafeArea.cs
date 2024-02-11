using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    public LayerMask playerMask;
    public PlayerBehaviour player;

    [SerializeField] private float checkRange;
    public bool inRange;
    private bool isFueling = false;
    private AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        inRange = Physics2D.OverlapCircle(transform.position, checkRange, playerMask);

        if (inRange && !isFueling && player.fuel < player.maxFuel && !PauseMenu.IsGamePaused)
        {
            StartCoroutine(Fueling());
        }

        if (isFueling)
        {
            source.pitch = player.fuel / player.maxFuel;
        }

    }

    IEnumerator Fueling()
    {
        isFueling = true;
        source.Play();

        for (int i = 0; i < 100; i++)
        {
            if (!inRange)
            {
                isFueling = false;
                source.Stop();
                yield break;
            }
            yield return new WaitForSeconds(0.05f);
            player.fuel += player.maxFuel * 0.01f;
            if (player.fuel > player.maxFuel)
            {
                player.fuel = player.maxFuel;
                break;
            }
        }

        isFueling = false;
        source.Stop();
    }
}
