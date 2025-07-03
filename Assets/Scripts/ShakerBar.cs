using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerBar : MonoBehaviour {
    [SerializeField] GameObject shakerBar;
    [SerializeField] private float testNum = 100;

    void Start() {
        
    }

    void Update() {
        // UIの動きを確かめる用のデバッグボタン
        if (Input.GetKeyDown(KeyCode.P)) {
            testNum -= 10f; // テスト用に値を減らす
            testNum = Mathf.Clamp(testNum, 0f, 100f);
            BarUpdate();
        }
    }

    // UIを動かす処理
    private void BarUpdate() {
        // testNum(100〜0) → scaleY(1.0〜0.0) に変換
        float scaleY = Mathf.InverseLerp(0f, 100f, testNum); // testNumが100のとき1、0のとき0になる
        Vector3 scale = shakerBar.transform.localScale;
        scale.y = scaleY;
        shakerBar.transform.localScale = scale;
    }
}
