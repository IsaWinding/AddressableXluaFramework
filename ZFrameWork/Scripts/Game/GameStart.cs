using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SRDebug.Init();
        //AttributeSetter.LoadDataFromFile("Assets/_ABs/LocalDontChange/AssetFiles/Attribute1.asset");
        UnityEngine.SceneManagement.SceneManager.LoadScene("1_GameUpdate");
    }
}
