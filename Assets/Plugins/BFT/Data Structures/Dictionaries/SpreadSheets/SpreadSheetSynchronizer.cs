using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public interface ISpreadSheetParser
    {
        void ParseTableData(string jsonTableData, bool createMissingData = false, bool deleteMissingData = false);
        void AfterParsePass();
        string GetTableDataAsJson();
        Dictionary<int, JsonData> GetTableDataSeparatedByRow();
    }

    public class SpreadSheetSynchronizer : SerializedScriptableObject
    {
        [BoxGroup("Spreadsheets")] public List<string> CustomTableOrder = new List<string>();

        private Dictionary<string, Dictionary<int, JsonData>> dataPerSheet =
            new Dictionary<string, Dictionary<int, JsonData>>();

        [BoxGroup("Options")] public bool DateRelatedTableCreation = true;

        [BoxGroup("Tools")] public bool DebugLog;

        public UnityEvent OnParsingStarted;

        [BoxGroup("Tools")] public float SecondsBetweenEachPackages = .5f;

        [BoxGroup("Options")] public string spreadsheetID;

        [BoxGroup("Spreadsheets")]
        public Dictionary<string, ISpreadSheetParser> SpreadSheetParsersByName =
            new Dictionary<string, ISpreadSheetParser>();

        [BoxGroup("Status")] public bool ReceivingData { get; private set; }

        [BoxGroup("Status")] private bool SendingData { get; set; }

        public void InitConnection()
        {
            CloudConnectorCore.debugMode = DebugLog;

#if UNITY_EDITOR
            CloudConnectorEditor.spreadsheetId = spreadsheetID;
#else
        throw new PlatformNotSupportedException();
#endif
        }


        [InfoBox("Receiving Data, Please Wait!", InfoMessageType.Info, "ReceivingData")]
        [Button(ButtonSizes.Medium)]
        public void GetSpreadsheetData()
        {
            InitConnection();

            CloudConnectorCore.GetAllTables(false);
            CloudConnectorCore.processedResponseCallback.AddListener(OnDataReceived);
            ReceivingData = true;
        }

        [InfoBox("Sending Data! Please wait", InfoMessageType.Info, "SendingData")]
        [Button(ButtonSizes.Medium), HideIf("SendingData")]
        public void RecreateAllTables()
        {
            InitConnection();
            RecreateTables();
        }

        private void RecreateTables()
        {
            dataPerSheet = new Dictionary<string, Dictionary<int, JsonData>>();

            foreach (var parser in SpreadSheetParsersByName)
            {
                Dictionary<int, JsonData> datas = parser.Value.GetTableDataSeparatedByRow();
                dataPerSheet.Add(parser.Key, datas);
            }

            SendingData = true;
            EditorCoroutines.StartCoroutine(RecreateTable(), this);
        }

        private IEnumerator RecreateTable()
        {
            foreach (var parser in dataPerSheet)
            {
                Dictionary<int, JsonData> datas = parser.Value;

                if (datas.Count == 0)
                    continue;

                string tableName = DateRelatedTableCreation
                    ? parser.Key + " " + DateTime.Today.ToShortDateString()
                    : parser.Key;

                CloudConnectorCore.CreateTable(datas.Values.ElementAt(0).GetFieldNames(), tableName, false);
                yield return new WaitForSeconds(SecondsBetweenEachPackages * 2);

                foreach (var jsonData in datas)
                {
                    if (DebugLog)
                        UnityEngine.Debug.Log("Sending Object JSon: " + jsonData.Value.ExportJson());

                    CloudConnectorCore.CreateObject(jsonData.Value.ExportJson(), tableName, false);
                    yield return new WaitForSeconds(SecondsBetweenEachPackages);
                }
            }

            SendingData = false;
        }

        private void OnDataReceived(CloudConnectorCore.QueryType queryType, List<string> objTypeNames,
            List<string> jsonData)
        {
            ReceivingData = true;

            try
            {
                if (queryType == CloudConnectorCore.QueryType.getAllTables)
                {
                    OnParsingStarted.Invoke();
                    List<int> parsedIds = new List<int>();
                    if (CustomTableOrder.Count > 0)
                    {
                        //first parse data in forced order
                        foreach (var t in CustomTableOrder)
                        {
                            int index = objTypeNames.IndexOf(t);
                            ParseJson(objTypeNames[index], jsonData[index]);
                            parsedIds.Add(index);
                        }
                    }

                    for (int i = 0; i < jsonData.Count; i++)
                    {
                        if (parsedIds.Contains(i))
                            continue;
                        ParseJson(objTypeNames[i], jsonData[i]);
                    }

                    foreach (var parsedData in SpreadSheetParsersByName)
                    {
                        parsedData.Value.AfterParsePass();
                    }
                }
            }
            catch (System.Exception)
            {
                CloudConnectorCore.processedResponseCallback.RemoveListener(OnDataReceived);
                ReceivingData = false;
                throw;
            }

            CloudConnectorCore.processedResponseCallback.RemoveListener(OnDataReceived);
            ReceivingData = false;
        }

        private void ParseJson(string objTypeName, string jsonData)
        {
            if (DebugLog)
            {
                UnityEngine.Debug.Log("Data received:");
                UnityEngine.Debug.Log(jsonData);
            }

            if (!SpreadSheetParsersByName.ContainsKey(objTypeName))
            {
                return;
            }

            SpreadSheetParsersByName[objTypeName].ParseTableData(jsonData, true);
        }
    }

// Helper class: because UnityEngine.JsonUtility does not support deserializing an array...
// http://forum.unity3d.com/threads/how-to-load-an-array-with-jsonutility.375735/
    public class JsonHelper
    {
        public static T[] JsonArray<T>(string json)
        {
            string newJson = "{ \"array\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] array = new T[] { };
        }
    }
}
