using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManagerScript : MonoBehaviour
{
    float interval;
    [SerializeField] float kInterval;

    int cloudCount;

    [SerializeField] CloudScript cloud;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        interval -= Time.deltaTime;
        if (interval < 0)
        {
            cloudCount++;
            interval = kInterval;

            float rand = Random.Range(0, 1000);

            if (rand > 500)
            {
                if (Random.Range(0, 100) > 50)
                {
                    Instantiate(cloud, new Vector3(-23, Random.Range(55, 65), -15), Quaternion.identity);
                }
                else
                {
                    Instantiate(cloud, new Vector3(23, Random.Range(55, 65), -15), Quaternion.identity);
                }
            }
            else if (rand > 250)
            {
                if (Random.Range(0, 100) > 50)
                {
                    Instantiate(cloud, new Vector3(-23, Random.Range(35, 40), -15), Quaternion.identity);
                }
                else
                {
                    Instantiate(cloud, new Vector3(23, Random.Range(35, 40), -15), Quaternion.identity);
                }
            }
            else if (rand > 100)
            {
                if (Random.Range(0, 100) > 50)
                {
                    Instantiate(cloud, new Vector3(-23, Random.Range(20, 30), -15), Quaternion.identity);
                }
                else
                {
                    Instantiate(cloud, new Vector3(23, Random.Range(20, 30), -15), Quaternion.identity);
                }
            }
        }
    }
}
