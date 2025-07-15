using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkScript : MonoBehaviour
{
    ShakerScript shaker;
    ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        shaker = FindAnyObjectByType<ShakerScript>();
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var emitter = particle.emission;
        if (!shaker.isGrabbed)
        {
            emitter.enabled = false;
        }
        else
        {
            emitter.enabled = true;
        }
    }
}
