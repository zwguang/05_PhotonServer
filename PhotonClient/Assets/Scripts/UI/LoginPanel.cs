using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour {

    private GameObject m_loginPanel;
    private GameObject m_registerPanel;
    public InputField m_userNameIF;
    public InputField m_passwordIF;
    public Text m_hitMessage;

    private LoginRequest m_loginRequest;

    private void Awake()
    {

        m_loginPanel = transform.gameObject;
        m_registerPanel = GameObject.FindGameObjectWithTag("RegisterPanel");
        m_loginRequest = GetComponent<LoginRequest>();
    }

    private void Start()
    {

        m_registerPanel.gameObject.SetActive(false);
    }

    public void OnLoginButton()
    {
        m_hitMessage.text = "";

        m_loginRequest.m_userName = m_userNameIF.text;
        m_loginRequest.m_passWord = m_passwordIF.text;
        m_loginRequest.DefaultRequest();
    }

    public void OnRegisterButton()
    {
        m_loginPanel.gameObject.SetActive(false);
        m_registerPanel.gameObject.SetActive(true);
    }

    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == (short)ReturnCode.Success)
        {
            SceneManager.LoadScene("GameMain");
        }
        else
        {
            Debug.Log("登录失败");
            m_hitMessage.text = "用户名或者密码错误";
        }
    }
}
