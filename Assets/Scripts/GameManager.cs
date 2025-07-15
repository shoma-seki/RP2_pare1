using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    ShakerScript shaker;
    GlassScript glass;

    Vector2 glassPosition = new Vector3(-2.79f, -5.51f);
    [SerializeField] GameObject cockTailGlass;
    [SerializeField] GameObject collinsGlass;

    EnemyGenerateManager enemyGenerateManager;

    //タイム
    public float gameTime;
    float preGameTime;
    [SerializeField] float kGameTime = 60f;

    bool isEnd;
    bool isClear;

    //フィーバー関連
    public bool isFever;
    float nextFeverTime;
    [SerializeField] float kNextFeverTime;      //何秒おきにフィーバータイムになるか
    float feverTime;
    [SerializeField] float kFeverTime;         //フィーバータイムが続く時間

    [SerializeField] float kEndWaitTime;
    float endWaitTime;
    float preEndWaitTime;

    //HUD
    Transform mainCanvas;
    [SerializeField] GameObject Fure;
    [SerializeField] GameObject Clear;
    [SerializeField] GameObject Oke;

    ChangeScene changeScene;

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

        enemyGenerateManager.isGenerate = true;

        gameTime = kGameTime;

        feverTime = kFeverTime;
        nextFeverTime = kNextFeverTime;

        changeScene = FindAnyObjectByType<ChangeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        Fever();

        if (!shaker.isGrounded)
        {
            gameTime -= Time.deltaTime;
        }

        if (gameTime < 0 && preGameTime >= 0)
        {
            isEnd = true;
            glass = FindAnyObjectByType<GlassScript>();
            Instantiate(Oke, mainCanvas);
        }

        if (isEnd)
        {
            if (glass == null)
            {
                isClear = true;
            }
        }

        if (isClear)
        {
            endWaitTime += Time.deltaTime;

            if (endWaitTime > kEndWaitTime)
            {
                changeScene.SceneChange("ClearScene");
            }

            preEndWaitTime = endWaitTime;
        }

        preGameTime = gameTime;

        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("TitleScene");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene("GameScene");
        }

        //preRestartTime = restartTime;
        //Debug.Log("gameTime  " + gameTime);
    }

    void Fever()
    {
        if (!shaker.isGrounded && !isEnd)
        {
            if (!isFever)
            {
                nextFeverTime -= Time.deltaTime;
                if (nextFeverTime < 0)
                {
                    isFever = true;
                    feverTime = kFeverTime;
                }
            }

            if (isFever)
            {
                feverTime -= Time.deltaTime;
                if (feverTime < 0)
                {
                    nextFeverTime = kNextFeverTime;
                    isFever = false;
                }
            }
        }
    }
}
