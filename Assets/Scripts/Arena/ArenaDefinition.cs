using UnityEngine;

[CreateAssetMenu(fileName = "ArenaDefinition", menuName = "Arena/ArenaDefinition")]
public class ArenaDefinition : ScriptableObject
{
    [SerializeField] private Vector2 center = Vector2.zero;
    [SerializeField] private Vector2 size = new Vector2(40f, 40f);
    [SerializeField] private float spawnMargin = 1f;

    public Vector2 Center => center;
    public float SpawnMargin => spawnMargin;
    public Bounds Bounds => new Bounds(center, size);
}