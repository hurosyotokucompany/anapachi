using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Player : MonoBehaviour
{

    // private Vector2 touchStartPos;   // タッチが開始した位置
    // private Vector2 paddleStartPos;  // 棒の初期位置
    // public float paddleSpeed = 1.0f; // 棒の移動速度

    // void Start()
    // {
    //     paddleStartPos = transform.position;
    // }

    // void Update()
    // {
    //     if (Input.touchCount > 0)
    //     {
    //         Touch touch = Input.GetTouch(0); // 最初のタッチのみを処理する

    //         if (touch.phase == TouchPhase.Began)
    //         {
    //             touchStartPos = touch.position;
    //         }
    //         else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
    //         {
    //             // タッチの移動量に応じて棒の位置を更新する
    //             float touchDelta = (touch.position.x - touchStartPos.x) / Screen.width;
    //             Vector2 paddlePos = paddleStartPos + new Vector2(touchDelta * paddleSpeed, 0f);
    //             transform.position = paddlePos;
    //         }
    //     }
    // }

    public void leftButtonDown(){
        transform.position += new Vector3(-2,0,0);
    }
    
    public void rightButtonDown(){
        transform.position += new Vector3(2,0,0);
    }
    public void rotButtonDown(){
        transform.Rotate(0,0,45f);
    }

}