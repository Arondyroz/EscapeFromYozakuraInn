public enum FloorType
{
    None,
    Trap, // trap itu bisa bikin player mati atau point berkurang dan player respawn di titik awal
    Obstacle, //obstacle itu bisa bikin player gak bisa lewat, dan bisa dihancurkan
    Wall, // gabisa dihancurkan dan player gabisa lewat
    Walkable, // floor yang bisa dilewati player
    Checkpoint, // checkpoint itu bisa bikin player respawn di titik itu kalau mati
    Start, //Titik awal player untuk spawn
    End, // titik akhir player untuk lanjut ke level berikutnya
}
