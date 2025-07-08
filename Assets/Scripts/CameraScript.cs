using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    ShakerScript target;        // �ΏۃI�u�W�F�N�g
    public Vector2 targetSize = new Vector2(5f, 3f);  // �^�[�Q�b�g�̌��������T�C�Y�i���[���h�P�ʁj
    public float aspectRatio = 16f / 9f;  // �Œ�A�X�y�N�g��
    public float padding = 0.5f;          // �]��

    private Camera cam;
    Vector3 position;
    Vector2 targetPosition;

    //�����ł�Ƃ��̃Y�[��
    ShakerScript shaker;

    // Start is called before the first frame update
    void Start()
    {
        target = FindAnyObjectByType<ShakerScript>();
        cam = Camera.main;
        shaker = FindAnyObjectByType<ShakerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        FitCameraToTarget();
        ZoomToGlass();
    }

    void FitCameraToTarget()
    {
        if (!shaker.isPour)
        {
            float length = Vector2.Distance(target.transform.position, new Vector2(0, -6.4f));

            if (target.transform.position.y > 8f)
            {
                cam.orthographicSize = length / 2f;
                targetPosition = Vector2.Lerp(target.transform.position, new Vector2(0f, -6.4f), 0.3f);
                position = Vector2.Lerp(position, targetPosition, 5f * Time.deltaTime);
            }
            else
            {
                position = Vector2.Lerp(position, Vector2.zero, 5f * Time.deltaTime);
            }

            if (cam.orthographicSize < 8)
            {
                cam.orthographicSize = 8;
            }
            if (cam.orthographicSize > 30)
            {
                cam.orthographicSize = 30;
            }

            position.x = 0;
            position.z = -30f;
            if (position.y < 0)
            {
                position.y = 0;
            }
            cam.transform.position = position;
        }
    }

    void ZoomToGlass()
    {
        if (shaker.isPour)
        {

        }
    }
}
