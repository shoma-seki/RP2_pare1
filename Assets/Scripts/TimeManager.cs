using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    private GameManager gameManager;
    [SerializeField] private GameObject timeNeedle;

    [SerializeField] private Image countdownImage;            // 中央に表示するImage
    [SerializeField] private Sprite[] numberSprites;          // 1〜10の数字スプライト（index 0 が「1」）

    private float maxTime;
    private int lastDisplayedSecond = -1; // 最後に表示した秒数

    void Start() {
        gameManager = FindAnyObjectByType<GameManager>();
        maxTime = gameManager.gameTime;

        // 最初は透明にして非表示
        if (countdownImage != null)
            countdownImage.color = new Color(1f, 1f, 1f, 0f);
    }

    void Update() {
        float currentTime = gameManager.gameTime;

        // 時間の範囲を制限
        currentTime = Mathf.Clamp(currentTime, 0f, maxTime);

        // 針の回転処理（時計回り）
        float ratio = 1f - (currentTime / maxTime);
        float angle = ratio * 360f;
        timeNeedle.transform.rotation = Quaternion.Euler(0f, 0f, -angle);

        // カウントダウン演出処理（10秒以下）
        int currentSecond = Mathf.CeilToInt(currentTime);
        if (currentSecond <= 10 && currentSecond > 0 && currentSecond != lastDisplayedSecond) {
            lastDisplayedSecond = currentSecond;
            ShowCountdown(currentSecond);
        }
    }

    void ShowCountdown(int number) {
        if (number < 1 || number > 10 || countdownImage == null || numberSprites.Length < 10)
            return;

        countdownImage.sprite = numberSprites[number - 1];
        StartCoroutine(FadeInOut(countdownImage));
    }

    IEnumerator FadeInOut(Image img) {
        float duration = 1f;
        float half = duration / 2f;

        // 最初は5倍サイズで透明からスタート
        Vector3 startScale = Vector3.one * 5f;
        Vector3 endScale = Vector3.one * 4;

        img.transform.localScale = startScale;
        img.color = new Color(1f, 1f, 1f, 0f);

        // フェードイン＆縮小
        float t = 0f;
        while (t < half) {
            t += Time.deltaTime;
            float progress = t / half;

            img.color = new Color(1f, 1f, 1f, progress); // alpha: 0 → 1
            img.transform.localScale = Vector3.Lerp(startScale, endScale, progress);
            yield return null;
        }

        // フェードアウト（サイズはそのまま）
        t = 0f;
        while (t < half) {
            t += Time.deltaTime;
            float progress = t / half;

            img.color = new Color(1f, 1f, 1f, 1f - progress); // alpha: 1 → 0
            yield return null;
        }

        img.color = new Color(1f, 1f, 1f, 0f);
    }

}
