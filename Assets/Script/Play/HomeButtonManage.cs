using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonManage : MonoBehaviour
{
   public void OnClickStartButton()
   {
    SceneManager.LoadScene("Home");
   }
}
