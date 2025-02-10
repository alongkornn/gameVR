using UnityEngine;
using System.Net;

public class AllowInsecureHTTP : MonoBehaviour
{
    // ตั้งค่าให้ Unity ยอมรับการเชื่อมต่อ HTTP
    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {
        // ปิดการตรวจสอบความปลอดภัยของการเชื่อมต่อ HTTP
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        ServicePointManager.ServerCertificateValidationCallback =
            (sender, certificate, chain, sslPolicyErrors) => true;
    }
}
