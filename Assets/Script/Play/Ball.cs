using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ball : MonoBehaviour
{
    // ボールの移動の速さを指定する変数
    public float speed = 10f * (1.00f * ((float)HomeSceneManager.selectedvalue * 0.5f + 0.5f));
    Rigidbody myRigidbody;

    private AudioSource audioSource;

    private bool hasCollided = false;

    void Start()
    {
        // Rigidbodyにアクセスして変数に保持しておく
        myRigidbody = GetComponent<Rigidbody>();
        // 右斜め45度に進む
        myRigidbody.velocity = new Vector3(speed * 2f, 2f * speed, 0f);

        // オーディオソースを取得
        audioSource = GetComponent<AudioSource>();
    }

     private void OnCollisionEnter(Collision collision)
    {
        // 最初のぶつかり時に音声を再生
        if (!hasCollided)
        {
            audioSource.Play();
            hasCollided = true;
        }
        else if (collision.gameObject.CompareTag("Block"))
        {
            // 他のオブジェクトにぶつかったとき、現在の音声を中断して再生
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.Play();
        }
    }
}

//unchi