using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ball : MonoBehaviour
{
    // ボールの移動の速さを指定する変数
    // public float speed = 30f * (1.00f * ((float)HomeSceneManager.selectedvalue * 0.5f + 0.5f));
    Rigidbody myRigidbody;

    void Start()
    {
        float speed = 20f;
        // Rigidbodyにアクセスして変数に保持しておく
        myRigidbody = GetComponent<Rigidbody>();
        // 右斜め45度に進む
        float rad = Random.Range(Mathf.PI*1/3f, Mathf.PI*2/3f);
        Debug.Log(Mathf.Cos(rad));
        Debug.Log(Mathf.Sin(rad));
        myRigidbody.velocity = new Vector3(speed* Mathf.Cos(rad), speed* Mathf.Sin(rad), 0f);
    }

}
//unchi