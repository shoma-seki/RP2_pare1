using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerScript : MonoBehaviour
{
    Player player;

    Vector2 position;
    Vector2 targetPosition;

    Vector2 prePosition;
    Vector2 preVelocity;
    Vector2 velocity;
    [SerializeField] Vector2 gravity;

    Vector3 rotation;

    bool isMouse;
    bool isGrabed;
    bool isGrounded = true;

    //スタートに戻す
    Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        position = transform.position;

        //スタートに戻すポジション
        startPosition = position;
    }

    private void FixedUpdate()
    {
        prePosition = transform.position;

        if (!isGrounded)
        {
            Move();
        }

        preVelocity = (Vector2)transform.position - prePosition;
    }

    // Update is called once per frame
    void Update()
    {
        Grab();

        if (Input.GetMouseButtonUp(0))
        {
            velocity = preVelocity;
        }

        //スタートに戻す
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPosition;
            position = transform.position;
            transform.rotation = Quaternion.identity;
            isGrounded = true;
        }
    }

    void Move()
    {
        if (isGrabed)
        {
            targetPosition = player.transform.position;
            position = Vector2.Lerp(position, targetPosition, 30f * Time.deltaTime);

            //回転
            rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 100f), 5f * Time.deltaTime);
            transform.rotation = Quaternion.Euler(rotation);
        }

        if (!isGrabed && !isGrounded)
        {
            if (velocity.y < gravity.y)
            {
                velocity.y = gravity.y;
            }

            velocity += gravity * Time.deltaTime;
            position += velocity * 45f * Time.deltaTime;

            //回転戻す
            float angle = Mathf.Atan2(velocity.normalized.y, velocity.normalized.x) * Mathf.Rad2Deg;
            rotation = Vector3.Lerp(rotation, new Vector3(0, 0, angle + 90), 5f * Time.deltaTime);
            transform.rotation = Quaternion.Euler(rotation);
            rotation = transform.rotation.eulerAngles;
        }

        transform.position = position;
    }

    void Grab()
    {
        if (isMouse)
        {
            if (Input.GetMouseButton(0))
            {
                isGrounded = false;
                isGrabed = true;

                //Debug.Log("つかんだよ");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isGrabed = false;
            //Debug.Log("離したよ");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            isMouse = true;
           ///Debug.Log("マウスオーバーだお");
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
