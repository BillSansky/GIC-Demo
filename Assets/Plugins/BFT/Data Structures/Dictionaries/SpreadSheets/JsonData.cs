using System;
using System.Collections.Generic;
using System.Text;

namespace BFT
{
    public class JsonData
    {
        public Dictionary<string, string> DataByID = new Dictionary<string, string>();
        public int ID = -1;

        public bool IsValid => ID != -1;

        public string this[string key]
        {
            get => DataByID[key];
            set => DataByID[key] = value;
        }

        public void AddData(string name, string value)
        {
            if (!DataByID.ContainsKey(name))
                DataByID.Add(name, value);
        }


        public void PopulateData(string jSon)
        {
#if UNITY_EDITOR
            DataByID = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jSon);

            if (!DataByID.ContainsKey("ID"))
                ID = -1;
            else
            {
                if (!int.TryParse(DataByID["ID"], out ID))
                    ID = -1;
                DataByID.Remove("ID");
            }

#else
        throw new PlatformNotSupportedException();
#endif
        }

        public string ExportJson()
        {
            StringBuilder json = new StringBuilder();

            json.Append("{");

            json.Append("\"");
            json.Append("ID");
            json.Append("\"");

            json.Append(":");

            json.Append("\"");
            json.Append(ID);
            json.Append("\"");

            foreach (var data in DataByID)
            {
                json.Append(",");

                json.Append("\"");
                json.Append(data.Key);
                json.Append("\"");

                json.Append(":");

                json.Append("\"");
                if (data.Value == null)
                    json.Append("");
                else
                    json.Append(data.Value.Replace("\"", "\\\"")).Replace("\\\\\"", "\\\"");
                json.Append("\"");
            }

            json.Append("}");

            return json.ToString();
        }

        public int CountOfDataColumnsWithTerm(string term, bool countEmptyEntries = false)
        {
            int count = 0;
            foreach (var pair in DataByID)
            {
                if (pair.Key.Contains(term) && (countEmptyEntries || !pair.Value.IsNullOrEmpty()))
                    count++;
            }

            return count;
        }

        public IEnumerable<string> DataForColumnsWithTerm(string term, bool enumerateEmptyEntries = false)
        {
            foreach (var pair in DataByID)
            {
                if (pair.Key.Contains(term) && (enumerateEmptyEntries || !pair.Value.IsNullOrEmpty()))
                    yield return pair.Value;
            }
        }

        /// <summary>
        ///     Use this when you need to retrieve few columns that have the same name with a number at the end.
        /// </summary>
        /// <param name="arrayName"></param>
        /// <param name="enumerateEmptyEntries"></param>
        /// <returns></returns>
        public IEnumerable<string> DataForColumnsArray(string arrayName, bool enumerateEmptyEntries = false)
        {
            for (int i = 0; i < DataByID.Count; i++)
            {
                string id = arrayName + " " + i;
                if (DataByID.ContainsKey(id))
                {
                    if (!DataByID[id].IsNullOrEmpty() || enumerateEmptyEntries)
                    {
                        yield return DataByID[id];
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public IEnumerable<string[]> DataForColumnsMultiArray(bool enumerateEmptyEntries = false,
            params string[] arrayNames)
        {
            string[] values = new string[arrayNames.Length];

            for (int i = 0; i < DataByID.Count; i++)
            {
                Array.Clear(values, 0, values.Length);
                bool invalidData = false;
                for (int j = 0; j < values.Length; j++)
                {
                    string id = arrayNames[j] + " " + i;
                    if (DataByID.ContainsKey(id))
                    {
                        if (!DataByID[id].IsNullOrEmpty() || enumerateEmptyEntries)
                        {
                            values[j] = DataByID[id];
                        }
                        else
                        {
                            invalidData = true;
                            break;
                        }
                    }
                    else
                    {
                        invalidData = true;
                        break;
                    }
                }

                if (!invalidData)
                    yield return values;
            }
        }

        public string[] GetFieldNames()
        {
            List<string> fields = new List<string>(DataByID.Count + 1);
            fields.Add("ID");
            fields.AddRange(DataByID.Keys);
            return fields.ToArray();
        }
    }
}
