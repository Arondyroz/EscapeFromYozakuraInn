using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed = 1f;
    protected Vector3 CurrentGridPos => GridManager.Instance.WorldToGrid(transform.position);

    public virtual void Start() { }

    public virtual void OnTriggerEnter2D(Collider2D collision) { }
}
