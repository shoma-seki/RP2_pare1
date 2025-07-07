using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static OjamaScript;

public class OjamaSpawnScript : MonoBehaviour
{
    [SerializeField] OjamaScript ojama;
    ShakerScript shaker;

    Vector2 spawnPoint;
    OjamaType ojamaType;
    Vector2 direction;
    float speed;
    float levelSpawnTime;

    private void Start()
    {
        shaker = FindAnyObjectByType<ShakerScript>();
    }

    private void Update()
    {
        if (!shaker.isGrounded)
        {
            levelSpawnTime -= Time.deltaTime;
            if (levelSpawnTime < 0)
            {
                OjamaScript newOjama = Instantiate(ojama, spawnPoint, Quaternion.identity);
                newOjama.Setting(ojamaType, direction.normalized, speed);

                Destroy(gameObject);
            }
        }
    }

    public void Spawn(Vector2 spawnPoint, OjamaScript.OjamaType ojamaType, Vector2 direction, float speed, float levelSpawnTime)
    {
        this.spawnPoint = spawnPoint;
        this.ojamaType = ojamaType;
        this.direction = direction;
        this.speed = speed;
        this.levelSpawnTime = levelSpawnTime;
    }
}
