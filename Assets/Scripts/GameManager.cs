using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    ShakerScript shaker;

    Vector2 glassPosition = new Vector2(-4.97f, -6.03f);
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
    public float gameTime;
    [SerializeField] readonly float kGameTime = 60f;
    public bool isRestart;
    float restartTime;
    float preRestartTime;

    //HUD
    Transform mainCanvas;
    [SerializeField] GameObject Fure;
    [SerializeField] GameObject Clear;
    [SerializeField] GameObject Restart;
    [SerializeField] GameObject Next;
    [SerializeField] GameObject TimeUp;

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

        gameTime = kGameTime;
    }

    // Update is called once per frame
    void Update()
    {
        GenerateGlass();

        if (!shaker.isGrounded && shaker.cocktailProgress <= 99)
        {
            gameTime -= Time.deltaTime;
        }

        if (gameTime < 0 && shaker.cocktailProgress <= 99)
        {
            isRestart = true;
        }

        if (isRestart)
        {
            restartTime += Time.deltaTime;

            if (preRestartTime <= 1f && restartTime > 1f)
            {
                Instantiate(TimeUp, mainCanvas);
            }

            if (restartTime > 5f)
            {
                RestartAll();
                isRestart = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("TitleScene");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene("GameScene");
        }

        preRestartTime = restartTime;
        Debug.Log("gameTime  " + gameTime);
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
                    if (shaker.cocktailProgress > 39)
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

                if (level == maxLevel)
                {
                    SceneManager.LoadScene("ClearScene");
                }
            }

            if (nextWaitTime > kNextWaitTime)
            {
                isNextStage = true;
                isWait = false;
            }

            if (isNextStage)
            {
                RestartAll();
                nextWaitTime = 0;
            }

            preWaitTime = nextWaitTime;
        }
    }

    void RestartAll()
    {
        if (FindAnyObjectByType<GlassScript>() == null)
        {
            if (Random.Range(0, 1000) > 500)
            {
                Instantiate(cockTailGlass, glassPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(collinsGlass, glassPosition, Quaternion.identity);
            }
        }

        Instantiate(Fure, mainCanvas);

        if (!isRestart)
        {
            if (shaker.cocktailProgress > 39)
            {
                level++;
            }
        }

        gameTime = kGameTime;

        enemyGenerateManager.isGenerate = true;

        isNextStage = false;
    }
}
