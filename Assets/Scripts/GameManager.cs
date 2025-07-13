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

    EnemyGenerateManager enemyGenerateManager;

    //ƒ^ƒCƒ€
    public float gameTime;
    [SerializeField] readonly float kGameTime = 60f;

    bool isEnd;

    [SerializeField] float kEndWaitTime;
    float endWaitTime;


    //HUD
    Transform mainCanvas;
    [SerializeField] GameObject Fure;
    [SerializeField] GameObject Clear;

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
        if (!shaker.isGrounded && shaker.cocktailProgress <= 99)
        {
            gameTime -= Time.deltaTime;
        }

        if (gameTime < 0)
        {
            isEnd = true;
        }

        if (isEnd)
        {
            endWaitTime += Time.deltaTime;
            if (endWaitTime > kEndWaitTime)
            {
                changeScene.SceneChange("ClearScene");
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

        //preRestartTime = restartTime;
        Debug.Log("gameTime  " + gameTime);
    }
}
