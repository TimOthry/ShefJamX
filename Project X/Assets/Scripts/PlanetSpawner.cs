using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] private float distance = 500;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Random.insideUnitCircle * distance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
