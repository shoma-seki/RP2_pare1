using UnityEngine;

public class MemoScript : MonoBehaviour {
    [SerializeField] private Player player;

    private Vector2 previousPosition;
    private Vector2 previousDirection;
    private Vector2 shakeStartPoint;

    private bool isGrabbed;
    private bool isMouseOver;

    [SerializeField] private float directionChangeThreshold = 140f;

    public float cocktailProgress;

    private float returnSpeed = 5f; // 中央へ戻るスピード

    void Start() {
        previousPosition = transform.position;
        shakeStartPoint = previousPosition;
    }

    void Update() {
        if (isMouseOver && Input.GetMouseButtonDown(0)) {
            isGrabbed = true;
        }

        if (Input.GetMouseButtonUp(0)) {
            isGrabbed = false;
        }

        if (isGrabbed) {
            transform.position = player.transform.position;
            HandleShake();
        } else {
            
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
                float shakeDistance = Vector2.Distance(shakeStartPoint, currentPosition);
                float shakeSpeed = moveVector.magnitude;

                float progress = 1f;
                cocktailProgress += progress;

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
}
