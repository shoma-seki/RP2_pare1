using UnityEngine;

public class Wave : MonoBehaviour {
    public float scrollSpeed = 2f;
    private float spriteWidth;

    void Start() {
        // スプライト幅を取得
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update() {
        // 左にスクロール
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // 左に完全に出たら、右端に移動
        if (transform.position.x <= -spriteWidth) {
            transform.position += new Vector3(spriteWidth * 2f, 0, 0);
        }
    }
}