using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    ShakerScript target;        // 対象オブジェクト
    public Vector2 targetSize = new Vector2(5f, 3f);  // ターゲットの見せたいサイズ（ワールド単位）
    public float aspectRatio = 16f / 9f;  // 固定アスペクト比
    public float padding = 0.5f;          // 余白

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        target = FindAnyObjectByType<ShakerScript>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        FitCameraToTarget();
    }

    void FitCameraToTarget()
    {
        float length = Vector2.Distance(target.transform.position, new Vector2(-0.02f, -4.4f));

        cam.orthographicSize = length / 2f;
        if (cam.orthographicSize < 5)
        {
            cam.orthographicSize = 5;
        }
        if(cam.orthographicSize > 15)
        {
            cam.orthographicSize = 15;
        }

        cam.transform.position = Vector3.Lerp(target.transform.position, new Vector2(-0.02f, -4.4f), 0.5f);
        cam.transform.position += new Vector3(0, 0, -30f);
    }
}
