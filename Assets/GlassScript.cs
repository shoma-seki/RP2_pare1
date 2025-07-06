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
    public bool isGrounded;
    bool isTouched;

    public bool isFull;
    bool isFullGrabbed;
    bool isClear;

    float releaseHeight;

    //注ぎ判定フィールド
    [SerializeField] GameObject pourField;

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
        Clear();

        //Debug.Log("isGrabbed" + isGrabbed);
        //Debug.Log("releaseHeight" + releaseHeight);
    }

    void Grab()
    {
        if (isMouse && shaker.isCompleted && !isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isGrabbed = true;
                isTouched = true;
                releaseHeight = -10;

                scale = Vector2.one * 0.32f;
                transform.localScale = scale;
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

            //回転        
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

            if (Input.GetMouseButtonUp(0))
            {
                isGrabbed = false;
                releaseHeight = transform.position.y;
            }
        }

        if (!isGrabbed && !isGrounded && isTouched)
        {
            position.y -= 6f * Time.deltaTime;
            transform.position = position;

            rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 0), 5f * Time.deltaTime);
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    void Clear()
    {
        if (isGrounded && isFull)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isFullGrabbed = true;
            }

            if (isFullGrabbed)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    isClear = true;
                }
            }

            if (isClear)
            {
                position.x += 10f * Time.deltaTime;
                transform.position = position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            isMouse = true;
        }

        if (collision.tag == "Counter")
        {
            if (releaseHeight < -4)
            {
                isGrounded = true;
                Instantiate(pourField, transform.position, Quaternion.identity);
            }
            else
            {
                Destroy(gameObject);
            }
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
