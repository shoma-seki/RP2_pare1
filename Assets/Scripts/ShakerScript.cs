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
    bool isGrabbed;
    bool isGrounded = true;
    bool isCollision;

    public bool isCompleted;

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
    float shakerHeight;     //�V�F�C�J�[�̍���

    //�����Ă�Ƃ��ɉ��Z
    [SerializeField] float rotationSpeed;
    float triggerRotation;

    //�U��Ƃ��̂��
    Vector2 direction;      //�i��ł������
    Vector2 previousDirection;
    float directionChangeThreshold = 140f; // �p�x����45�x�ȏ�Łu�}�ρv�Ɣ��f

    //����
    [SerializeField] Vector3 pourRotation;
    [SerializeField] Vector2 pourOffset;
    bool isPour;

    //�G�t�F�N�g
    [SerializeField] GameObject catchEffect;    //�L���b�`������
    [SerializeField] float catchOffset;

    [SerializeField] GameObject shakaEffect;    //�U�����Ƃ�
    [SerializeField] float shakaOffset;

    [SerializeField] GameObject gotuEffect;
    [SerializeField] float gotuOffset;

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
        Pour();

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

        //����
        if (cocktailProgress >= 100)
        {
            isCompleted = true;
        }

        //�R�}���h
#if UNITY_EDITOR
        Commands();
#endif
    }

    void Shake()
    {
        //�V�F�C�N�̃X�s�[�h
        shakeSpeed = ((Vector2)transform.position - prePosition).magnitude;

        //�V�F�C�N�̃^�C�~���O
        direction = ((Vector2)transform.position - prePosition).normalized;
        Vector2 currentDirection = direction.normalized; // ���݂̐i�s�����ivelocity�Ȃǂ�z��j

        if (!isCollision)
        {
            if (previousDirection != Vector2.zero)
            {
                float angleDiff = Vector2.Angle(previousDirection, currentDirection);

                if (angleDiff > directionChangeThreshold)
                {
                    shakeCount++;
                    Debug.Log("shakeCount" + shakeCount);

                    //�V���J�G�t�F�N�g
                    Vector2 shakaPoint = (Vector2)transform.position + previousDirection * shakaOffset;
                    Instantiate(shakaEffect, shakaPoint, Quaternion.identity);

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
        }

        isCollision = false;

        previousDirection = currentDirection;
    }

    void Move()
    {
        if (isGrabbed)
        {
            targetPosition = player.transform.position;
            position = Vector2.Lerp(position, targetPosition, 30f * Time.deltaTime);

            //��]
            rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 90f), 5f * Time.deltaTime);
            transform.rotation = Quaternion.Euler(rotation);
        }

        if (!isGrabbed && !isGrounded)
        {
            if (velocity.y < gravity.y)
            {
                velocity.y = gravity.y;
            }

            velocity += gravity * Time.deltaTime;
            position += velocity * 45f * Time.deltaTime;

            //��]������
            rotation.z += rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(rotation);
            rotation = transform.rotation.eulerAngles;

            //float angle = Mathf.Atan2(velocity.normalized.y, velocity.normalized.x) * Mathf.Rad2Deg;
            //rotation = Vector3.Lerp(rotation, new Vector3(0, 0, angle + 90), 5f * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(rotation);
            //rotation = transform.rotation.eulerAngles;

            //�����Ŋ����x�����Z
            shakerHeight = position.y + 6f;
            triggerRotation += rotationSpeed * Time.deltaTime;

            if (triggerRotation > 360)
            {
                cocktailProgress += shakerHeight / 5f;
                Debug.Log("�J�N�e���̊����x" + cocktailProgress);
                triggerRotation = 0;
            }
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
                isGrabbed = true;

                //�L���b�`�G�t�F�N�g
                Vector2 catchPoint = (Vector2)transform.position + new Vector2(Random.Range(-100f, 100f) / 100f, Random.Range(-100f, 100f) / 100f) * catchOffset;
                Instantiate(catchEffect, catchPoint, Quaternion.identity);

                //Debug.Log("���񂾂�");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isGrabbed = false;
            //Debug.Log("��������");
        }
    }

    void Pour()
    {

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

            isCollision = true;

            //�S�c�G�t�F�N�g   
            Instantiate(gotuEffect, transform.position + new Vector3(0, gotuOffset, 0), Quaternion.identity);
        }

        if (collision.tag == "Counter")
        {
            velocity = Vector2.Reflect(velocity.normalized, Vector2.up) / 5f;

            isCollision = true;

            //�S�c�G�t�F�N�g
            Instantiate(gotuEffect, transform.position + new Vector3(0, gotuOffset, 0), Quaternion.identity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            isMouse = false;
        }
    }

    void Commands()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            cocktailProgress = 110f;
        }
    }
}
