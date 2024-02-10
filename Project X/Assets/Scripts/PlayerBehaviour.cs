using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Vector2 movePosition = Vector2.zero;
    private Rigidbody2D rigidBody;
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private int health;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
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
    }

    private Vector3 GetMousePos()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
