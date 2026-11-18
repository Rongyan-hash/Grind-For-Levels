using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyDefinition definition;//敌人定义
    private int currentHp;//hp
    private Transform target;//运行时状态
    private float attackTimer = 0f;
    private bool IsDead{ get { return currentHp <= 0; } set { currentHp = 0; } }

    [SerializeField]
    private EnemyDefinition testDefinition;

    private void OnEnable()
    {
        if(EnemyManager.Instance!=null)
        {
            EnemyManager.Instance.RegisterEnemy(this);
        }
    } 
    private void OnDisable()
    {
        if(EnemyManager.Instance!=null)
        {
            EnemyManager.Instance.UnregisterEnemy(this);
        }
    }
    public void Initialize(EnemyDefinition definition, Transform target)
    {
        this.definition = definition;
        this.target = target;
        currentHp = definition.Hp;
        attackTimer = 0f;//池化必须
    }

    private void OnDestroy()
    {
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.UnregisterEnemy(this);
        }
    }

    public void Tick(float deltaTime)
    {
        if (definition == null || target == null) return;

        if (attackTimer > 0f)
            attackTimer -= deltaTime;

        // 每秒最多移动 definition.Speed 个单位，方向是当前位置指向玩家。
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            definition.Speed * deltaTime);
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
    public void TakeDamage(int damage)
    {
        if(IsDead ||definition == null) return;//如果没有定义，直接返回 敌人已经死了，忽略
        currentHp -= damage;//扣血
        if(currentHp<=0)
        {
            IsDead = true;
            Die();//死亡
        }
    }
    private void Die()
    {
        EnemyPool.Instance.Release(this);//回收敌人
    }
}