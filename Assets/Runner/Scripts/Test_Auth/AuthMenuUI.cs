using UnityEngine;
using UnityEngine.UI;

public class AuthMenuUI : MonoBehaviour
{
    [SerializeField] private Button _buttonRegister;
    [SerializeField] private Button _buttonLogin;

    [SerializeField] private RegistrationMenuUI _registrationMenuUI;
    [SerializeField] private LoginMenuUI _loginMenuUI;
    [SerializeField] private LogoutMenuUI _logoutMenuUI;
    private void Start()
    {
        _buttonRegister.onClick.AddListener(() =>
        {
            _registrationMenuUI.Show();
            Hide();
        });
        _buttonLogin.onClick.AddListener(() =>
        {
            _loginMenuUI.Show();
            Hide();
        });

        FirebaseService.Instance.OnFirebaseLoaded += FirebaseService_OnFirebaseLoaded;

        Hide();
    }

    private void FirebaseService_OnFirebaseLoaded(object sender, System.EventArgs e)
    {
        if (FirebaseService.Instance.IsLogged())
        {
            _logoutMenuUI.Show();
            Hide();
        }
        else
            Show();
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
        FirebaseService.Instance.OnFirebaseLoaded -= FirebaseService_OnFirebaseLoaded;
    }
}
