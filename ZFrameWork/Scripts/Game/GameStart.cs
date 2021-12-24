using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SRDebug.Init();
        UnityEngine.SceneManagement.SceneManager.LoadScene("1_GameUpdate");
    }
}
