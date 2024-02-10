using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.VersionControl;
using UnityEngine;

public class Starfield : MonoBehaviour
{
    [SerializeField] private int maxStars = 100;
    [SerializeField] private float starSize = 0.1f;
    [SerializeField] private float starSizeRange = 0.5f;
    [SerializeField] private float fieldWidth = 20f;
    [SerializeField] private float fieldHeight = 25f;
    [SerializeField] private bool colourise = false;

    [SerializeField] private float ParallaxFactor = 0f;
    [SerializeField] private Transform bgCamera;


    private float xOffset;
    private float yOffset;


    private ParticleSystem particles;
    private ParticleSystem.Particle[] stars;

    void Awake()
    {
        //bgCamera = GameObject.FindWithTag("BG_Camera").transform;
        stars = new ParticleSystem.Particle[maxStars];
        particles = GetComponent<ParticleSystem>();

        //Assert.IsNotNull(particles, "Particle system missing from object!");

        xOffset = fieldWidth * 0.5f;
        yOffset = fieldHeight * 0.5f;

        for (int i = 0; i < maxStars; i++)
        {
            float randSize = Random.Range(starSize, starSizeRange + 1f);
            float scaledColour = colourise ? randSize - starSizeRange : 1f;

            stars[i].position = GetRandomInRectangle(fieldWidth, fieldHeight) + transform.position;
            stars[i].startSize = starSize * randSize;
            stars[i].startColor = new Color(1f, scaledColour, scaledColour, 1f);
        }
        particles.SetParticles(stars, stars.Length);
    }

    Vector3 GetRandomInRectangle(float width, float height)
    {
        float x = Random.Range(0, width);
        float y = Random.Range(0, height);
        return new Vector3(x - xOffset, y - yOffset);
    }

    // Update is called once per frame
    void Update()
    {
        for ( int i=0; i<maxStars; i++ )
		{
			Vector3 pos = stars[ i ].position + transform.position;
 
			if ( pos.x < bgCamera.position.x - xOffset )
			{
				pos.x += fieldWidth;
			}
			else if ( pos.x > bgCamera.position.x + xOffset )
			{
				pos.x -= fieldWidth;
			}
 
			if ( pos.y < bgCamera.position.y - yOffset )
			{
				pos.y += fieldHeight;
			}
			else if ( pos.y > bgCamera.position.y + yOffset )
			{
				pos.y -= fieldHeight;
			}
 
			stars[ i ].position = pos - transform.position;
		}
		particles.SetParticles( stars, stars.Length );

        Vector3 newPos = bgCamera.position * ParallaxFactor;					// Calculate the position of the object
		newPos.z = 0;						// Force Z-axis to zero, since we're in 2D
		transform.position = newPos;

    }
}
