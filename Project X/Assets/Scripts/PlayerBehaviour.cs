using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Vector2 movePosition = Vector2.zero;
    private Rigidbody2D rigidBody;
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private int health = 100;
    public float fuel;
    public float maxFuel;
    [SerializeField] private float scaleMultFuel;
    [SerializeField] private float fuelDecay;
    [SerializeField] private float boost;

    private float distanceTravelled;
    private Vector2 lastPos;

    public SafeArea safeArea;

    // Start is called before the first frame update
    void Start()
    {
        boost = 1;
        maxFuel = 500; // This is to be changed when fuel is upgraded
        rigidBody = GetComponent<Rigidbody2D>();
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            boost = 10;
        }
        else
        {
            boost = 1;
        }

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    boost = 1;
        //}

        Vector3 mousePos = GetMousePos();
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;
        movePosition = Vector2.Lerp(transform.position, mousePos, moveSpeed * boost);

    }

    private void FixedUpdate()
    {
        if (!Input.GetMouseButton(1)) return;
        rigidBody.MovePosition(movePosition);

        distanceTravelled = Vector2.Distance(transform.position, lastPos);
        lastPos = transform.position;

        if (!safeArea.inRange)
        {
            fuel -= fuelDecay + (distanceTravelled * scaleMultFuel);
        }
        

        if (fuel <= 0)
        {
            // Added Player death here as well

            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.GetComponent<AsteroidBehaviour>() is { } asteroid)
        {
            Vector2 velocityDifference = movePosition - (Vector2)asteroid.vectorVelocity / Time.deltaTime;
            float speedDifference = velocityDifference.magnitude;
            Debug.Log(speedDifference.ToString("F2"));
        }
    }

    private Vector3 GetMousePos()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
