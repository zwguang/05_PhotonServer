using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : MonoBehaviour {

    private GameObject m_loginPanel;
    private GameObject m_registerPanel;
    public InputField m_userNameIF;
    public InputField m_passwordIF;
    public Text m_hitMessage;

    private RegisterRequest m_registerRequest;

    private void Awake()
    {
        m_loginPanel = GameObject.FindGameObjectWithTag("LoginPanel");
        m_registerPanel = transform.gameObject;

        m_registerRequest = GetComponent<RegisterRequest>();
    }

    public void OnReturnButton()
    {
        m_loginPanel.gameObject.SetActive(true);
        m_registerPanel.gameObject.SetActive(false);
    }

    public void OnConfirmButton()
    {
        m_hitMessage.text = "";
        m_registerRequest.m_userName = m_userNameIF.text;
        m_registerRequest.m_passWord = m_passwordIF.text;
        m_registerRequest.DefaultRequest();
    }

    public void OnConfirmResponse(ReturnCode returnCode)
    {
        if (returnCode == (short)ReturnCode.Success)
        {
            m_hitMessage.text = "注册成功，返回登录";
        }
        else
        {
            m_hitMessage.text = "用户名重复, 请重新输入";
            m_userNameIF.text = "";
        }
    }
}
