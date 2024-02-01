using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class NengaBall : MonoBehaviour
{
    // ボールの移動の速さを指定する変数
    // public float speed = 30f * (1.00f * ((float)HomeSceneManager.selectedvalue * 0.5f + 0.5f));
    Rigidbody myRigidbody;
    float speed = 15f;
    float minSpeed = 1f;
    

    void Start()
    {
        // Rigidbodyにアクセスして変数に保持しておく
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.useGravity = false;
        // 右斜め45度に進む
        float rad = Random.Range(Mathf.PI*1/3f, Mathf.PI*2/3f);
        Debug.Log(Mathf.Cos(rad));
        Debug.Log(Mathf.Sin(rad));
        myRigidbody.velocity = new Vector3(speed* Mathf.Cos(rad), speed* Mathf.Sin(rad), 0f);
        // myRigidbody.velocity = new Vector3(2*speed, speed, 0f);
    }
    void FixedUpdate()
    {
        myRigidbody = GetComponent<Rigidbody>();
        Vector3 rvelocity = myRigidbody.velocity;
        float sqrt2 = Mathf.Sqrt(2);
        // 総速度（magnitude）を計算
        float currentSpeed = rvelocity.magnitude;

        // 総速度が目標速度より小さい場合は調整
        // Debug.Log(rvelocity+" v");
        // Debug.Log(currentSpeed+" c");
        // Debug.Log(rvelocity.z+" z");
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

        

    
    }
}