using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int currentHp = 100;
    [SerializeField]
    private int attack;
    [SerializeField]
    private int maxHp = 100;
    [SerializeField]
    private Player profab;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private Bullet bulletPrefab;
    [SerializeField]
    private float attackInterval = 0.5f; // Time between attacks in seconds
    [SerializeField]
    private float attackRange = 8f; // Range of the attack

    private float attackTimer;
    private bool IsDead => currentHp <= 0;
    private Rigidbody2D rb;
    private Vector2 direction;
    public void Initialize(int hp, int attack, int movespeed)
    {
        this.currentHp = hp;
        this.attack = attack;
        this.moveSpeed = movespeed;
    }
    private void IsExceedMaxHp()
    {
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        IsExceedMaxHp();
        Debug.Log($"Player took {damage} damage. Current HP: {currentHp}");
        if(currentHp <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Player has died.");
        if(IsDead) return;
        currentHp = 0;
    }
    private void Awake() => rb = GetComponent<Rigidbody2D>();
    private void Update()
    {
        //Keyboard.current is a part of the new Input System in Unity. It allows you to check the state of keys on the keyboard.
        float x = (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0);
        float y = (Keyboard.current.wKey.isPressed ? 1 : 0) - (Keyboard.current.sKey.isPressed ? 1 : 0);
        direction = new Vector2(x, y).normalized;
        attackTimer -= Time.deltaTime;//每帧减，计时
        if(attackTimer > 0f) return;//还在冷却，这一帧啥都不做
        Enemy target = EnemyManager.Instance.GetNearest(transform.position, attackRange);
        if(target == null) return;
        attackTimer = attackInterval;//发射前先重置冷却
        Vector2 attackDirection = (target.transform.position - transform.position).normalized;
        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.Initialize(attackDirection, attack);
    }
    private void FixedUpdate()
    {
       rb.linearVelocity = direction * moveSpeed;
    }
}