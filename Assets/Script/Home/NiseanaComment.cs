using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class NiseanaComment : MonoBehaviour
{
    public GameObject Niseana_Comment = null; // Textオブジェクト
    public int maxnum=27;

    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(1, maxnum+1);
        TextMeshProUGUI Comment_text = Niseana_Comment.GetComponent<TextMeshProUGUI> ();
        if(r==1){
            Comment_text.text="Don't get lost! Anasan is with you.";
        }else if(r==2){
            Comment_text.text="Anasan always interests you.";
        }else if(r==3){
            Comment_text.text="This game is most fantastic for you.";
        }else if(r==4){
            Comment_text.text="Anasan is always in space.";
        }else if(r==5){
            Comment_text.text="Anasan must take you dream world.";
        }else if(r==6){
            Comment_text.text="You may see Anasan someday.";
        }else if(r==7){
            Comment_text.text="Time is money";
        }else if(r==8){
            Comment_text.text="Anasan melts your hurt.";
        }else if(r==9){
            Comment_text.text="Let's sing a song.";
        }else if(r==10){
            Comment_text.text="Anamoon is Anasun's brother.";
        }else if(r==11){
            Comment_text.text="Playing this game makes you smarter.";
        }else if(r==12){
            Comment_text.text="Hurry up and press the start button.";
        }else if(r==13){
            Comment_text.text="I can't count how many people Anasan saved.";
        }else if(r==14){
            Comment_text.text="If you have any confusion in your life, you should search Anasan.";
        }else if(r==15){
            Comment_text.text="Don't you forget something? You shoud ask Anasan.";
        }else if(r==16){
            Comment_text.text="My name is Niseana. I like baseball";
        }else if(r==17){
            Comment_text.text="Anasan was born in Meiji era.";
        }else if(r==18){
            Comment_text.text="Anasan loves Numazu.";
        }else if(r==19){
            Comment_text.text="Anasan runs fast.";
        }else if(r==20){
            Comment_text.text="Anasan serched Dottinoyu.";
        }else if(r==21){
            Comment_text.text="Anasan has many friends.";
        }else if(r==22){
            Comment_text.text="Anasan goes to Sendai by using Seishun18kippu.";
        }else if(r==23){
            Comment_text.text="Anasan made thunder on his high school class.";
        }else if(r==24){
            Comment_text.text="When you feel danger, please shout 'Anasan'.";
        }else if(r==25){
            Comment_text.text="Anasan is master of space.";
        }else if(r==26){
            Comment_text.text="Anasan is master of space.";
        }else if(r==27){
            Comment_text.text=$"There are 40 kind of Anasan info. Let's play a lot.";
        }

        UnityEngine.Debug.Log(r);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
