using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerateManager : MonoBehaviour
{
    GameManager gameManager;
    float gameTime;
    float preGameTime;

    [SerializeField] private List<GameObject> points = new List<GameObject>();
    [SerializeField] private OjamaSpawnScript ojamaSpawn;

    public bool isGenerate;

    private float spawnTimer = 0f;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        isGenerate = true;
    }

    void Update()
    {
        gameTime = gameManager.gameTime;

        //if (isGenerate)
        //{
        //    //SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 1f);
        //    //SpawnEnemy(1, OjamaScript.OjamaType.Stone, new Vector2(0, -1), 2f, 2f);
        //    isGenerate = false;
        //}

        //一気に生成されるのを防ぐため前のフレームと今のフレームのgameTimeを比較しする
        //if(isGenerate)の前には必ずこれを入れること。
        //直下のもので言うとgameTimeが5fを超えたフレームでisGenerateがtureになり、生成された後にisGenerateがfalseになるようになっている。
        if (preGameTime > 50f && gameTime <= 50f)
        {
            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.2f, 1f);
            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.2f, 3f);
        }

        if (preGameTime > 35f && gameTime <= 35f)
        {
            SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 1f);
            SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 3f);

        }

        if (preGameTime > 25f && gameTime <= 25f)
        {
            SpawnEnemy(0, OjamaScript.OjamaType.Stone, new Vector2(-1, -1), 0.7f, 1f);
            SpawnEnemy(2, OjamaScript.OjamaType.Stone, new Vector2(-1, 0), 0.7f, 2f);
            SpawnEnemy(3, OjamaScript.OjamaType.Stone, new Vector2(1, 0), 0.7f, 3f);
            SpawnEnemy(1, OjamaScript.OjamaType.Stone, new Vector2(1, -1), 0.7f, 4f);
        }

        if (preGameTime > 15f && gameTime <= 15f)
        {
            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.4f, 1f);
            SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 2f);

            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.4f, 4f);
            SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 5f);

            SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.4f, 11f);
            SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 12f);

            SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.4f, 14f);
            SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 15f);
        }

        if (preGameTime > 5f && gameTime <= 5f)
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
        }

        //if (isGenerate)
        //{
        //    SpawnEnemy(0, OjamaScript.OjamaType.Cannon, new Vector2(-1, 0), 0.1f, 1f);
        //    SpawnEnemy(1, OjamaScript.OjamaType.Cannon, new Vector2(1, 0), 0.1f, 3f);

        //    SpawnEnemy(0, OjamaScript.OjamaType.Stone, new Vector2(-1, -1), 0.7f, 7f);
        //    SpawnEnemy(1, OjamaScript.OjamaType.Stone, new Vector2(1, -1), 0.7f, 9f);

        //    SpawnEnemy(0, OjamaScript.OjamaType.Cannon, new Vector2(-1, 0), 0.1f, 11f);
        //    SpawnEnemy(1, OjamaScript.OjamaType.Cannon, new Vector2(1, 0), 0.1f, 13f);

        //    SpawnEnemy(0, OjamaScript.OjamaType.Stone, new Vector2(-1, -1), 0.7f, 16f);
        //    SpawnEnemy(1, OjamaScript.OjamaType.Stone, new Vector2(1, -1), 0.7f, 17f);

        //    isGenerate = false;
        //}
        //if (isGenerate)
        //{
        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.4f, 1f);
        //    SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 2f, 2f);
        //    SpawnEnemy(0, OjamaScript.OjamaType.Stone, new Vector2(-1, -1), 0.7f, 3f);

        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.4f, 5f);
        //    SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 2f, 6f);
        //    SpawnEnemy(1, OjamaScript.OjamaType.Stone, new Vector2(1, -1), 0.7f, 7f);

        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.4f, 10f);
        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.3f, 10f);

        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.4f, 13f);
        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 13f);

        //    isGenerate = false;
        //}

        //if (isGenerate)
        //{
        //    SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 2f);
        //    SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 2f);

        //    SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 5f);
        //    SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 5f);

        //    SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 8f);
        //    SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 8f);

        //    SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 11f);
        //    SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 11f);

        //    SpawnEnemy(1, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 14f);
        //    SpawnEnemy(0, OjamaScript.OjamaType.Poison, new Vector2(0, -1), 4f, 14f);

        //    isGenerate = false;
        //}

        //if (isGenerate)
        //{
        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.2f, 1f);
        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.2f, 3f);

        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 1f);
        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.3f, 3f);

        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.4f, 1f);
        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.4f, 3f);

        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.5f, 1f);
        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.5f, 3f);

        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.2f, 8f);
        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.2f, 10f);

        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 8f);
        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.3f, 10f);

        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.4f, 8f);
        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.4f, 10f);

        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.5f, 8f);
        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.5f, 10f);

        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.2f, 15f);
        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.2f, 17f);

        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.3f, 15f);
        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.3f, 17f);

        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.4f, 15f);
        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.4f, 17f);

        //    SpawnEnemy(3, OjamaScript.OjamaType.Cannon, new Vector2(1, 1), 0.5f, 15f);
        //    SpawnEnemy(2, OjamaScript.OjamaType.Cannon, new Vector2(-1, 1), 0.5f, 17f);

        //    isGenerate = false;
        //}

        preGameTime = gameTime;
    }

    void SpawnEnemy(int spawnPoint, OjamaScript.OjamaType ojamaType, Vector2 direction, float speed, float levelSpawnTime)
    {
        if (points.Count == 0)
            return;

        OjamaSpawnScript newSpawn = Instantiate(ojamaSpawn, points[spawnPoint].transform.position, Quaternion.identity);
        newSpawn.Spawn(points[spawnPoint].transform.position, ojamaType, direction, speed, levelSpawnTime);
    }
}
