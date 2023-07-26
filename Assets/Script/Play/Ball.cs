using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ball : MonoBehaviour
{
    // ボールの移動の速さを指定する変数
    public float speed = 10f * (1.00f * ((float)HomeSceneManager.selectedvalue * 0.5f + 0.5f));
    Rigidbody myRigidbody;

    void Start()
    {
        // Rigidbodyにアクセスして変数に保持しておく
        myRigidbody = GetComponent<Rigidbody>();
        // 右斜め45度に進む
        myRigidbody.velocity = new Vector3(speed * 2f, 2f * speed, 0f);
    }
}//
