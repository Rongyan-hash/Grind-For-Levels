using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int currentHp = 100;
    [SerializeField]
    private int attack;
    [SerializeField]
    private int movespeed;
    [SerializeField]
    private int maxHp = 100;
    [SerializeField]
    private Player profab;

    private bool IsDead => currentHp <= 0;
    public void Initialize(int hp, int attack, int movespeed)
    {
        this.currentHp = hp;
        this.attack = attack;
        this.movespeed = movespeed;
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

}