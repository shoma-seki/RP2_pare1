using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    Image image;
    bool isChangeScene;
    float t;
    [SerializeField] float kChangeTime;
    float changeTime;
    float preChangeTime;

    bool isChanged;

    string changedScene;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isChangeScene)
        {
            if (!isChanged)
            {
                changeTime += Time.deltaTime;
                t = changeTime / kChangeTime;

                image.color = Color.Lerp(Color.clear, Color.black, t);
            }

            if (preChangeTime < kChangeTime && changeTime >= kChangeTime)
            {
                SceneManager.LoadScene(changedScene);
                isChanged = true;
            }

            if (isChanged)
            {
                changeTime -= Time.deltaTime;
                t = changeTime / kChangeTime;

                image.color = Color.Lerp(Color.clear, Color.black, t);
            }

            if (changeTime > kChangeTime)
            {
                isChangeScene = false;
                changeTime = 0;
            }

            preChangeTime = changeTime;

            Debug.Log("t = " + t);
        }

    }

    public void SceneChange(string changedScene)
    {
        this.changedScene = changedScene;
        isChangeScene = true;
        isChanged = false;
    }
}
