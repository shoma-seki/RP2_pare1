using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    float swichTime;
    Vector3 position;
    bool isRight = true;

    private void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        swichTime += Time.deltaTime;
        if (swichTime > 1f)
        {
            isRight = !isRight;
            swichTime = 0;
        }

        if (isRight)
        {
            position.x += 1f * Time.deltaTime;
        }

        if (!isRight)
        {
            position.x -= 1f * Time.deltaTime;
        }

        position.z = -20f;
        transform.position = position;
    }
}
