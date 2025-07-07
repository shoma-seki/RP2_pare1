using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Vector2 glassPosition = new Vector2(-6.69f, -2f);
    [SerializeField] GameObject cockTailGlass;
    [SerializeField] GameObject collinsGlass;

    //ステージ
    int level;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GenerateGlass();
    }

    void GenerateGlass()
    {
        if (FindAnyObjectByType<GlassScript>() == null)
        {
            if (Random.Range(0, 1000) > 500)
            {
                Instantiate(cockTailGlass, glassPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(collinsGlass, glassPosition, Quaternion.identity);
            }

            level++;
        }
    }
}
