using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerDisconnect : MonoBehaviour {
    private Image m_mask;
    [SerializeField]
    private WarningPlayerUI m_warningImage;           // 「〇Pのコントロールが抜けました」
    private Image m_connectReadyImage;                       // 「接続できました」
   
    private enum EControllerConnected
    {
        Disconnect,
        Connect,
        ReConnect
    }
    private EControllerConnected m_eControllerConnected;

    private void Start()
    {
        m_mask = transform.GetChild(0).GetComponent<Image>();
        m_connectReadyImage = transform.GetChild(1).GetComponent<Image>();
        m_warningImage = transform.GetChild(2).GetComponent<WarningPlayerUI>();

        // 非表示
        m_mask.gameObject.SetActive(false);
        m_connectReadyImage.gameObject.SetActive(false);
        m_warningImage.gameObject.SetActive(false);
    }

    // コントローラーが抜けたとき表示
    public void OnDisconnected(List<int> playerID)
    {
        if (m_eControllerConnected == EControllerConnected.Disconnect) return;
        m_eControllerConnected = EControllerConnected.Disconnect;
        m_mask.gameObject.SetActive(true);
        m_warningImage.gameObject.SetActive(true);
        m_warningImage.Display(playerID);
    }

    // 接続が確認されて準備中の時表示
    public void OnConnectedReady()
    {
        if (m_eControllerConnected != EControllerConnected.Disconnect) return;
        m_eControllerConnected = EControllerConnected.ReConnect;
        m_connectReadyImage.gameObject.SetActive(true);
    }

    // 準備中が確認できたとき処理
    public void OnConnectComplete()
    {
        if (m_eControllerConnected != EControllerConnected.ReConnect) return;
        m_eControllerConnected = EControllerConnected.Connect;
        // 非表示
        m_mask.gameObject.SetActive(false);
        m_warningImage.gameObject.SetActive(false);
        m_connectReadyImage.gameObject.SetActive(false);
    }
}
