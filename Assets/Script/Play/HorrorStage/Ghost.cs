using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ghost : MonoBehaviour
{
    // ボールの移動の速さを指定する変数
    // public float speed = 30f * (1.00f * ((float)HomeSceneManager.selectedvalue * 0.5f + 0.5f));
    Rigidbody myRigidbody;
    float speed = 2f;
    float minSpeed = 0.1f;
    private float timer = 0f;
    // 拡大する間隔（秒）
    private float interval = 10f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Rigidbodyにアクセスして変数に保持しておく
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.useGravity = false;
        myRigidbody.velocity = new Vector3(speed, speed, 0f);
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRendererコンポーネントを取得
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
    }
    void FixedUpdate()
    {
        myRigidbody = GetComponent<Rigidbody>();
        Vector3 rvelocity = myRigidbody.velocity;
        float sqrt2 = Mathf.Sqrt(2);
        // 総速度（magnitude）を計算
        float currentSpeed = rvelocity.magnitude;

        // 総速度が目標速度より小さい場合は調整
        if (Mathf.Abs(currentSpeed - speed * sqrt2) > 0.01f)
        {
            Debug.Log("slowspeed");
            // 総速度が目標値になるように方向を保持しつつ速度を調整
            Vector2 newVelocity = rvelocity.normalized * speed * sqrt2;
            myRigidbody.velocity = newVelocity;
        }
        // x と y の速度が両方とも最小速度より小さい場合、速度を調整
        if (Mathf.Abs(rvelocity.x) < minSpeed )
        {
            Debug.Log(rvelocity.y+"x_0");
            float newx = rvelocity.x + Mathf.Sign(rvelocity.x)*minSpeed/3;
            float newy = Mathf.Sign(rvelocity.y)*Mathf.Sqrt(2*speed*speed-newx*newx);

            myRigidbody.velocity = new Vector3(newx, newy, 0f);

        }
        else if (Mathf.Abs(rvelocity.y) < minSpeed )
        {
            Debug.Log(rvelocity.y+"y_0");
            float newy = rvelocity.y + Mathf.Sign(rvelocity.y)*minSpeed;
            float newx = Mathf.Sign(rvelocity.x)*Mathf.Sqrt(2*speed*speed-newy*newy);
            Debug.Log(newy+"newy");

            myRigidbody.velocity = new Vector3(newx, newy, 0f);
            Debug.Log(myRigidbody.velocity+"velocity");
        }


        timer += Time.fixedDeltaTime;
        // 5秒ごとにオブジェクトのスケールを3倍にする
        if (timer >= interval)
        {
            // transform.localScale *= 3; // 現在のスケールを3倍にする
            // StartCoroutine(EnlargeImage());
            StartCoroutine(ChangeScale(5, 3));
            timer = 0f; // タイマーをリセット
        }
    }

    private IEnumerator EnlargeImage()
    {
        float duration = 0.5f;
        float currentTime = 0f;
        Vector3 startSize = transform.localScale; // initial scale before enlargement
        Vector3 endSize = startSize * 5; // target scale for full screen

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            transform.localScale = Vector3.Lerp(startSize, endSize, t);
            yield return null;
        }
    }


    private IEnumerator ChangeScale(float targetScaleMultiplier, float duration)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * targetScaleMultiplier;
        
        Color originalColor = spriteRenderer.color;
        Color startColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1);

        // Vector3 startPosition = new Vector3(transform.position.x, transform.position.y, -2);
        // transform.position = startPosition;

        // 拡大プロセス
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t / duration);
            spriteRenderer.color = Color.Lerp(originalColor, targetColor, t / duration);
            yield return null;
        }
        transform.localScale = targetScale; // 确保达到目标尺寸
        yield return new WaitForSeconds(3);

        // 縮小プロセス (元のサイズに戻す)
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, t / duration);
            spriteRenderer.color = Color.Lerp(targetColor, originalColor, t / duration);
            yield return null;
        }

        transform.localScale = originalScale; // 确保恢复到原始尺寸
        spriteRenderer.color=originalColor;

        // Vector3 originalPosition = new Vector3(transform.position.x, transform.position.y, 0);
        // transform.position = originalPosition;
    }


}