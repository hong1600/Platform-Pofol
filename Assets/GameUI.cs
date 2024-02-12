using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{

    public bool ispause = false;

    public GameObject menu;

    private void Awake()
    {
        menu.SetActive(false);
    }

    public void onclickMenu()
    {
        ispause = true;

        if (ispause == true)
        {
            Time.timeScale = 0f;
            menu.SetActive(true);
        }

    }

    public void onclickContinue()
    {
        ispause = false;

        if (ispause == false)
        {

            Time.timeScale = 1f;
            menu.SetActive(false);
        }
    }

    public void onclickMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
