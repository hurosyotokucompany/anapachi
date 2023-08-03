using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSceneManager : MonoBehaviour
{
    [SerializeField] Dropdown ddtmp;
    public static List<int> Stagelist { get; private set; } = new List<int>(new int[100]);
    public static int selectedvalue;

    private string FirstStageName="初級";
    private string SecondStageName="中級";
    private string ThirdStageName="上級";

    void Start()
    {  
        //Playシーンでエラー起こさないために残してるけど、使わないコード
        List<string> optionlist = new List<string>();
        for (int i = 0; i < Stagelist.Sum()+ 1; i++)
        {
            optionlist.Add(Convert.ToString(i + 1));
        }
        //ここまで使わない

        //Stage名が入ったリストを作成。ドロップダウン(ddtmp)に入れる
        List<string> StageList=new List<string>();
        StageList.Add(FirstStageName);
        StageList.Add(SecondStageName);
        StageList.Add(ThirdStageName);

        ddtmp.AddOptions(StageList);
    }

    public void OnClickStartButton()
    {
        //ddtmpに表示されたステージ名に応じて、移動するシーンを変更。
        string selectedstr = ddtmp.options[ddtmp.value].text;
        if (selectedstr==FirstStageName){
            SceneManager.LoadScene("Play");
        }else if(selectedstr==SecondStageName){
            SceneManager.LoadScene("Play2");
        }else{
            SceneManager.LoadScene("Play3");
        }
    }

    public void OnClickRecordButton(){
        SceneManager.LoadScene("Record");
    }
}
