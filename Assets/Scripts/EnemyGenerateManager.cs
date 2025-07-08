using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerateManager : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] private List<GameObject> points = new List<GameObject>();
    [SerializeField] private OjamaSpawnScript ojamaSpawn;

    //[SerializeField] private float baseSpawnInterval = 5f;  // äÓñ{ÇÃê∂ê¨ä‘äu
    //SerializeField] private float minSpawnInterval = 1f;   // ç≈è¨ÇÃê∂ê¨ä‘äu

    //ÉåÉxÉãÇ…âûÇ∂ÇƒìGÇÃê∂ê¨ä‘äuÇ‚Ç«ÇÃìGÇ™ê∂ê¨ÇµÇ‚Ç∑Ç≠Ç»ÇÈÇÃÇ©í≤êÆÇ≈Ç´ÇÈ
    int level = 1;
    public bool isGenerate;

    private float spawnTimer = 0f;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        level = gameManager.level;

        if (level == 1)
        {
            if (isGenerate)
            {
                SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 1f);
                SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 2f, 2f);
                isGenerate = false;
            }
        }

        if (level == 2)
        {
            if (isGenerate)
            {
                SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 1f);
                SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 2f, 2f);

                isGenerate = false;
            }
        }

        if (level == 3)
        {
            if (isGenerate)
            {
                SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 1f);
                SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 2f, 2f);

                isGenerate = false;
            }
        }

        if (level == 4)
        {
            if (isGenerate)
            {
                SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 1f);
                SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 2f, 2f);

                isGenerate = false;
            }
        }

        if (level == 5)
        {
            if (isGenerate)
            {
                SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 1f);
                SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 2f, 2f);

                isGenerate = false;
            }
        }

        if (level == 6)
        {
            if (isGenerate)
            {
                SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 1f);
                SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 2f, 2f);

                isGenerate = false;
            }
        }

        if (level == 7)
        {
            if (isGenerate)
            {
                SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 1f);
                SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 2f, 2f);

                isGenerate = false;
            }
        }

        if (level == 8)
        {
            if (isGenerate)
            {
                SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 1f);
                SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 2f, 2f);

                isGenerate = false;
            }
        }

        if (level == 9)
        {
            if (isGenerate)
            {
                SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 1f);
                SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 2f, 2f);

                isGenerate = false;
            }
        }

        if (level == 10)
        {
            if (isGenerate)
            {
                SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 1f);
                SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 2f, 2f);

                isGenerate = false;
            }
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

    //    return enemies[0]; // îOÇÃÇΩÇﬂÇÃÉtÉHÅ[ÉãÉoÉbÉN
    //}

    // äOïîÇ©ÇÁÉåÉxÉãÇê›íËÇ∑ÇÈópÅiîCà”Åj
    public void SetLevel(int newLevel)
    {
        level = Mathf.Max(1, newLevel);
    }
}
