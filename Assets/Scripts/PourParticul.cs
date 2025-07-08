using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourParticul : MonoBehaviour
{
    ShakerScript shakerScript;
    // Start is called before the first frame update
    void Start()
    {
        shakerScript = FindAnyObjectByType<ShakerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(shakerScript.transform.position.x, shakerScript.transform.position.y, 10f);
    }
}
