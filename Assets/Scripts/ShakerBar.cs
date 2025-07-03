using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerBar : MonoBehaviour {
    [SerializeField] ShakerScript shakerScript;

    void Start() {
        
    }

    void Update() {
        // UIの動きを確かめる用のデバッグボタン
        if (Input.GetKeyDown(KeyCode.P)) {
            
        }
        //バーの更新（常に）
        BarUpdate();
    }

    // UIを動かす処理
    private void BarUpdate() {
        // cocktailProgress(0〜100) → scaleX(0.0〜10.0)
        float normalized = Mathf.InverseLerp(0f, 100f, shakerScript.cocktailProgress); // 0〜1に正規化
        float scaleX = Mathf.Lerp(0f, 10f, normalized); // 0〜10に変換

        Vector3 scale = transform.localScale;
        scale.x = scaleX;
        transform.localScale = scale;
    }

}
