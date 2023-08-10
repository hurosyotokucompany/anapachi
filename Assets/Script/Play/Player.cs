// using System.Threading.Tasks.Dataflow;
using UnityEngine;
using System.Runtime.InteropServices;
using System.ComponentModel;
//using System.Numerics;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;


class Player : MonoBehaviour
{
    private Vector2 basepoint;
    void Update(){ 
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
                transform.position += new Vector3(deltaPosition.x/100,0, 0);
                basepoint = Input.touches[0].position;
            }
        }
    }
    

//以下はボタン処理（消す予定）
    public void rotButtonDown(){
        transform.Rotate(0,0,45f);
    }

}