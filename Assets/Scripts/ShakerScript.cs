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

    //�X�^�[�g�ɖ߂�
    Vector2 startPosition;

    //���ˌW��
    [SerializeField] float reflection;

    //���g
    float shakeSpeed;       //�U�����X�s�[�h
    float shakeDistance;    //�U��������
    float shakeCount;       //�U������
    float cocktailAmount;   //�J�N�e���̗�
    Vector2 preShakePoint;  //�O��V�F�C�N�����ꏊ
    public float cocktailProgress; //�J�N�e���̊����x   
    public float cocktailProgressMax;

    //�U��Ƃ��̂��
    Vector2 direction;      //�i��ł������
    Vector2 previousDirection;
    float directionChangeThreshold = 140f; // �p�x����45�x�ȏ�Łu�}�ρv�Ɣ��f

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        position = transform.position;

        //�X�^�[�g�ɖ߂��|�W�V����
        startPosition = position;
    }

    private void FixedUpdate()
    {
        prePosition = transform.position;

        if (!isGrounded)
        {
            Move();
            Shake();
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

        //�X�^�[�g�ɖ߂�
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPosition;
            position = transform.position;
            transform.rotation = Quaternion.identity;
            isGrounded = true;
        }
    }

    void Shake()
    {
        //�V�F�C�N�̃X�s�[�h
        shakeSpeed = ((Vector2)transform.position - prePosition).magnitude;

        //�V�F�C�N�̃^�C�~���O
        direction = ((Vector2)transform.position - prePosition).normalized;
        Vector2 currentDirection = direction.normalized; // ���݂̐i�s�����ivelocity�Ȃǂ�z��j

        if (previousDirection != Vector2.zero)
        {
            float angleDiff = Vector2.Angle(previousDirection, currentDirection);

            if (angleDiff > directionChangeThreshold)
            {
                shakeCount++;
                Debug.Log("shakeCount" + shakeCount);

                if (shakeCount >= 2)
                {
                    shakeDistance = Vector2.Distance(preShakePoint, transform.position);

                    //�J�N�e���̊����x��ύX
                    cocktailProgress += shakeDistance * shakeSpeed / 10f;
                    Debug.Log("�J�N�e���̊����x" + cocktailProgress);
                }

                preShakePoint = transform.position;
            }
        }

        previousDirection = currentDirection;
    }

    void Move()
    {
        if (isGrabed)
        {
            targetPosition = player.transform.position;
            position = Vector2.Lerp(position, targetPosition, 30f * Time.deltaTime);

            //��]
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

            //��]�߂�
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
            if (Input.GetMouseButtonDown(0))
            {
                isGrounded = false;
                isGrabed = true;

                //Debug.Log("���񂾂�");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isGrabed = false;
            //Debug.Log("��������");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            isMouse = true;
            ///Debug.Log("�}�E�X�I�[�o�[����");
        }

        if (collision.tag == "Wall")
        {
            if (transform.position.x < 0)
            {
                velocity = new Vector2(1, 1) / reflection;
            }

            if (transform.position.x > 0)
            {
                velocity = new Vector2(-1, 1) / reflection;
            }
        }

        if (collision.tag == "Counter")
        {
            velocity = Vector2.Reflect(velocity.normalized, Vector2.up) / reflection;
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
