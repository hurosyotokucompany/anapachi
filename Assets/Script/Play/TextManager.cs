using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TextManager : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject GameOverText;
    [SerializeField] GameObject RetryButton;
    [SerializeField] GameObject GameClearText;

    private GameObject[] BlockObjects;  //GameObjectにBlockObjectsを格納します
    
    // Start is called before the first frame update
    void Start()
    {
       GameOverText.SetActive(false);
       RetryButton.SetActive(false);
       GameClearText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (target.transform.position.y<-10){
            GameOverText.SetActive(true);
            RetryButton.SetActive(true);
        }
        //消えるオブジェクトにBlockタグをつけます。
        BlockObjects = GameObject.FindGameObjectsWithTag("Block");

        if (BlockObjects.Length == 0)  //Blockタグがついてる残りが0になれば
        {
            RetryButton.SetActive(true);
            GameClearText.SetActive(true);
            target.SetActive(false);
            HomeSceneManager.Stagelist[HomeSceneManager.selectedvalue]=1;
            Debug.Log(HomeSceneManager.Stagelist.Sum());
        }
        
    }
}
