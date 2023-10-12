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
        float sqrt2 = Mathf.Sqrt(2);
        // 総速度（magnitude）を計算
        float currentSpeed = rvelocity.magnitude;

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
            Debug.Log(newx+"newx");
            myRigidbody.velocity = new Vector3(newx, newy, 0f);
            Debug.Log(myRigidbody.velocity+"velocity");
        }

        if(transform.position.x > -12 )
        {
            rvelocity.y=-1*rvelocity.y;
        }

    
    }
}