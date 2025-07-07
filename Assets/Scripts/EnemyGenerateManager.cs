using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerateManager : MonoBehaviour {
    [SerializeField] private List<GameObject> points = new List<GameObject>();
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    [SerializeField] private float baseSpawnInterval = 5f;  // Šî–{‚Ì¶¬ŠÔŠu
    [SerializeField] private float minSpawnInterval = 1f;   // Å¬‚Ì¶¬ŠÔŠu

    //ƒŒƒxƒ‹‚É‰ž‚¶‚Ä“G‚Ì¶¬ŠÔŠu‚â‚Ç‚Ì“G‚ª¶¬‚µ‚â‚·‚­‚È‚é‚Ì‚©’²®‚Å‚«‚é
    [SerializeField] private int level = 1;

    private float spawnTimer = 0f;

    void Update() {
        spawnTimer += Time.deltaTime;

        float currentInterval = Mathf.Max(baseSpawnInterval - level * 0.5f, minSpawnInterval);
        if (spawnTimer >= currentInterval) {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy() {
        if (points.Count == 0 || enemies.Count == 0)
            return;

        GameObject point = points[Random.Range(0, points.Count)];
        GameObject enemyPrefab = GetWeightedRandomEnemy();

        Instantiate(enemyPrefab, point.transform.position, Quaternion.identity);
    }

    GameObject GetWeightedRandomEnemy() {
        float[] weights = new float[enemies.Count];
        float totalWeight = 0f;

        for (int i = 0; i < enemies.Count; i++) {
            weights[i] = Mathf.Pow((i + 1), level);
            totalWeight += weights[i];
        }

        float randomValue = Random.Range(0f, totalWeight);
        float currentSum = 0f;

        for (int i = 0; i < weights.Length; i++) {
            currentSum += weights[i];
            if (randomValue <= currentSum) {
                return enemies[i];
            }
        }

        return enemies[0]; // ”O‚Ì‚½‚ß‚ÌƒtƒH[ƒ‹ƒoƒbƒN
    }

    // ŠO•”‚©‚çƒŒƒxƒ‹‚ðÝ’è‚·‚é—pi”CˆÓj
    public void SetLevel(int newLevel) {
        level = Mathf.Max(1, newLevel);
    }
}
