using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputEmail;
    [SerializeField] private TMP_InputField _inputPassword;
    [SerializeField] private TMP_InputField _inputConfirmPassword;
    [SerializeField] private TMP_InputField _inputNickname;

    [SerializeField] private Button _buttonRegister;
    [SerializeField] private Button _buttonIHaveAccount;

    [SerializeField] private LoginMenuUI _loginMenuUI;
    private void Start()
    {
        _buttonRegister.onClick.AddListener(() =>
        {
            RegisterUser();
        });
        _buttonIHaveAccount.onClick.AddListener(() =>
        {
            _loginMenuUI.Show();
            Hide();
        });

        FirebaseService.Instance.OnUserRegistrationSucceeded += FirebaseService_OnUserRegistrationSucceeded;
        FirebaseService.Instance.OnFirebaseLoaded += FirebaseServise_OnFirebaseLoaded;
    }

    private void FirebaseServise_OnFirebaseLoaded(object sender, System.EventArgs e)
    {
        if (FirebaseService.Instance.IsLogged())
            SceneManager.LoadScene("Scene_Game");
    }

    private void FirebaseService_OnUserRegistrationSucceeded(object sender, System.EventArgs e)
    {
        SceneManager.LoadScene("Scene_Game");
    }

    private void RegisterUser()
    {
        if(_inputPassword.text == _inputConfirmPassword.text)
        {
            FirebaseService.Instance.RegisterUserWithFirestore(_inputEmail.text, _inputPassword.text, _inputNickname.text);
        }
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
