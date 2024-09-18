using UnityEngine;
using UnityEngine.SceneManagement; // シーン遷移に必要な名前空間をインポート

public class SceneChanger : MonoBehaviour
{
    // 遷移先のシーン名を設定するための変数
    [SerializeField] private string sceneName;

    // オブジェクトがクリックされたときに呼び出されるメソッド
    void OnMouseDown()
    {
        // シーンをロードする
        SceneManager.LoadScene(sceneName);
    }
}