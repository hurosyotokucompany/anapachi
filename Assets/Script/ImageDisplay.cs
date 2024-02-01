using UnityEngine;
using UnityEngine.UI;

public class ImageDisplay : MonoBehaviour
{
    public RawImage rawImage; // UnityのUI RawImageコンポーネントをアタッチ

    public void DisplayImage(Texture2D texture)
    {
        // RawImageのサイズをCanvasに合わせる
        RectTransform rectTransform = rawImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        // RawImageに画像をセットし、中心に配置する
        rawImage.texture = texture;
        rawImage.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rawImage.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rawImage.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rawImage.rectTransform.anchoredPosition = Vector2.zero;
    }

    public void LoadTextureFromFile(string filePath)
    {
        // ファイルパスからTexture2Dを読み込む
        byte[] fileData = System.IO.File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2); // 適切なサイズに変更するか、ファイルからサイズを取得するなどの工夫が必要
        texture.LoadImage(fileData);

        // DisplayImageメソッドを呼び出して画像を表示
        DisplayImage(texture);
    }
}
