﻿using System;
using System.IO;
using System.Net;
using System.Text;

namespace Client
{
    public class RestClient
    {
        const string AZURE_ADDRESS = "http://texasholdem2017.azurewebsites.net/api/";
        private static string _endPoint = AZURE_ADDRESS;

        private static void WriteData(HttpWebRequest request, string data)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            request.ContentType = "application/json";
            request.ContentLength = buffer.Length;

            var reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();
        }   

        public static string MakePostRequest(string data)
        {
            string strResponseValue = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_endPoint);
            request.Method = "POST";
            WriteData(request, data);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("Error code: " + response.StatusCode.ToString());
                }
                //Process the response stream... (JSON/XML/HTML...)
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }//End of Stream Reader
                    }
                }//End of using ResponseStream

            }//End of using ResponseStream
            _endPoint = AZURE_ADDRESS;
            return strResponseValue;
        }

        public static string MakeGetRequest()
        {
            string strResponseValue = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_endPoint);
            request.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("Error code: " + response.StatusCode.ToString());
                }
                //Process the response stream... (JSON/XML/HTML...)
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }//End of Stream Reader
                    }
                }//End of using ResponseStream

            }//End of using ResponseStream
            _endPoint = AZURE_ADDRESS;
            return strResponseValue;
        }

        public static void SetController(string suffix)
        {
            _endPoint = _endPoint + suffix;
        }

    }
}