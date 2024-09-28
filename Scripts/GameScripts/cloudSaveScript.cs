using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine.UI;
using System.Threading.Tasks;

public class cloudSaveScript : MonoBehaviour
{
    private float userScore;

    public bool check = true;

    private bool authenticated = false;

    void Update()
    {
        if (check)
        {
            check = false;
            clearScores();
        }
    }

    public async void Awake()
    {
        await UnityServices.InitializeAsync();
    }

    public async Task SignUpWithUsernamePassword(string username, string password)
    {
        try
        {

            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            authenticated = true;
            Debug.Log("SignUp is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            await SignInWithUsernamePassword(username, password);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    async Task SignInWithUsernamePassword(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            authenticated = true;
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    public async Task SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            authenticated = true;
            Debug.Log("Sign in anonymously succeeded!");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    public bool isAuthenticated()
    {
        return authenticated;
    }

    public async Task SomeAsyncMethod()
    {
        await Task.Delay(100);
    }

    public async Task saveLeaderboardData(float newUserScore, string key)
    {
        var data = new Dictionary<string, object>();
        data[key] = newUserScore;
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
    }

    public async Task saveData(float newUserScore)
    {
        var data = new Dictionary<string, object>();

        for (int i = 1; i <= 3; i++)
        {
            Debug.Log(i+"stScore  1stdebug");
            await loadData(i + "stScore");

            if (newUserScore > userScore)
            {
                // Store the new score in the current position
                Debug.Log(i+"stScore: newuserscore - "+newUserScore);
                data[i + "stScore"] = newUserScore;

                // Update the scores below the current position
                for (int j = i + 1; j <= 3; j++)
                {
                    await loadData(j + "stScore");
                    Debug.Log(j + "stScore: userscore - " + userScore);
                    data[j + "stScore"] = userScore;
                }
                Debug.Log(newUserScore + "newuserscore end");
                // Save the updated leaderboard
                await CloudSaveService.Instance.Data.Player.SaveAsync(data);
                break;
            }
        }
    }

    public async Task<Task> loadData(string inputtedScore)
    {
        Dictionary<string, string> userData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { inputtedScore });

        if (userData.ContainsKey(inputtedScore))
        {
            userScore = float.Parse(userData[inputtedScore]);
        }
        else
        {
            print("Key not found!");
        }
        return Task.CompletedTask;
    }

    public async Task<string> loadSomeData(string inputtedScore)
    {
        Dictionary<string, string> userData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { inputtedScore });
        return userData[inputtedScore];
    }



    public async void clearScores()
    {
        for(int i = 1; i <= 3; i++)
        {
            var data = new Dictionary<string, object> { { i + "stScore", 0 } };
            await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        }
    }
}