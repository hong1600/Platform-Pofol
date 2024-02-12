using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    public string type;
    public float cooltime;
    public float currenttime;
    public Image icon;

    private void Awake()
    {
        icon = GetComponent<Image>();
    }

}
