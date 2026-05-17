using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyCircleInGrid : EnemyBase, IEnemyMove
{
    [SerializeField]
    Vector2Int circleGridSize; // urutan offset untuk circle in grid

    public override void Start()
    {
        base.Start();

        Move();
    }

    // lets say grid 2x2
    // Pindah ke kanan 1x, bawah 1x, kiri 1x, atas1x, terus ulangi
    // Ganti direction saat nyampe circlegridsize.x atau circlegridsize.y
    public void Move()
    {
        var rightDirection = new Vector2(circleGridSize.x - 1, 0);
        var downDirection = new Vector2(0, circleGridSize.y - 1);
        var leftDirection = new Vector2(-(circleGridSize.x - 1), 0);
        var upDirection = new Vector2(0, -(circleGridSize.y - 1));

        //Ganti direction saat movetowards nyamoe ke right terus down, terus left, terus up
        // Implementation for changing direction would go here
    }
}
