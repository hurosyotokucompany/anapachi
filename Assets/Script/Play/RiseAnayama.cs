using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseAnayama : MonoBehaviour
{
    //===== 定義領域 =====
    private Animator anim;  //Animatorをanimという変数で定義する
    //bool anarise = false;

    //===== 初期処理 =====
    void Start()
    {
        //変数animに、Animatorコンポーネントを設定する
        anim = gameObject.GetComponent<Animator>();
    }

    //===== 主処理 =====
    void Update()
    {
        //スペースキーが押された場合→スマホ用に変更
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("riseAnaBotton", true);
            Debug.Log("space");
        }
    }
    public void ResetIsSpacePressed()
    {
        // Set the "IsSpacePressed" parameter back to false
        anim.SetBool("riseAnaBotton", false);
    }
}