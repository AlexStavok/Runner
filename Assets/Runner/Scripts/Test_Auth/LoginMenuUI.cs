using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputEmail;
    [SerializeField] private TMP_InputField _inputPassword;

    [SerializeField] private Button _buttonLogin;
    [SerializeField] private Button _buttonDontHaveAccount;

    [SerializeField] private RegisterMenuUI _registerMenuUI;

    private void Start()
    {
        _buttonLogin.onClick.AddListener(() =>
        {
            LoginUser();
        });
        _buttonDontHaveAccount.onClick.AddListener(() =>
        {
            _registerMenuUI.Show();
            Hide();
        });

        FirebaseService.Instance.OnUserLoginSucceeded += FirebaseService_OnUserLoginSucceeded;
    }

    private void FirebaseService_OnUserLoginSucceeded(object sender, System.EventArgs e)
    {
        SceneManager.LoadScene("Scene_Game");
        Hide();
    }

    private void LoginUser()
    {
        FirebaseService.Instance.LoginUser(_inputEmail.text, _inputPassword.text);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        FirebaseService.Instance.OnUserLoginSucceeded -= FirebaseService_OnUserLoginSucceeded;
    }
}
