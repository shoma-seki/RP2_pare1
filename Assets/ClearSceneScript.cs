using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearSceneScript : MonoBehaviour
{
    ChangeScene changeScene;
    int score;
    [SerializeField] Text scoreText;

    private int displayedScore = 0;
    private int targetScore = 0;
    private float animationDuration = 3.0f; // 数字が3秒かけて増える
    private float animationTimer = 0f;

    [SerializeField] Image rankImage;               // ランクを表示するUI画像
    [SerializeField] Sprite[] rankSprites;          // 5枚の評価スプライト（S〜D）
    private bool rankShown = false;

    void Start() {
        changeScene = FindAnyObjectByType<ChangeScene>();

        targetScore = Mathf.RoundToInt(PlayerPrefs.GetFloat("CocktailProgress", 0f));
        displayedScore = 0;
        animationTimer = 0f;

        // ランク画像は最初は非表示にしておく
        rankImage.gameObject.SetActive(false);
    }

    void Update() {
        if (displayedScore < targetScore) {
            animationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(animationTimer / animationDuration);
            displayedScore = Mathf.RoundToInt(Mathf.Lerp(0, targetScore, t));
            scoreText.text = displayedScore.ToString() + "%";
        } else if (!rankShown) {
            // スコア表示が完了したら1度だけランクを表示
            ShowRank(targetScore);
            rankImage.gameObject.SetActive(true);
            rankShown = true;
        }

        if (Input.GetMouseButtonDown(0)) {
            PlayerPrefs.SetFloat("CocktailProgress", 0f);
            PlayerPrefs.Save();
            changeScene.SceneChange("TitleScene");
        }
    }
    private void ShowRank(int score) {
        int rankIndex = 0;

        if (score >= 1000)
            rankIndex = 0; // S
        else if (score >= 400)
            rankIndex = 1; // A
        else if (score >= 200)
            rankIndex = 2; // B
        else if (score >= 100)
            rankIndex = 3; // C
        else
            rankIndex = 4; // D

        if (rankSprites != null && rankSprites.Length > rankIndex) {
            rankImage.sprite = rankSprites[rankIndex];
        } else {
            Debug.LogWarning("ランクスプライトが不足しているか設定されていません");
        }
    }

}
