using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ball_insta : MonoBehaviour
{
    Rigidbody myRigidbody;
    float speed = 15f; // 初期のボール速度
    float minSpeed = 1f;
    private BaseSceneManager_insta sceneManager;
    float speedIncrement = 0.5f; // 速度増加量
    int collisionCount = 1; // 衝突回数をカウントする変数
    int collisionsToSpeedUp = 1; // 加速するための衝突回数

    void Start()
    {
        // Rigidbodyにアクセスして変数に保持しておく
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.useGravity = false;
        myRigidbody.velocity = new Vector3(2 * speed, speed, 0f);

        // BaseSceneManager_insta スクリプトへの参照を取得
        sceneManager = FindObjectOfType<BaseSceneManager_insta>();
    }

    void FixedUpdate()
    {
        myRigidbody = GetComponent<Rigidbody>();
        Vector3 rvelocity = myRigidbody.velocity;
        float sqrt2 = Mathf.Sqrt(2);
        // 総速度（magnitude）を計算
        float currentSpeed = rvelocity.magnitude;

        if (Mathf.Abs(currentSpeed - speed * sqrt2) > 0.01f)
        {
            Debug.Log("slowspeed");
            // 総速度が目標値になるように方向を保持しつつ速度を調整
            Vector2 newVelocity = rvelocity.normalized * speed * sqrt2;
            myRigidbody.velocity = newVelocity;
        }

        // x と y の速度が両方とも最小速度より小さい場合、速度を調整
        if (Mathf.Abs(rvelocity.x) < minSpeed)
        {
            Debug.Log(rvelocity.y + "x_0");
            float newx = rvelocity.x + Mathf.Sign(rvelocity.x) * minSpeed / 3;
            float newy = Mathf.Sign(rvelocity.y) * Mathf.Sqrt(2 * speed * speed - newx * newx);

            myRigidbody.velocity = new Vector3(newx, newy, 0f);
        }
        else if (Mathf.Abs(rvelocity.y) < minSpeed)
        {
            Debug.Log(rvelocity.y + "y_0");
            float newy = rvelocity.y + Mathf.Sign(rvelocity.y) * minSpeed;
            float newx = Mathf.Sign(rvelocity.x) * Mathf.Sqrt(2 * speed * speed - newy * newy);
            Debug.Log(newy + "newy");

            myRigidbody.velocity = new Vector3(newx, newy, 0f);
            Debug.Log(myRigidbody.velocity + "velocity");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // ボールがプレイヤーに触れたとき
        if (collision.gameObject.CompareTag("Player"))
        {
            // 衝突回数を増加させる
            collisionCount++;

            // 衝突回数が指定された回数に達したら速度を増加
            if (collisionCount >= collisionsToSpeedUp)
            {
                // ボールの速度を増加させる
                speed += speedIncrement;

                // 現在の進行方向を維持しつつ新しい速度を適用
                Vector3 currentDirection = myRigidbody.velocity.normalized; // 現在の進行方向を取得
                myRigidbody.velocity = currentDirection * speed; // 新しい速度を設定

                collisionCount = 0; // 衝突回数をリセット
            }

            // デフレクションカウントを増加させる
            if (sceneManager != null)
            {
                sceneManager.IncrementDeflectionCount();
            }
        }
    }
}
