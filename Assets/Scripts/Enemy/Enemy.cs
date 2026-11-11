using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyDefinition definition;//敌人定义
    private int currentHp;//hp
    private Transform target;//运行时状态
    private float attackTimer = 0f;
    [SerializeField]
    private EnemyDefinition testDefinition;

    private void Start()
    {
        if (testDefinition != null)
        {
            Initialize(testDefinition, GameObject.FindGameObjectWithTag("Player").transform);
        }
    }
    public void Initialize(EnemyDefinition definition, Transform target)
    {
        this.definition = definition;
        this.currentHp = definition.Hp;
        this.target = target;
    }
    public void Tick(float deltaTime)
    {
    if (definition == null || target == null) return;

    if (attackTimer > 0f)
        attackTimer -= deltaTime;

    float distance = Vector2.Distance(transform.position, target.position);
    transform.position = Vector3.MoveTowards(transform.position, target.position, definition.Speed * Time.deltaTime);
    }
    private void OnTriggerStay2D(Collider2D other)//引擎在触发器重叠期间自动调用的方法
    {
        if(!other.CompareTag("Player"))
        {
            return;//如果不是玩家，直接返回
        }
        if(attackTimer>0f)
        {
            return;//还在冷却，返回
        }
        attackTimer = definition.AttackCooldown;//重置冷却
        other.GetComponent<Player>().TakeDamage(definition.Damage);//调用玩家的受伤方法
    }
}
