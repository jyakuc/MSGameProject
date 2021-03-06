﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour {
    [SerializeField]
    private Image m_mask;
    [SerializeField]
    private Text m_pause;
    [SerializeField]
    private Text m_pause1;


    // Update is called once per frame
    void Update () {
        if (PauseManager.IsPause)
        {
            m_mask.gameObject.SetActive(true);
            m_pause.gameObject.SetActive(true);
            m_pause1.gameObject.SetActive(true);
        }
        else
        {
            m_mask.gameObject.SetActive(false);
            m_pause.gameObject.SetActive(false);
            m_pause1.gameObject.SetActive(false);
        }
	}
}
