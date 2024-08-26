using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class NiseanaComment : MonoBehaviour
{
    public GameObject Niseana_Comment = null; // Textオブジェクト
    public int maxnum=32;

    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(1, maxnum+1);
        TextMeshProUGUI Comment_text = Niseana_Comment.GetComponent<TextMeshProUGUI> ();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
