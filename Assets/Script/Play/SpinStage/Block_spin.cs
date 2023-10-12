// using System.Threading.Tasks.Dataflow;
using UnityEngine;
using System.Runtime.InteropServices;
using System.ComponentModel;
//using System.Numerics;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;


class Block_spin : MonoBehaviour
{
    private Vector2 basepoint;
    // private int roSpeed = Random.Range(-300, 300);
    void Update(){ 
        float roSpeed = 300f;// 回転速度（度/秒）
        Debug.Log(roSpeed);
        transform.Rotate(Vector3.forward * roSpeed * Time.deltaTime);
    }
    
}