using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerTopScript : MonoBehaviour
{
    ShakerScript shaker;

    Vector3 position;
    Vector2 targetPosition;

    [SerializeField] float t;

    // Start is called before the first frame update
    void Start()
    {
        shaker = FindAnyObjectByType<ShakerScript>();
        position = shaker.transform.position;
        targetPosition = shaker.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = shaker.transform.position;
        position = Vector2.Lerp(position, targetPosition, t * Time.deltaTime);
        position.z = -11f;
        transform.position = position;
        transform.rotation = Quaternion.Lerp(transform.rotation, shaker.transform.rotation, t * Time.deltaTime);
    }
}
