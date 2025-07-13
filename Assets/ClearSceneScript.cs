using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearSceneScript : MonoBehaviour
{
    ChangeScene changeScene;

    private void Start()
    {
        changeScene = FindAnyObjectByType<ChangeScene>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            changeScene.SceneChange("TitleScene");
        }
    }
}
