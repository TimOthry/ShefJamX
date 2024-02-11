using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Rigidbody2D rigidBody;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float accelerationTime;
    private float moveSpeed;
    private Vector2 moveStep;
    private Vector2 mouseDifferenceNormalised;
    public float fuel;
    public float maxFuel;
    [SerializeField] private float scaleMultFuel;
    [SerializeField] private float fuelDecay;
    [SerializeField] private float boost;

    [SerializeField] private GameObject destructionEffect;
    [SerializeField] private AudioClip explosionSound;

    public int credits;
    private AudioSource source;
    

    public float distanceTravelled;
    private Vector2 lastPos;

    public SafeArea safeArea;
    public GameObject shop;
    public GameObject HUD;

    public bool asteriodBreaker = false;
    [SerializeField] AudioClip itemClip;
    [SerializeField] private float thrustVolume;

    // Start is called before the first frame update
    void Start()
    {
        boost = 1f;
        maxFuel = 500; // This is to be changed when fuel is upgraded
        rigidBody = GetComponent<Rigidbody2D>();
        lastPos = transform.position;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            boost = 2f;
        }
        else
        {
            boost = 1f;
        }

        if (safeArea.inRange && Input.GetKey(KeyCode.S))
        {
            HUD.SetActive(false);
            shop.SetActive(true);
            Time.timeScale = 0;
        }

        Vector3 mousePos = GetMousePos();
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;
        mouseDifferenceNormalised = new Vector2(Mathf.Clamp(mousePos.x - transform.position.x, -9f, 9f),
            Mathf.Clamp(mousePos.y - transform.position.y, -9f, 9f)) / 9f;

        
        source.volume = moveStep.magnitude * thrustVolume;
        source.panStereo = -moveStep.x/(maxMoveSpeed*5f);

    }

    private void FixedUpdate()
    {
        distanceTravelled = Vector2.Distance(transform.position, lastPos);
        Debug.Log(distanceTravelled.ToString("F4"));
        lastPos = transform.position;

        if (!safeArea.inRange)
        {
            fuel -= fuelDecay + (distanceTravelled * scaleMultFuel);
        }
        if (fuel <= 0)
        {
            Die();
        }

        if (!Input.GetMouseButton(1))
        {
            if (moveSpeed > 0f) moveSpeed -= Time.fixedDeltaTime / accelerationTime;
            if (moveSpeed < 0f) moveSpeed = 0f;
        }
        else if (moveSpeed < maxMoveSpeed) moveSpeed += Time.fixedDeltaTime / accelerationTime;
        moveStep = mouseDifferenceNormalised * (moveSpeed * boost);
        rigidBody.MovePosition(transform.position + (Vector3)moveStep);
        Debug.Log("Move step: " + moveStep.magnitude.ToString("F3") + " mouse dif: " + mouseDifferenceNormalised.ToString("F3"));
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.GetComponent<Minerals>() is { } minerals)
        {
            AudioSource.PlayClipAtPoint(itemClip, transform.position, 50f);
            int money = minerals.Collect();
            credits += money;
        }
        else if (hitInfo.GetComponent<AsteroidBehaviour>() is {} asteroid)
        {
            if (asteriodBreaker)
            {
                asteroid.Die();
            }
            else
            {
                Die();
            }
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position, 2f);
        Instantiate(destructionEffect, transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        PauseMenu.Instance.GameOver();
    }

    private Vector3 GetMousePos()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
