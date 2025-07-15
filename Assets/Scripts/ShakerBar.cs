using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShakerBar : MonoBehaviour {
    [SerializeField] ShakerScript shakerScript;
    [SerializeField] GameObject tipBar;

    private float previousProgress = 0f;
    private Coroutine tipBarCoroutine;
    private float scaleFactor = 0.05f; // 1 progress あたり 0.1 スケール → 100 progress でスケール10
    [SerializeField] Text cocktailProgressText;
    void Update() {
        // デバッグ用
        if (Input.GetKeyDown(KeyCode.P)) {
            shakerScript.cocktailProgress += 10f;
        }

        BarUpdate();
        CheckProgressChange();
        UpdateCocktailProgressText();
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
}
