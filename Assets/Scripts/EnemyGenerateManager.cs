using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerateManager : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] private List<GameObject> points = new List<GameObject>();
    [SerializeField] private OjamaSpawnScript ojamaSpawn;

    //[SerializeField] private float baseSpawnInterval = 5f;  // 基本の生成間隔
    //SerializeField] private float minSpawnInterval = 1f;   // 最小の生成間隔

    //レベルに応じて敵の生成間隔やどの敵が生成しやすくなるのか調整できる
    //int level = 1;
    public bool isGenerate;

    private float spawnTimer = 0f;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        //level = gameManager.level;


        if (isGenerate)
        {
            //SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 1f);
            //SpawnEnemy(1, OjamaScript.OjamaType.Stone, new Vector2(0, -1), 2f, 2f);
            isGenerate = false;
        }

        if (isGenerate)
        {
            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.2f, 1f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.2f, 3f);
            isGenerate = false;
        }

        if (isGenerate)
        {
            SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 1f);
            SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 3f);

            isGenerate = false;
        }

        if (isGenerate)
        {
            SpawnEnemy(0, OjamaScript.OjamaType.Stone, new Vector2(-1, -1), 0.7f, 1f);
            SpawnEnemy(2, OjamaScript.OjamaType.Stone, new Vector2(-1, 0), 0.7f, 2f);
            SpawnEnemy(3, OjamaScript.OjamaType.Stone, new Vector2(1, 0), 0.7f, 3f);
            SpawnEnemy(1, OjamaScript.OjamaType.Stone, new Vector2(1, -1), 0.7f, 4f);

            isGenerate = false;
        }

        if (isGenerate)
        {
            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.4f, 1f);
            SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 2f);

            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.4f, 4f);
            SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 5f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.4f, 11f);
            SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 12f);

            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.4f, 14f);
            SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 15f);

            isGenerate = false;
        }

        if (isGenerate)
        {
            SpawnEnemy(0, OjamaScript.OjamaType.Stone, new Vector2(-1, -1), 1f, 1f);
            SpawnEnemy(0, OjamaScript.OjamaType.Stone, new Vector2(0, -1), 1f, 1f);

            SpawnEnemy(1, OjamaScript.OjamaType.Stone, new Vector2(1, -1), 1f, 3f);
            SpawnEnemy(1, OjamaScript.OjamaType.Stone, new Vector2(0, -1), 1f, 3f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 9f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.3f, 11f);

            SpawnEnemy(0, OjamaScript.OjamaType.Stone, new Vector2(-1, -1), 1f, 11f);
            SpawnEnemy(0, OjamaScript.OjamaType.Stone, new Vector2(0, -1), 1f, 11f);

            SpawnEnemy(1, OjamaScript.OjamaType.Stone, new Vector2(1, -1), 1f, 13f);
            SpawnEnemy(1, OjamaScript.OjamaType.Stone, new Vector2(0, -1), 1f, 13f);

            isGenerate = false;
        }
        if (isGenerate)
        {
            SpawnEnemy(0, OjamaScript.OjamaType.Cannon, new Vector2(-1, 0), 0.1f, 1f);
            SpawnEnemy(1, OjamaScript.OjamaType.Cannon, new Vector2(1, 0), 0.1f, 3f);

            SpawnEnemy(0, OjamaScript.OjamaType.Stone, new Vector2(-1, -1), 0.7f, 7f);
            SpawnEnemy(1, OjamaScript.OjamaType.Stone, new Vector2(1, -1), 0.7f, 9f);

            SpawnEnemy(0, OjamaScript.OjamaType.Cannon, new Vector2(-1, 0), 0.1f, 11f);
            SpawnEnemy(1, OjamaScript.OjamaType.Cannon, new Vector2(1, 0), 0.1f, 13f);

            SpawnEnemy(0, OjamaScript.OjamaType.Stone, new Vector2(-1, -1), 0.7f, 16f);
            SpawnEnemy(1, OjamaScript.OjamaType.Stone, new Vector2(1, -1), 0.7f, 17f);

            isGenerate = false;
        }
        if (isGenerate)
        {
            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.4f, 1f);
            SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 2f, 2f);
            SpawnEnemy(0, OjamaScript.OjamaType.Stone, new Vector2(-1, -1), 0.7f, 3f);

            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.4f, 5f);
            SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 2f, 6f);
            SpawnEnemy(1, OjamaScript.OjamaType.Stone, new Vector2(1, -1), 0.7f, 7f);

            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.4f, 10f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.3f, 10f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.4f, 13f);
            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 13f);

            isGenerate = false;
        }

        if (isGenerate)
        {
            SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 2f);
            SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 2f);

            SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 5f);
            SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 5f);

            SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 8f);
            SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 8f);

            SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 11f);
            SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 11f);

            SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 14f);
            SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 14f);

            isGenerate = false;
        }

        if (isGenerate)
        {
            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.2f, 1f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.2f, 3f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 1f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.3f, 3f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.4f, 1f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.4f, 3f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.5f, 1f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.5f, 3f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.2f, 8f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.2f, 10f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 8f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.3f, 10f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.4f, 8f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.4f, 10f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.5f, 8f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.5f, 10f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.2f, 15f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.2f, 17f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 15f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.3f, 17f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.4f, 15f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.4f, 17f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.5f, 15f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.5f, 17f);

            isGenerate = false;
        }
    }

    void SpawnEnemy(int spawnPoint, OjamaScript.OjamaType ojamaType, Vector2 direction, float speed, float levelSpawnTime)
    {
        if (points.Count == 0)
            return;

        OjamaSpawnScript newSpawn = Instantiate(ojamaSpawn, points[spawnPoint].transform.position, Quaternion.identity);
        newSpawn.Spawn(points[spawnPoint].transform.position, ojamaType, direction, speed, levelSpawnTime);
    }

    //GameObject GetWeightedRandomEnemy()
    //{
    //    float[] weights = new float[enemies.Count];
    //    float totalWeight = 0f;

    //    for (int i = 0; i < enemies.Count; i++)
    //    {
    //        weights[i] = Mathf.Pow((i + 1), level);
    //        totalWeight += weights[i];
    //    }

    //    float randomValue = Random.Range(0f, totalWeight);
    //    float currentSum = 0f;

    //    for (int i = 0; i < weights.Length; i++)
    //    {
    //        currentSum += weights[i];
    //        if (randomValue <= currentSum)
    //        {
    //            return enemies[i];
    //        }
    //    }

    //    return enemies[0]; // 念のためのフォールバック
    //}

    // 外部からレベルを設定する用（任意）
    //public void SetLevel(int newLevel)
    //{
    //    level = Mathf.Max(1, newLevel);
    //}
}
