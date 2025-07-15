using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorBallScript : MonoBehaviour
{
    Vector3 rotation;

    // Update is called once per frame
    void Update()
    {
        rotation.y += 70f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
