using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjamaScript : MonoBehaviour
{
    enum OjamaType
    {
        Stone, Poison
    }
    [SerializeField] OjamaType type;

    Vector3 position;
    Vector2 velocity;
    Vector2 direction;
    float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
