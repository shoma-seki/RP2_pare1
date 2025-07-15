using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShakerBar : MonoBehaviour {
    [SerializeField] ShakerScript shakerScript;
    [SerializeField] GameObject tipBar;
    private GameManager gameManager;

    private float previousProgress = 0f;
    private Coroutine tipBarCoroutine;
    private float scaleFactor = 0.05f; // 1 progress あたり 0.1 スケール → 100 progress でスケール10
    [SerializeField] Text cocktailProgressText;
    public static float saveCocktailProgress;

    private bool isFading = false;

    private void Start() {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update() {
        // デバッグ用
        if (Input.GetKeyDown(KeyCode.P)) {
            shakerScript.cocktailProgress += 10f;
        }

        BarUpdate();
        CheckProgressChange();
        UpdateCocktailProgressText();

        // スコアに変化があった場合だけ保存
        float currentProgress = shakerScript.cocktailProgress;

        if (!Mathf.Approximately(saveCocktailProgress, currentProgress)) {
            saveCocktailProgress = currentProgress;
            PlayerPrefs.SetFloat("CocktailProgress", saveCocktailProgress);
            PlayerPrefs.Save();
        }

        if (!isFading && gameManager.gameTime <= 10f) {
            StartCoroutine(FadeOutText(cocktailProgressText, 2f)); // 2秒でフェード
            isFading = true;
        }
    }

    private void BarUpdate() {
        float progress = shakerScript.cocktailProgress;
        float scaleX = progress * scaleFactor;

        Vector3 scale = transform.localScale;
        scale.x = scaleX;
        transform.localScale = scale;
    }

    private void CheckProgressChange() {
        float currentProgress = shakerScript.cocktailProgress;

        // 値に変化があったとき
        if (!Mathf.Approximately(currentProgress, previousProgress)) {
            previousProgress = currentProgress;

            // Coroutineが動いてなければ2秒後に追従を開始
            if (tipBarCoroutine == null) {
                tipBarCoroutine = StartCoroutine(AnimateTipBarAfterDelay(2f, 0.5f));
            }
        }
    }

    private IEnumerator AnimateTipBarAfterDelay(float delay, float duration) {
        yield return new WaitForSeconds(delay);

        float fromX = tipBar.transform.localScale.x;
        float toX = shakerScript.cocktailProgress * scaleFactor;

        float timer = 0f;
        while (timer < duration) {
            float t = timer / duration;
            float scaleX = Mathf.Lerp(fromX, toX, t);

            Vector3 scale = tipBar.transform.localScale;
            scale.x = scaleX;
            tipBar.transform.localScale = scale;

            timer += Time.deltaTime;
            yield return null;
        }

        Vector3 finalScale = tipBar.transform.localScale;
        finalScale.x = toX;
        tipBar.transform.localScale = finalScale;

        tipBarCoroutine = null;
    }

    private void UpdateCocktailProgressText() {
        int percent = Mathf.RoundToInt(shakerScript.cocktailProgress);
        cocktailProgressText.text = percent.ToString() + "%";
    }

    private IEnumerator FadeOutText(Text targetText, float duration) {
        Color originalColor = targetText.color;
        float timer = 0f;

        while (timer < duration) {
            float alpha = Mathf.Lerp(1f, 0f, timer / duration);
            targetText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            timer += Time.deltaTime;
            yield return null;
        }

        // 最終的に完全に透明に
        targetText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
