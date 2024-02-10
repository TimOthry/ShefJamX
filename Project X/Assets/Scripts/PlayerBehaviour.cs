using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Vector2 movePosition = Vector2.zero;
    private Rigidbody2D rigidBody;
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private int health = 100;
    [SerializeField] private float fuel;
    [SerializeField] private float scaleMult;

    private float distanceTravelled;
    private Vector2 lastPos;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = GetMousePos();
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;
        movePosition = Vector2.Lerp(transform.position, mousePos, moveSpeed);
    }

    private void FixedUpdate()
    {
        if (!Input.GetMouseButton(1)) return;
        rigidBody.MovePosition(movePosition);

        distanceTravelled = Vector2.Distance(transform.position, lastPos);
        lastPos = transform.position;

        fuel -= distanceTravelled * scaleMult;

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
