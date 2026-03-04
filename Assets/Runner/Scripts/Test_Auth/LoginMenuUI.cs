using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputEmail;
    [SerializeField] private TMP_InputField _inputPassword;

    [SerializeField] private Button _buttonLogin;
    [SerializeField] private Button _buttonBack;

    [SerializeField] private AuthMenuUI _authMenuUI;
    [SerializeField] private LogoutMenuUI _logoutMenuUI;

    private void Start()
    {
        _buttonLogin.onClick.AddListener(() =>
        {
            LoginUser();
        });
        _buttonBack.onClick.AddListener(() =>
        {
            _authMenuUI.Show();
            Hide();
        });

        FirebaseService.Instance.OnUserLoginSucceeded += FirebaseService_OnUserLoginSucceeded;
    }

    private void FirebaseService_OnUserLoginSucceeded(object sender, System.EventArgs e)
    {
        _logoutMenuUI.Show();
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
