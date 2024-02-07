// using System.Threading.Tasks.Dataflow;
using UnityEngine;
using System.Runtime.InteropServices;
using System.ComponentModel;
// using System.Numerics;
// using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;


class Player : MonoBehaviour
{
    private Vector2 basepoint;
    private float minX=-10f; // 最小X座標（左壁）
    private float maxX=10f; // 最大X座標（右壁）

    //float smoothTime = 0.0001f;  // 補間にかかる時間
    Vector3 velocity = Vector3.zero;  // 補間の速度（内部的に使用）
    
    void FixedUpdate(){ 
        //以下はタッチしてpaddleを動かすためのコード
        
       if (Input.touchCount > 0)
        {
            //最初にタッチした時の位置情報取得
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                basepoint = Input.touches[0].position;
            }
            //指の初期位置からx方向に動かした分だけ、paddleがx方向に動く
            if (Input.touches[0].phase == TouchPhase.Moved)
            {   
                Vector2 currentPosition = Input.touches[0].position;
                Vector2 deltaPosition = currentPosition - basepoint;
                // 新しい位置を計算
                float newX = transform.position.x + deltaPosition.x / 100;
                // 新しい位置がゲームエリア内であるか確認し、必要であれば調整
                newX = Mathf.Clamp(newX, minX, maxX);
                // 更新された位置を設定
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                // Vector3 targetPosition = new Vector3(newX, transform.position.y, transform.position.z);
                // transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

                basepoint = Input.touches[0].position;
            }
        }
    }
}