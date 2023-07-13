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

    //afjlkjflkaj
    //slkjflsjflj

    void Start()
    {
    // for (int i = 0; i < Stagelist.Count; i++)
    //     {
    //         Stagelist[i] = 0;
    //     }    

        List<string> optionlist = new List<string>();
        for (int i = 0; i < Stagelist.Sum()+ 1; i++)
        {
            optionlist.Add(Convert.ToString(i + 1));
        }

        ddtmp.AddOptions(optionlist);
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Play");
        string selectedstr = ddtmp.options[ddtmp.value].text;
        selectedvalue=int.Parse(selectedstr);
        Debug.Log(Stagelist.Sum());
    }
}
