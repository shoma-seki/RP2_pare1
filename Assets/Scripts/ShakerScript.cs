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
    public Vector2 velocity;
    [SerializeField] Vector2 gravity;

    Vector3 rotation;

    bool isMouse;
    public bool isGrabbed;
    public bool isGrounded = true;
    bool isCollision;

    public bool isClear;

    public bool isCompleted;

    bool isPlusRotate;    //triggerRotation�𑫂���

    //�X�^�[�g�ɖ߂�
    Vector2 startPosition;

    //���ˌW��
    [SerializeField] float reflection;

    //�������
    [SerializeField] float throwPower;

    //���g
    float shakeSpeed;       //�U�����X�s�[�h
    float shakeDistance;    //�U��������
    float shakeCount;       //�U������
    public float cocktailAmount;
    [SerializeField] float kCocktailAmount;   //�J�N�e���̗�
    [SerializeField] float cocktailAmountMinus; //�J�N�e���̗ʂ���炷�W��
    Vector2 preShakePoint;  //�O��V�F�C�N�����ꏊ
    public float cocktailProgress; //�J�N�e���̊����x   
    public float cocktailProgressMax;
    float shakerHeight;     //�V�F�C�J�[�̍���

    //�����Ă�Ƃ��ɉ��Z
    [SerializeField] float rotationSpeed;
    public float triggerRotation;

    //�U��Ƃ��̂��
    Vector2 direction;      //�i��ł������
    Vector2 previousDirection;
    float directionChangeThreshold = 140f; // �p�x����45�x�ȏ�Łu�}�ρv�Ɣ��f

    //����
    [SerializeField] Vector2 pourOffset;
    Vector2 pourCenter;
    public bool isPour;

    //Reset�p
    public float pourTime;
    [SerializeField] float kPourTime;

    //�G�t�F�N�g
    [SerializeField] GameObject catchEffect;    //�L���b�`������
    [SerializeField] float catchOffset;

    [SerializeField] GameObject shakaEffect;    //�U�����Ƃ�
    [SerializeField] float shakaOffset;

    [SerializeField] GameObject gotuEffect;     //�Ԃ������Ƃ�
    [SerializeField] float gotuOffset;

    [SerializeField] GameObject spillParticle;  //�Ԃ������Ƃ��̃p�[�e�B�N��


    [SerializeField] GameObject pourParticle;  //�����Ƃ��̃p�[�e�B�N��

    [SerializeField] GameObject trickEffect;
    [SerializeField] GameObject trickParticle;
    float trickEffectRotate;

    //��
    [SerializeField] GameObject shakaSound;     //�U�����Ƃ��̉�
    [SerializeField] GameObject catchSound;     //�L���b�`�����Ƃ��̉�
    [SerializeField] GameObject trickCatchSound;     //�g���b�N����߂ăL���b�`�����Ƃ��̉�
    [SerializeField] GameObject gotuSound;      //�Ԃ������Ƃ��̉�

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        position = transform.position;

        //�X�^�[�g�ɖ߂��|�W�V����
        startPosition = transform.position;
        targetPosition = startPosition;

        pourTime = kPourTime;

        cocktailAmount = kCocktailAmount;
    }

    private void FixedUpdate()
    {
        prePosition = transform.position;

        if (!isGrounded && !isPour)
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
        ResetToStart();
        Clear();

        if (Input.GetMouseButtonUp(0))
        {
            velocity = preVelocity * throwPower;
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

        Debug.Log("�J�N�e���̗�" + cocktailAmount);

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
                    //Debug.Log("shakeCount" + shakeCount);

                    //�V���J�G�t�F�N�g
                    Vector2 shakaPoint = (Vector2)transform.position + previousDirection * shakaOffset;
                    Instantiate(shakaEffect, shakaPoint, Quaternion.identity);
                    Instantiate(shakaSound);

                    if (shakeCount >= 2)
                    {
                        shakeDistance = Vector2.Distance(preShakePoint, transform.position);

                        //�J�N�e���̊����x��ύX
                        cocktailProgress += shakeDistance * shakeSpeed / 10f;
                        //Debug.Log("�J�N�e���̊����x" + cocktailProgress);
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

            //80�ȏ�Ŕ��]
            if (position.y >= 80)
            {
                velocity.y = 0;
                position.y = 80;
            }

            //��]������
            rotation.z += rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(rotation);
            rotation = transform.rotation.eulerAngles;

            //float angle = Mathf.Atan2(velocity.normalized.y, velocity.normalized.x) * Mathf.Rad2Deg;
            //rotation = Vector3.Lerp(rotation, new Vector3(0, 0, angle + 90), 5f * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(rotation);
            //rotation = transform.rotation.eulerAngles;

            //�����Ŋ����x����Z

            if (velocity.y > 0)
            {
                shakerHeight = position.y + 6f;
            }

            if (isPlusRotate)
            {
                triggerRotation += rotationSpeed * Time.deltaTime;

                trickEffectRotate += rotationSpeed * Time.deltaTime;
                //�G�t�F�N�g
                if (trickEffectRotate > 360)
                {
                    trickEffectRotate = 0;
                    //Instantiate(trickEffect, transform.position, Quaternion.identity);
                    Instantiate(trickParticle, transform.position, Quaternion.identity);
                }
            }

            //if (triggerRotation > 360)
            //{
            //    cocktailProgress += shakerHeight / 5f;
            //    //Debug.Log("�J�N�e���̊����x" + cocktailProgress);
            //    triggerRotation = 0;
            //}
        }

        transform.position = position;
    }

    void Grab()
    {
        if (isMouse && !isPour)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isGrounded = false;
                isGrabbed = true;

                //�L���b�`�G�t�F�N�g
                Vector2 catchPoint = (Vector2)transform.position + new Vector2(Random.Range(-100f, 100f) / 100f, Random.Range(-100f, 100f) / 100f) * catchOffset;
                Instantiate(catchEffect, catchPoint, Quaternion.identity);
                Instantiate(catchSound);

                //Debug.Log("���񂾂�");

                //�����ăL���b�`�̂Ƃ�
                cocktailProgress += (triggerRotation / 1000f) * (shakerHeight / 5f);
                isPlusRotate = true;
                if (triggerRotation > 2160)
                {
                    Instantiate(trickCatchSound);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isGrabbed = false;

            shakerHeight = 0;
            triggerRotation = 0;
            //Debug.Log("��������");
        }
    }

    void Pour()
    {
        if (isPour)
        {
            targetPosition = pourCenter + pourOffset;
            position = Vector2.Lerp(position, targetPosition, 1f * Time.deltaTime);
            transform.position = position;

            //��]
            rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 100f), 1f * Time.deltaTime);
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    void ResetToStart()
    {
        if (isPour)
        {
            pourTime -= Time.deltaTime;
            if (pourTime < 0) {
                isPour = false;
                targetPosition = startPosition;
                pourTime = kPourTime;
                isGrounded = true;

                GlassScript glass = FindAnyObjectByType<GlassScript>();
                if (glass.isGrounded) {
                    glass.isFull = true;
                }
            }
        }

        if (isGrounded)
        {
            position = Vector2.Lerp(position, targetPosition, 30f * Time.deltaTime);
            transform.position = position;

            //��]
            rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 0), 5f * Time.deltaTime);
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    void Clear()
    {
        if (isClear)
        {
            isClear = false;
            isCompleted = false;

            cocktailAmount = kCocktailAmount;
            cocktailProgress = 0;
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
            triggerRotation = 0;
            isPlusRotate = false;

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
            if (!isGrounded)
            {
                Instantiate(gotuEffect, transform.position + new Vector3(0, gotuOffset, 0), Quaternion.identity);
                Instantiate(gotuSound);

                Instantiate(spillParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 1080f))));
            }

            //�ʂ���炷
            cocktailAmount -= cocktailAmountMinus;
            if (cocktailAmount < 0)
            {
                cocktailAmount = 0;
            }
        }

        if (collision.tag == "Counter")
        {
            velocity = Vector2.Reflect(velocity.normalized, Vector2.up) / 5f;

            isCollision = true;

            triggerRotation = 0;

            //�S�c�G�t�F�N�g
            if (!isGrounded)
            {
                Instantiate(gotuEffect, transform.position + new Vector3(0, gotuOffset, 0), Quaternion.identity);
                Instantiate(gotuSound);

                Instantiate(spillParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 1080f))));
            }

            //�ʂ���炷
            cocktailAmount -= cocktailAmountMinus;
            if (cocktailAmount < 0)
            {
                cocktailAmount = 0;
            }
        }

        if (collision.tag == "PourField")
        {
            isPour = true;
            pourCenter = collision.transform.position;
            Instantiate(pourParticle, new Vector3(transform.position.x, transform.position.y, 10f), pourParticle.transform.rotation);
        }

        if (collision.tag == "Ojama")
        {
            triggerRotation = 0;
            isPlusRotate = false;

            //�ʂ���炷
            cocktailAmount -= 10f;
            if (cocktailAmount < 0)
            {
                cocktailAmount = 0;
            }

            Instantiate(spillParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 1080f))));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Counter")
        {
            velocity.y = 0.15f;

            isCollision = true;

            triggerRotation = 0;
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
