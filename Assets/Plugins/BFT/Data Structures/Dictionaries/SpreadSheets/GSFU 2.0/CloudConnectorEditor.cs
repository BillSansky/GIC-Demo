using System.Collections.Generic;
using UnityEngine.Networking;

namespace BFT
{
    public class CloudConnectorEditor
    {
        // -- Complete the following fields. --
        public static string webServiceUrl =
            "https://script.google.com/macros/s/AKfycbwegx6xSZMm-Av9_2yAd56rjCmuKzo4ny8g54NcP_UlObYuQW3N/exec";

        public static string
            spreadsheetId = ""; // If this is a fixed value could also be setup on the webservice to save POST request size.

        public static string servicePassword = "BillSansky";

        public static void CreateRequest(Dictionary<string, string> form)
        {
#if UNITY_EDITOR
            form.Add("ssid", spreadsheetId);
            form.Add("pass", servicePassword);

            UnityEditor.EditorApplication.update += EditorUpdate;
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

            startTime = UnityEditor.EditorApplication.timeSinceStartup;
            www.SendWebRequest();
#endif
        }

        static void EditorUpdate()
        {
#if UNITY_EDITOR
            while (!www.isDone)
            {
                elapsedTime = UnityEditor.EditorApplication.timeSinceStartup - startTime;
                if (elapsedTime >= timeOutLimit)
                {
                    CloudConnectorCore.ProcessResponse("TIME_OUT", (float) elapsedTime);
                    UnityEditor.EditorApplication.update -= EditorUpdate;
                }

                return;
            }

            if (www.isNetworkError)
            {
                CloudConnectorCore.ProcessResponse(
                    CloudConnectorCore.MSG_CONN_ERR + "Connection error after " + elapsedTime.ToString() + " seconds: " +
                    www.error, (float) elapsedTime);
                return;
            }

            CloudConnectorCore.ProcessResponse(www.downloadHandler.text, (float) elapsedTime);

            UnityEditor.EditorApplication.update -= EditorUpdate;
#endif
        }
#pragma warning disable 414
        private static float timeOutLimit = 30f;

        private static bool usePOST = true;
        // --

        private static UnityWebRequest www;
        private static double elapsedTime = 0.0f;
        private static double startTime = 0.0f;
#pragma warning restore 414
    }
}
