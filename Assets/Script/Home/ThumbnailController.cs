using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ThumbnailController : MonoBehaviour
{

    private void Update()
    {
        //ここからはobjectの大きさ変換の話
        // オブジェクトの位置を取得
        Vector3 objectPosition = transform.position;
        // 位置に基づいてサイズを計算
        float newSize = 1-(0.2f*Math.Abs(objectPosition.x));
        // オブジェクトのスケールを変更
        if (newSize>0){
        transform.localScale = new Vector3(1.0f*newSize,1.0f*newSize, 1);
        }else{
            transform.localScale=new Vector3(0,0,0);
        }
    }
}

