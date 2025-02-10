using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class LoginController : MonoBehaviour
{
    private string apiURL = "http://localhost:8000/api/auth/login";
    public TMP_InputField emailField, passwordField;
    public TextMeshProUGUI statusText;

    public void Login()
    {
        // เรียกใช้ coroutine เพื่อทำการ login
        StartCoroutine(LoginCoroutine(emailField.text, passwordField.text));
    }

    IEnumerator LoginCoroutine(string email, string password)
    {
        // สร้างข้อมูล JSON สำหรับการส่งไปยัง API
        string jsonData = JsonUtility.ToJson(new LoginData(email, password));

        // สร้าง UnityWebRequest สำหรับการส่งข้อมูล
        UnityWebRequest request = new UnityWebRequest(apiURL, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // ส่งคำขอไปยัง API
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            statusText.text = "Login Successful"; // แสดงข้อความเมื่อสำเร็จ
            Debug.Log("Response: " + request.downloadHandler.text);

            // แปลงข้อมูลที่ได้จาก API เป็น LoginResponse
            LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
            PlayerPrefs.SetString("auth_token", response.token); // บันทึก token ไว้ใน PlayerPrefs
        }
        else
        {
            statusText.text = "Login Failed: " + request.error; // แสดงข้อความเมื่อเกิดข้อผิดพลาด
            Debug.Log("Error: " + request.error);
        }
    }

    // ข้อมูลที่ส่งไปยัง API สำหรับ Login
    [System.Serializable]
    public class LoginData
    {
        public string email;
        public string password;

        public LoginData(string emailData, string passwordData)
        {
            email = emailData;
            password = passwordData;
        }
    }

    // ข้อมูลที่ได้รับจาก API หลังจากทำการ login
    [System.Serializable]
    public class LoginResponse
    {
        public string status;
        public string token;
    }
}
