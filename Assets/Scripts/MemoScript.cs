using System.Collections;
using UnityEngine;

public class MemoScript : MonoBehaviour {
    [SerializeField] private Player player;

    private Vector2 previousPosition;
    private Vector2 previousDirection;
    private Vector2 shakeStartPoint;

    private bool isGrabbed;
    private bool isMouseOver;
    private bool isDropped;

    [SerializeField] private float directionChangeThreshold = 140f;

    public float cocktailProgress;

    private Rigidbody2D rb;

    private bool isAnimating;

    void Start() {
        previousPosition = transform.position;
        shakeStartPoint = previousPosition;

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        StartCoroutine(AnimateAppear());
    }

    void Update() {
        if (isDropped)
            return;

        if (isMouseOver && Input.GetMouseButtonDown(0)) {
            isGrabbed = true;
        }

        if (Input.GetMouseButtonUp(0)) {
            if (isGrabbed) {
                isGrabbed = false;
                StartDrop();
            }
        }

        if (isGrabbed) {
            transform.position = player.transform.position;
            HandleShake();
        }

        previousPosition = transform.position;
    }

    private void HandleShake() {
        Vector2 currentPosition = transform.position;
        Vector2 moveVector = currentPosition - previousPosition;

        if (moveVector.sqrMagnitude < 0.0001f)
            return;

        Vector2 currentDirection = moveVector.normalized;

        if (previousDirection != Vector2.zero) {
            float angleDiff = Vector2.Angle(previousDirection, currentDirection);

            if (angleDiff > directionChangeThreshold) {
                cocktailProgress += 1f;
                shakeStartPoint = currentPosition;
            }
        }

        previousDirection = currentDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Mouse")) {
            isMouseOver = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Mouse")) {
            isMouseOver = false;
        }
    }

    private void StartDrop() {
        isDropped = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;

        Destroy(gameObject, 3f);
    }

    private IEnumerator AnimateAppear() {
        isAnimating = true;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up * 7f; // è„Ç…7ÉÜÉjÉbÉg

        float duration = 2f;
        float timer = 0f;

        while (timer < duration) {
            timer += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, timer / duration);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        transform.position = endPos;

        isAnimating = false;
    }
}
