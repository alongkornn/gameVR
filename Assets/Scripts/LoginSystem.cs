using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public GameObject errorMessage;

    public GameObject selectPoint;

    public GameObject playerPoint;

    private string loginURL = "http://localhost:8000/api/auth/login";

    public void OnLoginButtonPressed()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ShowErrorMessage("Please try Email and Password");
            return;
        }

        StartCoroutine(SendLoginRequest(email, password));
    }

    private IEnumerator SendLoginRequest(string email, string password)
    {
        string jsonData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\"}";

        using (UnityWebRequest request = new UnityWebRequest(loginURL, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Login Success: " + request.downloadHandler.text);
                playerPoint.transform.position = selectPoint.transform.position;
                // โหลด scene "Level1"
                // SceneManager.LoadScene("Level1");

                // // ตั้งตำแหน่งหลังจากโหลด scene
                // SceneManager.sceneLoaded += (scene, mode) =>
                // {
                //     if (scene.name == "Level1")
                //     {
                //         GameObject player = GameObject.FindWithTag("Player"); // หรือใช้ชื่อที่เหมาะสม
                //         if (player != null)
                //         {
                //             player.transform.position = new Vector3(0, 1, 0); // เปลี่ยนตำแหน่งตามที่ต้องการ
                //         }
                //     }
                // };
            }
            else
            {
                Debug.LogError("Login Failed: " + request.error);
                ShowErrorMessage("Login Failed, Please try again.");
            }
        }
    }

    private void ShowErrorMessage(string message)
    {
        if (errorMessage != null)
        {
            errorMessage.GetComponent<TMP_Text>().text = message;
            errorMessage.SetActive(true);
        }
    }
}
