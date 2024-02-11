using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private float phase = 200;
    [SerializeField] private Transform cameraTransform;
    private AudioSource kick;
    [SerializeField] private float snareDistance = 500f;
    private AudioSource snare;
    [SerializeField] private float hatDistance = 1000f;
    private AudioSource hat;
    [SerializeField] private float arpDistance = 200f;
    private AudioSource arp;
    [SerializeField] private float bassDistance = 50f;
    private AudioSource bass;
    // Start is called before the first frame update
    void Start()
    {
        kick = GetComponents<AudioSource>()[0];
        snare = GetComponents<AudioSource>()[1];
        hat = GetComponents<AudioSource>()[2];
        arp = GetComponents<AudioSource>()[3];
        bass = GetComponents<AudioSource>()[4];
    }

    // Update is called once per frame
    void Update()
    {
        snare.volume = CalculateVolume(snareDistance, 1f);
        hat.volume = CalculateVolume(hatDistance, 1f);
        arp.volume = CalculateVolume(arpDistance, 0.5f);
        bass.volume = CalculateVolume(bassDistance, 0.5f);
    }

    private float CalculateVolume(float threshhold, float vol)
    {
        float displacement = cameraTransform.position.magnitude;
        if (displacement < threshhold)
        {
            return 0f;
        } 
        else if (displacement > threshhold + phase)
        {
            return vol;
        }
        else// if (displacement < threshhold + phase)
        {
            return math.min(vol*(displacement - threshhold) / phase, vol);
        }
    }
}
