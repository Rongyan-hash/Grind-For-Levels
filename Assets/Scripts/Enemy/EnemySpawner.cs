using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyDefinition> enemyTypes;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float spawnRadius = 13f;

    private float spawnTimer;
    private Transform playerTransform;
    private EnemyDefinition def;
    private float angle;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //找出玩家位置
        spawnTimer = spawnInterval;//计时器
        //初始化计时器
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;//计时器递减

        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        //1.选择敌人
        def = enemyTypes[Random.Range(0, enemyTypes.Count)];//随机选择敌人类型
        //2.计算位置
        angle = Random.Range(0f,Mathf.PI*2f);//选择随机角度
        Vector2 dir/*方向*/ = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));//计算方向
        Vector3 pos/*位置*/ = playerTransform.position + (Vector3)(dir * spawnRadius);//计算位置
        //3.生成敌人
        Enemy enemy = EnemyPool.Instance.Get(def, playerTransform);//从对象池获取敌人
        enemy.transform.position = pos;//设置位置
    }
}
