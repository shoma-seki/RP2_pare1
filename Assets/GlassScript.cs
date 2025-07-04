using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlassScript : MonoBehaviour
{
    Player player;
    ShakerScript shaker;

    Vector3 position;
    Vector2 targetPosition;
    Vector2 prePosition;

    Vector2 scale;

    Vector3 rotation;

    bool isMouse;
    bool isGrabbed;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        shaker = FindAnyObjectByType<ShakerScript>();

        position = transform.position;
        position.z = 30f;
        transform.position = position;
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        prePosition = transform.position;

        Grab();
        Move();
    }

    void Grab()
    {
        if (isMouse && shaker.isCompleted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isGrabbed = true;

                scale = Vector2.one * 0.32f;
                transform.localScale = scale;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isGrabbed = false;
                //Debug.Log("—£‚µ‚½‚æ");
            }
        }
    }

    void Move()
    {
        if (isGrabbed)
        {
            targetPosition = player.transform.position - new Vector3(0, 0.5f);
            position = Vector2.Lerp(position, targetPosition, 30f * Time.deltaTime);
            position.z = -5f;
            transform.position = position;

            Vector2 velocity = (Vector2)position - prePosition;

            //‰ñ“]        
            if (velocity.magnitude < 0.1f)
            {
                rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 0), 5f * Time.deltaTime);
                transform.rotation = Quaternion.Euler(rotation);
            }
            else
            {
                if (velocity.x < 0)
                {
                    rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 40f), 5f * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(rotation);
                }

                if (velocity.x > 0)
                {
                    rotation = Vector3.Lerp(rotation, new Vector3(0, 0, -40f), 5f * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(rotation);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            isMouse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            isMouse = false;
        }
    }
}
