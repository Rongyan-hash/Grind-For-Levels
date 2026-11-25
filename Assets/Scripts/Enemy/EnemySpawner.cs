using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyDefinition> enemyTypes;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private ArenaDefinition arena;
    [SerializeField] private float minPlayerDistance = 10f;
    [SerializeField] private int attempts = 12;
    private float spawnTimer;
    private Transform playerTransform;
    
    private void Start()
    {
        Debug.Log("Spawner Start");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //找出玩家位置
        spawnTimer = spawnInterval;//计时器
        //初始化计时器

    }

    private void Update()
    {
        //Debug.Log($"Spawner Update timer={spawnTimer:F2}");
        spawnTimer -= Time.deltaTime;//计时器递减
        if(spawnTimer > 0f) return;
        Vector3 pos = GetRandomPoint();
        /*Debug.Log($"Cand={pos}"+$"dist={Vector3.Distance(pos,playerTransform.position):F1}"+$"onScreen={IsOnScreen(pos)}"+"inArena={IsInsideArena(pos)}"+
        $"camNull={Camera.main==null}"+$"arenaNull={arena == null}"+$"poolNull={EnemyPool.Instance == null}");*/
        if(TrySpawnEnemy())
        {
            spawnTimer += spawnInterval;
        }
        else
        {
            spawnTimer = 0.25f;
        }
    }

       private Vector3 GetRandomPoint()
    {
        Bounds b = arena.Bounds;
        float m = arena.SpawnMargin;
        return new Vector3(Random.Range(b.min.x + m, b.max.x - m),Random.Range(b.min.y + m, b.max.y - m),0f);
    }

    private bool IsOnScreen(Vector3 worldPos)
    {
        Vector3 vp = Camera.main.WorldToViewportPoint(worldPos);
        return vp.x > 0f && vp.x < 1f && vp.y > 0f && vp.y < 1f && vp.z > 0f;
    }

    /*private bool IsInsideArena(Vector3 pos)
    {
        Bounds inner = arena.Bounds;
        inner.Expand(-2f * arena.SpawnMargin);
        return inner.Contains(pos);
    }*/

    private bool TrySpawnEnemy()
    {
        if(enemyTypes == null || enemyTypes.Count ==0) return false;

        for(int i = 0;i < attempts;i++)
        {
            Vector3 pos = GetRandomPoint();
            if(Vector3.Distance(pos,playerTransform.position)<minPlayerDistance) continue;
            if(IsOnScreen(pos)) continue;
            //if(!IsInsideArena(pos)) continue;
            
            Enemy enemy = EnemyPool.Instance.Get(enemyTypes[Random.Range(0,enemyTypes.Count)],playerTransform);
            enemy.transform.position = pos;
            return true;
        }
        return false;
    }
}
