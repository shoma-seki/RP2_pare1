using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    ShakerScript shaker;
    GlassScript glass;

    Vector2 glassPosition = new Vector2(-4.97f, -6.03f);
    [SerializeField] GameObject cockTailGlass;
    [SerializeField] GameObject collinsGlass;

    EnemyGenerateManager enemyGenerateManager;

    //ƒ^ƒCƒ€
    public float gameTime;
    [SerializeField] float kGameTime = 60f;

    bool isEnd;
    bool isClear;

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

        changeScene = FindAnyObjectByType<ChangeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shaker.isGrounded)
        {
            gameTime -= Time.deltaTime;
        }

        if (gameTime < 0)
        {
            isEnd = true;
            glass = FindAnyObjectByType<GlassScript>();
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

            if (endWaitTime > 0.5f && preEndWaitTime <= 0.5f)
            {
                Instantiate(Oke, mainCanvas);
            }

            if (endWaitTime > kEndWaitTime)
            {
                changeScene.SceneChange("ClearScene");
            }

            preEndWaitTime = endWaitTime;
        }

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
}
