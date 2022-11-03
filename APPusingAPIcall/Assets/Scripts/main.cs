using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using System.Text;
using System.IO;
public class main : MonoBehaviour
{   public TextMeshProUGUI output;
    string input;
    string url;
    public class response
    {
        public string years;
        public string predictedsal;
    }  
    public void ReadInput(string str)
    {
        input = str;
        UnityEngine.Debug.Log(input);
        url = "https://yujalshrestha.pythonanywhere.com/?input="+input;
    }

    public void predict(){
        StartCoroutine(Apicall(url));
    }
    IEnumerator Apicall (string url)
    {
        using(UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            switch(webRequest.result){

            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
            Debug.Log("Smthh wrong");
            break;
            case UnityWebRequest.Result.Success:
            response r= JsonConvert.DeserializeObject<response>(webRequest.downloadHandler.text);
            output.text="The predicted salary for " + input +" years of experience is Rs. " + r.predictedsal;
            break;
            }


        }
    }
}
