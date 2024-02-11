using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource kick;
    // Start is called before the first frame update
    void Start()
    {
        kick = GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        kick.volume = .5f;
    }
}
