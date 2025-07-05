using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourFieldScript : MonoBehaviour
{
    ShakerScript shaker;

    // Start is called before the first frame update
    void Start()
    {
        shaker = FindAnyObjectByType<ShakerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shaker.isGrounded)
        {
            Destroy(gameObject);
        }
    }
}
