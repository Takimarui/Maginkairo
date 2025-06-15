using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 3f;

    [Header("Dash")]

    [Header("Interaction")]
    [SerializeField] private float _interactRadius = 1f;
    [SerializeField] private LayerMask interactableLayers;

    public Vector2 MoveVector { get; private set; }

    private Rigidbody2D _rb;
    private Vector2 _inputVector;

    private const float _unitsPerPixel = 1f / 16f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnActionPressed += HandleActionPressed;
            GameInput.Instance.OnPausePressed += HandlePausePressed;
        }
    }

    private void OnDisable()
    {
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnActionPressed -= HandleActionPressed;
            GameInput.Instance.OnPausePressed -= HandlePausePressed;
        }
    }

    private void Update()
    {
        if (GameInput.Instance != null)
        {
            _inputVector = GameInput.Instance.GetMoveVector();
        }
        else
        {
            _inputVector = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        MoveVector = _inputVector * _moveSpeed;

        Vector2 movement = MoveVector * Time.fixedDeltaTime;
        Vector2 targetPosition = _rb.position + movement;

        Vector2 snappedPosition = SnapToPixelGrid(targetPosition);

        _rb.MovePosition(snappedPosition);
    }

    private void HandleActionPressed()
    {
        Debug.Log("Action.");
    }

    private void HandlePausePressed()
    {
        Debug.Log("Pause.");
    }

    private Vector2 SnapToPixelGrid(Vector2 position)
    {
        position.x = Mathf.Round(position.x / _unitsPerPixel) * _unitsPerPixel;
        position.y = Mathf.Round(position.y / _unitsPerPixel) * _unitsPerPixel;

        return position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _interactRadius);
    }
}
