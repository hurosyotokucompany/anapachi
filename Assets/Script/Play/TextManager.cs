using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class TextManager : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject BGM; 

    // ゲームオーバー
    [SerializeField] GameObject GameOverText;
    [SerializeField] GameObject GameOverPicture;
    [SerializeField] GameObject GameOverSound; 

    // ゲームクリア
    [SerializeField] GameObject RetryButton;
    [SerializeField] GameObject HomeButton;
    [SerializeField] GameObject GameClearText;
    [SerializeField] GameObject GameClearPicture;
    [SerializeField] TextMeshProUGUI timerText; // 経過時間表示用
    [SerializeField] TextMeshProUGUI BestRecordText; // ベストレコード表示用
    

    private GameObject[] BlockObjects;  //GameObjectにBlockObjectsを格納します
    private float timer; // 時間計測用
    private bool isGameOver; // ゲーム終了判定
    
    // Start is called before the first frame update
    void Start()
    {
       BGM.SetActive(true);

       GameOverText.SetActive(false);
       GameOverPicture.SetActive(false);
       GameOverSound.SetActive(false);

       RetryButton.SetActive(false);
       HomeButton.SetActive(false);
       GameClearText.SetActive(false);
       GameClearPicture.SetActive(false);     
    // BestRecordText.SetActive(false);
       timer = 0f; // タイマー初期化
       isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2");
            // Debug.Log(timer);
        }

        if (target.transform.position.y<-15){
            GameOverText.SetActive(true);
            GameOverPicture.SetActive(true);
            RetryButton.SetActive(true);
            HomeButton.SetActive(true);
            GameOverSound.SetActive(true);
            isGameOver = true;
        }
        //消えるオブジェクトにBlockタグをつけます。
        BlockObjects = GameObject.FindGameObjectsWithTag("Block");

        if (BlockObjects.Length == 0)  //Blockタグがついてる残りが0になれば
        {
            RetryButton.SetActive(true);
            HomeButton.SetActive(true);
            GameClearText.SetActive(true);
            GameClearPicture.SetActive(true);
            target.SetActive(false);
            HomeSceneManager.Stagelist[HomeSceneManager.selectedvalue]=SceneManager.GetActiveScene().buildIndex;//
            
            // ベストタイム記録
            string key = "BestTime_" + HomeSceneManager.selectedvalue;
            Debug.Log(key);
            if (!PlayerPrefs.HasKey(key) || PlayerPrefs.GetFloat(key) > timer)
            {
                PlayerPrefs.SetFloat(key, timer);
                // BestRecordText.SetActive(true); // ベストレコード表示
                BestRecordText.text = "Best Record ! " + timer.ToString("F2");
                
            }
            PlayerPrefs.Save();

            isGameOver = true; 
        }
        
    }
}
