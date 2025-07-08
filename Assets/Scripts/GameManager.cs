using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    ShakerScript shaker;

    Vector2 glassPosition = new Vector2(-6.69f, -2f);
    [SerializeField] GameObject cockTailGlass;
    [SerializeField] GameObject collinsGlass;

    //ステージ
    public int level;
    [SerializeField] int maxLevel;
    EnemyGenerateManager enemyGenerateManager;
    float nextWaitTime;
    float preWaitTime;
    [SerializeField] float kNextWaitTime;
    public bool isWait;
    bool isNextStage;

    //タイム


    //HUD
    Transform mainCanvas;
    [SerializeField] GameObject Fure;
    [SerializeField] GameObject Clear;
    [SerializeField] GameObject Restart;
    [SerializeField] GameObject Next;

    // Start is called before the first frame update
    void Start()
    {
        shaker = FindAnyObjectByType<ShakerScript>();
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<RectTransform>();
        enemyGenerateManager = FindAnyObjectByType<EnemyGenerateManager>();

        if (Random.Range(0, 1000) > 500)
        {
            Instantiate(cockTailGlass, glassPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(collinsGlass, glassPosition, Quaternion.identity);
        }

        Instantiate(Fure, mainCanvas);

        level++;
        enemyGenerateManager.isGenerate = true;
    }

    // Update is called once per frame
    void Update()
    {
        GenerateGlass();
    }

    void GenerateGlass()
    {
        if (FindAnyObjectByType<GlassScript>() == null)
        {
            nextWaitTime += Time.deltaTime;
            isWait = true;

            if (nextWaitTime > 4.5f)
            {
                if (preWaitTime < 4.5f)
                {
                    if (shaker.cocktailAmount > 39)
                    {
                        Instantiate(Clear, mainCanvas);
                    }
                    else
                    {
                        Instantiate(Restart, mainCanvas);
                    }
                }
            }

            if (nextWaitTime > 7f)
            {
                if (level < maxLevel)
                {
                    if (preWaitTime < 7f)
                    {
                        Instantiate(Next, mainCanvas);
                    }
                }
                else
                {
                    SceneManager.LoadScene("Clear");
                }
            }

            if (nextWaitTime > kNextWaitTime)
            {
                isNextStage = true;
                isWait = false;
            }

            if (isNextStage)
            {
                if (Random.Range(0, 1000) > 500)
                {
                    Instantiate(cockTailGlass, glassPosition, Quaternion.identity);
                }
                else
                {
                    Instantiate(collinsGlass, glassPosition, Quaternion.identity);
                }

                Instantiate(Fure, mainCanvas);

                if (shaker.cocktailAmount > 39)
                {
                    level++;
                }

                enemyGenerateManager.isGenerate = true;

                isNextStage = false;
            }

            preWaitTime = nextWaitTime;
        }
    }
}
