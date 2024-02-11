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
    [SerializeField] [Range(0,1)] private float kickVolume;
    [SerializeField] [Range(0,1)] private float snareVolume;
    [SerializeField] [Range(0,1)] private float hatVolume;
    [SerializeField] [Range(0,1)] private float arpVolume;
    [SerializeField] [Range(0,1)] private float bassVolume;
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
        kick.volume = kickVolume;
        snare.volume = CalculateVolume(snareDistance) * snareVolume;
        hat.volume = CalculateVolume(hatDistance) * hatVolume;
        arp.volume = CalculateVolume(arpDistance) * arpVolume;
        bass.volume = CalculateVolume(bassDistance) * bassVolume;
    }

    private float CalculateVolume(float threshhold)
    {
        float displacement = cameraTransform.position.magnitude;
        if (displacement < threshhold)
        {
            return 0f;
        } 
        if (displacement > threshhold + phase)
        {
            return 1f;
        }
        return math.min(1f * (displacement - threshhold) / phase, 1f);
    }
}
