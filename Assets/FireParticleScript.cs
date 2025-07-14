using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticleScript : MonoBehaviour
{
    ShakerScript shaker;
    [SerializeField] Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        shaker = FindAnyObjectByType<ShakerScript>();
        transform.position = Camera.main.transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.transform.position + offset;
    }
}
