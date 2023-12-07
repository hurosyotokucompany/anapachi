// using System.Numerics;
// using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSceneManager : MonoBehaviour
{
    public static List<int> Stagelist { get; private set; } = new List<int>(new int[100]);

    Vector3 startTouchPos;
    Vector3 endTouchPos;

    float flickValue_x;
    float flickValue_y;
    private int StageNumber=1;

    // エラー出さないために残すけど、のちに消します
    public static int selectedvalue=1;


    // void Start()
    // {  
    //     //Playシーンでエラー起こさないために残してるけど、使わないコード
    //     List<string> optionlist = new List<string>();
    //     for (int i = 0; i < Stagelist.Sum()+ 1; i++)
    //     {
    //         optionlist.Add(Convert.ToString(i + 1));
    //     }
    //     //ここまで使わない
    // }

    void Update()
    {
        // if (Input.GetMouseButtonDown(0) == true)
        // {
        //     startTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        // }
        // if (Input.GetMouseButtonUp(0) == true)
        // {
        //     endTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        //     FlickDirection();
        //     GetDirection();
        // }
    }

    // void FlickDirection()
    // {
    //     flickValue_x = endTouchPos.x - startTouchPos.x;
    //     flickValue_y = endTouchPos.y - startTouchPos.y;
    //     Debug.Log("x スワイプ量は" + flickValue_x);
    //     Debug.Log("y スワイプ量は" + flickValue_y);
    // }

    // void GetDirection()
    // {
    //     if (flickValue_x > 500.0f)
    //     {
    //         StageNumber += 1;
    //         if(StageNumber>10){
    //             StageNumber=10;
    //         }
    //     }

    //     if (flickValue_x < -500.0f)
    //     {
    //         StageNumber -= 1;
    //         if(StageNumber<1){
    //             StageNumber=1;
    //         }
    //     }
    // }

    public void OnClickStartButton()
    {
        if (StageNumber==1){
            SceneManager.LoadScene("SpaceStage");
        }
    }

    // public void OnClickRecordButton(){
    //     SceneManager.LoadScene("Record");
    // }
}
