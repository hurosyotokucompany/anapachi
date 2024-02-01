using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public Image imageComponent; // UnityのUI Imageコンポーネントをアタッチ
    public int threshold_1 = 45; // 画像を切り替える閾値
    public int threshold_2= 90;
    public int threshold_3=99;
    public string ImagePath;

    void Start()
    {
        int rnd = Random.Range(1, 101);

        if(rnd<=threshold_1)
        {
            ImagePath="Assets/Textures/over1";
        }else if(rnd<=threshold_2){
            ImagePath="Assets/Textures/over4";
        }else if(rnd<=threshold_3){
            ImagePath="Assets/Textures/over3";
        }else{
            ImagePath="Assets/Textures/over2";
        }

        // Resources.Loadを使用して画像をロード
        Sprite loadedSprite = Resources.Load<Sprite>(ImagePath);

        // Imageコンポーネントにロードしたスプライトをセット
        if (loadedSprite != null)
        {
            imageComponent.sprite = loadedSprite;
        }
        else
        {
            Debug.LogError("Image not found at path: " + ImagePath);
        }
    }
}
