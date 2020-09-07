using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class GetAudio : MonoBehaviour
{
    public InputField Field;
    private bool wasFocused;

    private void Update()
    {
        if (wasFocused && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(Field.text);
            StartCoroutine(Upload(Field.text));
        }
    
        wasFocused = Field.isFocused;
    }
    IEnumerator Upload(string musicName)
    {
        string m_path = Application.dataPath;
        WWWForm form = new WWWForm();
        form.AddField("music_name", musicName);

        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:5000/api/v1/get_music", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            byte[] music = www.downloadHandler.data;
            File.WriteAllBytes("Assets/Audios/" + musicName + ".mp3", music);
            Globals globals = new Globals();
            globals.musicName = musicName;
        }
    }
}
