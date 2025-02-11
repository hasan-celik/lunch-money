using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using Firebase.Extensions;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Google;


public class FirebaseController : MonoBehaviour
{
    #region GoogleSingIn

    private string api = "111832701440-t9qkvp4j46o83gei4rhsvfmf745jt0ja.apps.googleusercontent.com";
    GoogleSignInConfiguration config;

    private FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    private void Awake()
    {
        config = new GoogleSignInConfiguration
        {
            WebClientId = api,
            RequestIdToken = true
        };
    }

    private void CheckFirebase() 
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            DependencyStatus status = task.Result;
            if (status == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                user = auth.CurrentUser;
            }
        });
    }

    public void SingInWithGoogle() 
    {
        GoogleSignIn.Configuration = config;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(FinishSingIn);
        OpenProfilePanel();
    }

    private void FinishSingIn(Task<GoogleSignInUser> task) 
    {
        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.Log("Kayit Hatasi");
            return;
        }

        Credential credential = GoogleAuthProvider.GetCredential(task.Result.IdToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log("Credential Hatasi");
                return;
            }
            user = task.Result;
        });
    }

    public void SingOut() 
    {
        if (user != null)
        {
            auth.SignOut();
            user = null;
        }
    }

    #endregion

    #region verificationEmail
    private void SendVerificationEmail(FirebaseUser user)
    {
        user.SendEmailVerificationAsync().ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.Log( "Doğrulama e-postası gönderilemedi.");
            }
            else
            {
                Debug.Log("Doğrulama e-postası gönderildi. Lütfen e-postanızı kontrol edin.");
            }
        });
    }

    public void OtoVerification() 
    {
        SendVerificationEmail(user);
    }

    #endregion

    #region Panels

    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        signUpPanel.SetActive(false);
        profilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
    }
    
    public void OpenSignupPanel()
    {
        loginPanel.SetActive(false);
        signUpPanel.SetActive(true);
        profilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
    }

    public void OpenProfilePanel()
    {
        loginPanel.SetActive(false);
        signUpPanel.SetActive(false);
        profilePanel.SetActive(true);
        forgetPasswordPanel.SetActive(false);
    }

    public void OpenForgetPasswordPanel()
    {
        loginPanel.SetActive(false);
        signUpPanel.SetActive(false);
        profilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(true);
    }
    
    private void ShowNotificationMessage(string title, string message)
    {
        errorTitle_Text.text = "" + title;
        errorMessage_Text.text = "" + message;
        notificationPanel.SetActive(true);
    }

    public void CloseNotification()
    {
        errorTitle_Text.text = "";
        errorMessage_Text.text = "";
        notificationPanel.SetActive(false);
    }

    public void LogOut()
    {
        auth.SignOut();
        profileUserName_Text.text = "";
        profileUserEmail_Text.text = "";
        OpenLoginPanel();
    }
    #endregion

    public GameObject loginPanel, signUpPanel, profilePanel, forgetPasswordPanel, notificationPanel;
    public TMP_InputField loginEmail, loginPassword, signUpEmail,signUpPassword, signUpCPassword, signUpUserName,forgetPasswordEmail;
    public TMP_Text errorTitle_Text, errorMessage_Text, profileUserName_Text, profileUserEmail_Text;
    public Toggle rememberMe_toggle;
    //public Button StartButton;
    
    //Firebase.Auth.FirebaseAuth auth;
    //Firebase.Auth.FirebaseUser user;

    private bool isSignIn = false;

    private void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                InitializeFirebase();
                
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            } else {
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    public void LoginUser()
    {
        if (string.IsNullOrEmpty(loginEmail.text) && string.IsNullOrEmpty(loginPassword.text))
        {
            ShowNotificationMessage("Error", "Fields are Must be Filled");
            return;
        }
        SignInUser(loginEmail.text, loginPassword.text);
    }
    
    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(signUpEmail.text)
            && string.IsNullOrEmpty(signUpPassword.text)
            && string.IsNullOrEmpty(signUpCPassword.text)
            && string.IsNullOrEmpty(signUpUserName.text))
        {
            ShowNotificationMessage("Error", "Fields can't be Empty");
            return;
        }
        CreateUser(signUpEmail.text, signUpPassword.text, signUpUserName.text);
    }
    
    public void forgetPasswordUser()
    {
        if (string.IsNullOrEmpty(forgetPasswordEmail.text))
        {
            ShowNotificationMessage("Error", "Forget Email Empty");
            return;
        }
    }

    public void CreateUser(string email, string password, string userName)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
            
            UpdateUserProfile(userName);
        });
    }

    public void SignInUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled) {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            profileUserName_Text.text = "" + result.User.DisplayName;
            profileUserEmail_Text.text = "" + result.User.Email;
            OpenProfilePanel();
        });

    }
    
    void InitializeFirebase() {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs) {
        if (auth.CurrentUser != user) {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
                                                     && auth.CurrentUser.IsValid();
            if (!signedIn && user != null) {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn) {
                Debug.Log("Signed in " + user.UserId);
                isSignIn = true;
            }
        }
    }

    void OnDestroy() {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    void UpdateUserProfile(string userName)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null) {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile {
                DisplayName = userName,
                PhotoUrl = new System.Uri("https://fastly.picsum.photos/id/663/200/200.jpg?hmac=MzrpRFuFESictjAXjWDdc1tKVJQkRXcUjAqfOHgUiww"),
            };
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled) {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted) {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User profile updated successfully.");
                
                ShowNotificationMessage("Alert", "Account Succesfully Created");
            });
        }
    }

    private bool isSigned = false;

    public void LoadLobbyScene()
    {
        SceneManager.LoadScene("Loading");
    }
}