using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }

    [SerializeField] private Enemy enemyPrefab;//敌人预制体
    [SerializeField] private int initialPoolSize = 10;

    private readonly Stack<Enemy> idle = new Stack<Enemy>();//空闲敌人栈

    private void Awake()
    {
        Instance = this;
        for(int i = 0; i < initialPoolSize; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, transform);
            enemy.gameObject.SetActive(false);//禁用敌人
            idle.Push(enemy);//入栈
        }
    }

    public Enemy Get(EnemyDefinition definition, Transform target)
    {
        Enemy enemy = idle.Count > 0 ? idle.Pop() : Instantiate(enemyPrefab, transform);
        enemy.Initialize(definition, target);//初始化敌人
        enemy.gameObject.SetActive(true);//激活敌人
        return enemy;//返回敌人
    }

    public void Release(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);//禁用敌人
        idle.Push(enemy);//入栈
    }
}