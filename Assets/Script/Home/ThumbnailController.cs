using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ThumbnailController : MonoBehaviour
{
    public GameObject unlock_second;
    public GameObject unlock_third;
    public GameObject unlock_forth;
    public GameObject unlock_fifth;
    public GameObject unlock_sixth;
    public GameObject unlock_seventh;
    public GameObject unlock_eighth;
    

    private void Update()
    {
       if (PlayerPrefs.HasKey("BestTime_BaseStage")){
            Destroy(unlock_second);
       }
       if (PlayerPrefs.HasKey("BestTime_SpaceStage")){
            Destroy(unlock_third);
       }
       if (PlayerPrefs.HasKey("BestTime_ThunderStage")){
            Destroy(unlock_forth);
       }
       if (PlayerPrefs.HasKey("BestTime_DoubleStage")){
            Destroy(unlock_fifth);
       }
       if (PlayerPrefs.HasKey("BestTime_SpinStage")){
            Destroy(unlock_sixth);
       }
       if (PlayerPrefs.HasKey("BestTime_HorrorStage")){
            Destroy(unlock_seventh);
       }

    }
}

