using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    public GameObject endGameUI;

    private void Start()
    {
        endGameUI.SetActive(false);
    }

    public void EndGameUIEnable()
    {
        endGameUI.SetActive(true);
    }
}
