using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginScreen : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;

    public GameObject canvas;
    public GameObject loginMenu;

    private cloudSaveScript saver;

    // Start is called before the first frame update
    void Start()
    {
        GameObject camera = GameObject.Find("Main Camera");
        saver = camera.GetComponent<cloudSaveScript>();
    }

    public async void SignIn()
    {
        await saver.SignUpWithUsernamePassword(username.text, password.text);
        switchMenu();
    }
    
    public async void GuestSignIn()
    {
        await saver.SignInAnonymouslyAsync();
        switchMenu();
    }

    private void switchMenu()
    {
        if (saver.isAuthenticated())
        {
            canvas.SetActive(true);
            loginMenu.SetActive(false);
        }
    }
}
