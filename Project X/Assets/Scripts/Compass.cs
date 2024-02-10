using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Compass : MonoBehaviour
{
    public GameObject HomePlanet;
    [SerializeField] private float CompasRange = 20f;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance (transform.parent.position, HomePlanet.transform.position) > CompasRange)
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * (Mathf.Atan2(transform.parent.position.y, transform.parent.position.x) * Mathf.Rad2Deg + 90));
            spriteRenderer.enabled = true;
        } else {
            spriteRenderer.enabled = false;
        }
    }
}
