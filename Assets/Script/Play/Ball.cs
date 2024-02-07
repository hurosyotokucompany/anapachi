using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ball : MonoBehaviour
{
    // ボールの移動の速さを指定する変数
    // public float speed = 30f * (1.00f * ((float)HomeSceneManager.selectedvalue * 0.5f + 0.5f));
    Rigidbody myRigidbody;
    float speed = 10f;
    float minSpeed = 1f;

    private Vector3 lastPosition;
    private float positionCheckDelay = 5.0f; // 位置をチェックする間隔（秒）
    private float moveThreshold = 0.1f; // 位置が「変わらない」と判断する閾値
    private float additionalSpeed = 2.0f; // 追加する速度の大きさ

    void Start()
    {
        // Rigidbodyにアクセスして変数に保持しておく
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.useGravity = false;
        lastPosition = transform.position;
        StartCoroutine(CheckPositionRoutine());

        // 右斜め45度に進む
        // float rad = Random.Range(Mathf.PI*1/3f, Mathf.PI*2/3f);
        // Debug.Log(Mathf.Cos(rad));
        // Debug.Log(Mathf.Sin(rad));
        // myRigidbody.velocity = new Vector3(speed* Mathf.Cos(rad), speed* Mathf.Sin(rad), 0f);
        myRigidbody.velocity = new Vector3(2*speed, speed, 0f);
    }
    void FixedUpdate()
    {
        // myRigidbody = GetComponent<Rigidbody>();
        Vector3 rvelocity = myRigidbody.velocity;
        float sqrt2 = Mathf.Sqrt(2);
        // 総速度（magnitude）を計算
        float currentSpeed = rvelocity.magnitude;

        // 総速度が目標速度より小さい場合は調整
        if (Mathf.Abs(currentSpeed - speed * sqrt2) > 0.01f)
        {
            Debug.Log("slowspeed");
            // 総速度が目標値になるように方向を保持しつつ速度を調整
            Vector3 newVelocity = rvelocity.normalized * speed * sqrt2;
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
    
    }

    private IEnumerator CheckPositionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(positionCheckDelay);

            if (Vector3.Distance(transform.position, lastPosition) < moveThreshold)
            {
                ApplyAdditionalSpeed();
            }

            lastPosition = transform.position;
        }
    }

    private void ApplyAdditionalSpeed()
    {
        Vector3 direction = myRigidbody.velocity.normalized;
        if (direction.magnitude == 0) // 速度が0の場合はランダムな方向を選択
        {
            direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        }
        myRigidbody.velocity += direction * additionalSpeed;
    }


}