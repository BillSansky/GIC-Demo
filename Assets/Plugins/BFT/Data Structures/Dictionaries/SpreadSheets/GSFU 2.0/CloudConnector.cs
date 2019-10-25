using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace BFT
{
    public class CloudConnector : MonoBehaviour
    {
        // --

        private static CloudConnector _Instance;
        private string servicePassword = "passcode";

        private string
            spreadsheetId = ""; // If this is a fixed value could also be setup on the webservice to save POST request size.

        private float timeOutLimit = 30f;

        public bool usePOST = true;

        // -- Complete the following fields. --
        private string webServiceUrl = "";

        private UnityWebRequest www;

        public static CloudConnector Instance =>
            _Instance ?? (_Instance = new GameObject("CloudConnector").AddComponent<CloudConnector>());

        public void CreateRequest(Dictionary<string, string> form)
        {
            form.Add("ssid", spreadsheetId);
            form.Add("pass", servicePassword);

            if (usePOST)
            {
                CloudConnectorCore.UpdateStatus("Establishing Connection at URL " + webServiceUrl);
                www = UnityWebRequest.Post(webServiceUrl, form);
            }
            else // Use GET.
            {
                string urlParams = "?";
                foreach (KeyValuePair<string, string> item in form)
                {
                    urlParams += item.Key + "=" + item.Value + "&";
                }

                CloudConnectorCore.UpdateStatus("Establishing Connection at URL " + webServiceUrl + urlParams);
                www = UnityWebRequest.Get(webServiceUrl + urlParams);
            }

            StartCoroutine(ExecuteRequest(form));
        }

        IEnumerator ExecuteRequest(Dictionary<string, string> postData)
        {
            www.SendWebRequest();

            float elapsedTime = 0.0f;

            while (!www.isDone)
            {
                elapsedTime += UnityEngine.Time.deltaTime;
                if (elapsedTime >= timeOutLimit)
                {
                    CloudConnectorCore.ProcessResponse("TIME_OUT", elapsedTime);
                    break;
                }

                yield return null;
            }

            if (www.isNetworkError)
            {
                CloudConnectorCore.ProcessResponse(
                    CloudConnectorCore.MSG_CONN_ERR + "Connection error after " + elapsedTime.ToString() + " seconds: " +
                    www.error, elapsedTime);
                yield break;
            }

            CloudConnectorCore.ProcessResponse(www.downloadHandler.text, elapsedTime);
        }
    }
}
