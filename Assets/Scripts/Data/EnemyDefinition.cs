using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefinition", menuName = "Scriptable Objects/EnemyDefinition")]
public class EnemyDefinition : ScriptableObject
{
    [SerializeField]
    private int EnemyHp;
    [SerializeField]
    private int EnemyDamage;
    [SerializeField]
    private float EnemySpeed;
    [SerializeField]
    private float EnemyAttackCooldown;
    [SerializeField]
    private Enemy prefab;

    //私有字段+公开只读属性
    public int Hp => EnemyHp;
    public int Damage => EnemyDamage;
    public float Speed => EnemySpeed;
    public float AttackCooldown => EnemyAttackCooldown;
    public Enemy Prefab => prefab;
}
