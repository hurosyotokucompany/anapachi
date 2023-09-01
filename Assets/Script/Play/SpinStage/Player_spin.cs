// using System.Threading.Tasks.Dataflow;
using UnityEngine;
using System.Runtime.InteropServices;
using System.ComponentModel;
//using System.Numerics;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;


class Player_spin : MonoBehaviour
{
    private Vector2 basepoint;
    
    void Update(){ 
        float roSpeed = 150f;// 回転速度（度/秒）
        transform.Rotate(Vector3.forward * roSpeed * Time.deltaTime);
    }
    
}