using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    //readonly只读字段的修饰符，只能在声明时或构造函数中赋值，之后不能再修改。它保证了字段的值在对象的生命周期内保持不变。
    private readonly List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);//把引用塞进列表
        }
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);//找到第一个相等的引用，移除
        }
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        // 倒着遍历：中途移除某个敌人时，后面的下标不会被挤乱。
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            Enemy enemy = enemies[i];
            if (enemy == null)
            {
                enemies.RemoveAt(i);
                continue;
            }
            enemy.Tick(deltaTime);
        }
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            for(int i=enemies.Count - 1;i>=0;i--)
            {
                enemies[i].TakeDamage(10);
            }
        }
    }

    public Enemy GetNearest(Vector3 from, float maxRange)
    {
        Enemy nearest = null;
        float best = maxRange;

        for(int i = 0;i<enemies.Count;i++)
        {
            Enemy e = enemies[i];
               if(e == null || !e.gameObject.activeSelf) continue;

               float distance = Vector2.Distance(from, e.transform.position);
               if(distance < best)
               {
                   best = distance;
                   nearest = e;
               }
        }
        return nearest;
    }
}
