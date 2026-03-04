using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputEmail;
    [SerializeField] private TMP_InputField _inputPassword;
    [SerializeField] private TMP_InputField _inputNickname;

    [SerializeField] private Button _buttonRegister;
    [SerializeField] private Button _buttonBack;

    [SerializeField] private AuthMenuUI _authMenuUI;
    [SerializeField] private LogoutMenuUI _logoutMenuUI;

    private void Start()
    {
        _buttonRegister.onClick.AddListener(() =>
        {
            RegisterUser();
        });
        _buttonBack.onClick.AddListener(() =>
        {
            _authMenuUI.Show();
            Hide();
        });

        FirebaseService.Instance.OnUserRegistrationSucceeded += FirebaseService_OnUserRegistrationSucceeded;
    }

    private void FirebaseService_OnUserRegistrationSucceeded(object sender, System.EventArgs e)
    {
        _logoutMenuUI.Show();
        Hide();
    }

    private void RegisterUser()
    {
        FirebaseService.Instance.RegisterUserWithFirestore(_inputEmail.text, _inputPassword.text, _inputNickname.text);
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
        FirebaseService.Instance.OnUserLoginSucceeded -= FirebaseService_OnUserRegistrationSucceeded;
    }
}
