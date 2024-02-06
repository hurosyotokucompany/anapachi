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
    private float interval = 5f;

    void Start()
    {
        // Rigidbodyにアクセスして変数に保持しておく
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.useGravity = false;
        myRigidbody.velocity = new Vector3(speed, speed, 0f);
    }
    void FixedUpdate()
    {
        myRigidbody = GetComponent<Rigidbody>();
        Vector3 rvelocity = myRigidbody.velocity;

        if(transform.position.x > -12 )
        {
            rvelocity.y=-1*rvelocity.y;
        }

                // タイマーを更新
        timer += Time.fixedDeltaTime;
        // 5秒ごとにオブジェクトのスケールを3倍にする
        if (timer >= interval)
        {
            // transform.localScale *= 3; // 現在のスケールを3倍にする
            StartCoroutine(EnlargeImage());
            timer = 0f; // タイマーをリセット
        }

    
    }

    private IEnumerator EnlargeImage()
    {
        float duration = 0.5f;
        float currentTime = 0f;
        Vector3 startSize = transform.localScale; // initial scale before enlargement
        Vector3 endSize = startSize * 3; // target scale for full screen

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            transform.localScale = Vector3.Lerp(startSize, endSize, t);
            yield return null;
        }


    }
}