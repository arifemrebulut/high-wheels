using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float maxXBounds;
    [SerializeField] private float lerpSpeed;

    private Vector3 targetPosition;

    private bool canMove = false;

    PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            canMove = true;
        }

        if (canMove)
        {
            ForwardMovement();
            SwerveMovement();
        }       
    }

    private void ForwardMovement()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    private void SwerveMovement()
    {
        float desiredXPosition = Mathf.Clamp(playerInput.deltaX, -maxXBounds, maxXBounds);

        targetPosition = new Vector3(Mathf.Clamp(transform.position.x + desiredXPosition, -maxXBounds, maxXBounds), transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed);
    }
}