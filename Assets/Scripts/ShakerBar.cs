using System.Collections;
using UnityEngine;

public class ShakerBar : MonoBehaviour
{
    [SerializeField] ShakerScript shakerScript;
    [SerializeField] GameObject tipBar;

    private float previousProgress = 0f;
    private bool isWaitingToStretch = false;

    void Update()
    {
        // デバッグ用
        if (Input.GetKeyDown(KeyCode.P))
        {
            shakerScript.cocktailProgress += 10f;
        }

        BarUpdate();
        CheckProgressChange();
    }

    private void BarUpdate()
    {
        float normalized = Mathf.InverseLerp(0f, shakerScript.cocktailProgressMax, shakerScript.cocktailProgress);
        float scaleX = Mathf.Lerp(0f, 10f, normalized);

        Vector3 scale = transform.localScale;
        scale.x = scaleX;
        transform.localScale = scale;
    }

    private void CheckProgressChange()
    {
        float currentProgress = shakerScript.cocktailProgress;

        if (!Mathf.Approximately(currentProgress, previousProgress))
        {
            previousProgress = currentProgress;

            // すでに待機中でなければコルーチンスタート
            if (!isWaitingToStretch)
            {
                isWaitingToStretch = true;
                StartCoroutine(AnimateTipBarAfterDelay(2f, 0.5f)); //目標値は中で取得する
            }
        }
    }

    private IEnumerator AnimateTipBarAfterDelay(float delay, float duration)
    {
        yield return new WaitForSeconds(delay);

        float fromX = tipBar.transform.localScale.x;

        //ここで改めて "今のBarの長さ" を取得
        float currentBarNormalized = Mathf.InverseLerp(0f, 100f, shakerScript.cocktailProgress);
        float toX = Mathf.Lerp(0f, 10f, currentBarNormalized);

        float timer = 0f;
        while (timer < duration)
        {
            float t = timer / duration;
            float scaleX = Mathf.Lerp(fromX, toX, t);

            Vector3 scale = tipBar.transform.localScale;
            scale.x = scaleX;
            tipBar.transform.localScale = scale;

            timer += Time.deltaTime;
            yield return null;
        }

        // 最終反映
        Vector3 finalScale = tipBar.transform.localScale;
        finalScale.x = toX;
        tipBar.transform.localScale = finalScale;

        isWaitingToStretch = false;
    }

}
