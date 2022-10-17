using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour {

    public GameObject username;
    public GameObject txt;
    public GameObject butto;
    private string Name;

    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSd21ma5X1uCgD3ddd6p_ZngeU-uUNBOqj3pBSZ2RTuqdoimcQ/formResponse";

    IEnumerator Post(string name) {
        WWWForm form = new WWWForm();
        form.AddField("entry.705493010", name);

       // byte[] rawData = form.data;
       // WWW www = new WWW(BASE_URL, rawData);
       // yield return www;

        UnityWebRequest www = UnityWebRequest.Post(BASE_URL, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
    public void Send() {
        Name = username.GetComponent<InputField>().text;
        StartCoroutine(Post(Name));
        txt.SetActive(false);
        butto.SetActive(false);

    }
}