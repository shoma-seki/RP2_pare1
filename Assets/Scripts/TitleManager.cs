using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {
    private TitleShake titleShake;

    [SerializeField] private float cocktailProgressMax = 10f;

    [Header("Wave設定")]
    [SerializeField] private List<GameObject> waves = new List<GameObject>();
    [SerializeField] private float waveEndYOffset = 4f;
    [SerializeField] private float smoothTime = 0.3f;

    private class WaveData {
        public GameObject waveObject;
        public float startY;
        public float currentY;
        public float velocityY;

        public WaveData(GameObject obj) {
            waveObject = obj;
            startY = obj.transform.position.y;
            currentY = startY;
            velocityY = 0f;
        }
    }

    private List<WaveData> waveDataList = new List<WaveData>();

    private float lastProgress;
    private float progressTimer = 0f;
    private float resetDelay = 3f; // 3秒間進捗がなければリセット

    void Start() {
        titleShake = FindAnyObjectByType<TitleShake>();

        foreach (var wave in waves) {
            if (wave != null)
                waveDataList.Add(new WaveData(wave));
        }

        lastProgress = titleShake?.cocktailProgress ?? 0f;
    }

    void Update() {
        if (titleShake == null || waveDataList.Count == 0)
            return;

        float currentProgress = titleShake.cocktailProgress;

        // 進捗に変化があったか判定
        if (Mathf.Approximately(currentProgress, lastProgress)) {
            progressTimer += Time.deltaTime;

            // 3秒進捗が増えなかったらリセット
            if (progressTimer >= resetDelay) {
                titleShake.cocktailProgress = 0f;
                progressTimer = 0f;
            }
        } else {
            progressTimer = 0f;
            lastProgress = currentProgress;
        }

        // 再計算後の progressRate
        float progressRate = Mathf.Clamp01(titleShake.cocktailProgress / cocktailProgressMax);

        foreach (var waveData in waveDataList) {
            float targetY = waveData.startY + waveEndYOffset * progressRate;

            waveData.currentY = Mathf.SmoothDamp(waveData.currentY, targetY, ref waveData.velocityY, smoothTime);

            Vector3 pos = waveData.waveObject.transform.position;
            pos.y = waveData.currentY;
            waveData.waveObject.transform.position = pos;
        }

        if (titleShake.cocktailProgress >= cocktailProgressMax) {
            Debug.Log("NiceShake");
        }
    }
}
