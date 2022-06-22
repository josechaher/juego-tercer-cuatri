using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Events : MonoBehaviour
{
    public void BeholderDestroyed() {
        StartCoroutine(ChangeScene.DelaySceneChange("Congrats", 3));
    }
}
