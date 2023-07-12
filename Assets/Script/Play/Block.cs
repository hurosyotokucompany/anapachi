using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Block : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody myRigidbody;

    // 何かとぶつかった時に呼ばれるビルトインメソッド
    void OnCollisionEnter(Collision collision)
    {
        // Rigidbodyにアクセスして変数に保持しておく
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = new Vector3(0f ,-speed, speed*100);
        // ゲームオブジェクトを削除するメソッド
        Destroy(gameObject);
    }
}
