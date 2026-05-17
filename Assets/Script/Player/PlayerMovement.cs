using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f; // step size

    InputSystem_Actions inputActions;

    Vector3Int currentGridPos;

    Vector3 targetWorldPos;

    bool isMoving;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void Start()
    {
        currentGridPos = GridManager.Instance.WorldToGrid(transform.position);
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
    }

    void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    void Update()
    {
        // Jangan move kalau tidak sedang moving
        if (!isMoving)
            return;

        // Gerak perlahan menuju target
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetWorldPos,
            moveSpeed * Time.deltaTime
        );

        // Sudah sampai target?
        if (transform.position == targetWorldPos)
        {
            isMoving = false;
        }
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        // Jangan bisa input saat masih bergerak
        if (isMoving)
            return;

        Vector2Int direction = Vector2Int.RoundToInt(ctx.ReadValue<Vector2>());

        Move(direction);
    }

    //Should Check MoveSpeed Calculation
    void Move(Vector2Int direction)
    {
        Vector3Int targetGridPos = currentGridPos + new Vector3Int(direction.x, direction.y, 0);

        if (!CanMove(targetGridPos))
            return;

        // Update logical position
        currentGridPos = targetGridPos;

        // Tentukan target visual position
        targetWorldPos = GridManager.Instance.GridToWorld(currentGridPos);

        // Lock movement
        isMoving = true;
    }

    bool CanMove(Vector3Int direction)
    {
        if (GridManager.Instance.TryGetCell(direction, out Cell cell))
        {
            return cell.IsWalkable;
        }

        return false;
    }
}
