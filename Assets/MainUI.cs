using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainUI : MonoBehaviour
{


    public void onclickStart()
    {
        SceneManager.LoadScene(1);
        Debug.Log("시작");
    }


    public void onclickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
        Debug.Log("종료");
#endif
    }
}
