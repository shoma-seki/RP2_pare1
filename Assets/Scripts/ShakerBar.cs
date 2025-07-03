using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerBar : MonoBehaviour {
    [SerializeField] GameObject shakerBar;
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
        // cocktailProgress(100〜0) → scaleX(1.0〜0.0) に変換
        float scaleX = Mathf.InverseLerp(0f, 100f, shakerScript.cocktailProgress); // cocktailProgressが100のとき1、0のとき0になる
        Vector3 scale = shakerBar.transform.localScale;
        scale.x = scaleX;
        shakerBar.transform.localScale = scale;
    }
}
