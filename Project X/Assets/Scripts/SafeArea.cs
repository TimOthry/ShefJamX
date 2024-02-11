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
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        inRange = Physics2D.OverlapCircle(transform.position, checkRange, playerMask);

        if (inRange && !isFueling)
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
        float leftToFuel = player.maxFuel - player.fuel;

        for (int i = 0; i < leftToFuel; i++)
        {
            if (!inRange)
            {
                isFueling = false;
                yield break;
            }
            yield return new WaitForSeconds(0.00001f);
            player.fuel += player.maxFuel * 0.1f;
        }

        isFueling = false;
        source.Stop();

        if (player.fuel > player.maxFuel)
        {
            player.fuel = player.maxFuel;
        }
        
    }
}
