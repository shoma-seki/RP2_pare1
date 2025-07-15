using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverManager : MonoBehaviour
{
    [SerializeField] GameObject feverLightRight;
    [SerializeField] GameObject feverLightLeft;
    [SerializeField] Transform VFXCanvas;
    GameManager gameManager;

    float lightInterval;
    [SerializeField] float kLightInterval;

    int lightCount;

    CameraScript mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        lightInterval = kLightInterval;
        mainCamera = FindAnyObjectByType<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isFever)
        {
            lightInterval -= Time.deltaTime;
            if (lightInterval < 0)
            {
                if (lightCount % 4 == 0 || lightCount % 4 == 1)
                {
                    GameObject newLight = Instantiate(feverLightLeft, VFXCanvas);
                    Color color = mainCamera.pColor;
                    color.a = 0.3f;
                    newLight.GetComponent<LightScript>().SetColor(color);
                }
                else
                {
                    GameObject newLight = Instantiate(feverLightRight, VFXCanvas);
                    Color color = mainCamera.pColor;
                    color.a = 0.3f;
                    newLight.GetComponent<LightScript>().SetColor(color);
                }

                lightInterval = kLightInterval;
                lightCount++;
            }
        }
    }
}
