using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Blocks : MonoBehaviour
{
    public GameObject effectPrefab;  // エフェクトのプレハブを指定します。
    Rigidbody myRigidbody;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);

            // ブロックの位置にエフェクトを生成します。
            var effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);

            // エフェクトのアニメーションを開始します。
            // var animator = effect.GetComponent<Animator>();
            // if (animator != null)
            // {
            //     animator.Play("EffectAnimation");  // "EffectAnimation"はあなたが作成したアニメーションクリップの名前に置き換えてください。
            // }
        }
    }
}
