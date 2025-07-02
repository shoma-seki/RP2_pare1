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

    bool isMouse;
    bool isGrabed;
    bool isGrounded = true;

    //�X�^�[�g�ɖ߂�
    Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        position = transform.position;

        //�X�^�[�g�ɖ߂��|�W�V����
        startPosition = position;
    }

    private void LateUpdate()
    {
    }

    // Update is called once per frame
    void Update()
    {
        prePosition = transform.position;

        if (!isGrounded)
        {
            Move();
        }

        Grab();

        //�X�^�[�g�ɖ߂�
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPosition;
            position = transform.position;
            isGrounded = true;
        }

        preVelocity = (Vector2)transform.position - prePosition;
    }

    void Move()
    {
        if (isGrabed)
        {
            targetPosition = player.transform.position;
            position = Vector2.Lerp(position, targetPosition, 10f * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            velocity = preVelocity;
        }

        if (!isGrabed && !isGrounded)
        {
            if (velocity.y < gravity.y)
            {
                velocity.y = gravity.y;
            }

            velocity += gravity * Time.deltaTime;
            position += velocity;
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

                Debug.Log("���񂾂�");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isGrabed = false;
            Debug.Log("��������");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            isMouse = true;
            Debug.Log("�}�E�X�I�[�o�[����");
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
