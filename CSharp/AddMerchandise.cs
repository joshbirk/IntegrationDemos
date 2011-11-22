﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Web;
using System.Net;
using System.IO;

namespace AddMerchandise
{
    class AddMerchandise
    {
        private const string oauthendpoint = "/services/oauth2/token";
        private const string oauthoptions = "grant_type=password";

        public string token;
        public string instance_url;
        public Dictionary<string, string> properties;

        static void Main(string[] args)
        {
            AddMerchandise app = new AddMerchandise();
            if(app.login()) {
                app.insertItem();
            }
        }

        public AddMerchandise() {
            properties = new Dictionary<string, string>();
            foreach (String row in File.ReadAllLines("AddMerchandise.txt"))
            {
                properties.Add(row.Split('=')[0], row.Split('=')[1]);
            }
        }

        public Boolean login()
        {
            Console.Write("Logging in: "+properties["username"]+", "+properties["login_url"]);
            
            try {
                string login_params =  oauthoptions + "&client_id=" + properties["consumerkey"] + "&client_secret=" + properties["privatekey"] + "&username=" + properties["username"] + "&password=" + properties["password"];
                string responseFromServer = doHTTPRequest(properties["login_url"] + oauthendpoint, login_params, "", false);
                
                string[] data = responseFromServer.Split(':');
                token = data[7];
                token = token.Substring(1, token.Length - 1);
                token = token.Replace("\"}", "");

                instance_url = data[5];
                instance_url = instance_url.Substring(2, instance_url.Length - 2);
                instance_url = instance_url.Substring(0, instance_url.IndexOf("\""));

                return true;
                } catch (System.Exception error) {
                Console.Write(error);
                Console.Write("\n\nError logging in.  Please check that buyerapp.txt has proper credentials.\n"); 
                    
                
                return false;      
                }
        }

        public void insertItem()
        {
            string postData = "{\"Name\" : \"" + properties["merchandise_name"] + "\"";
            if(properties["merchandise_price"] != "") {
              postData += ", \"Price__c\" : " + properties["merchandise_price"];
            }
            if(properties["merchandise_inventory"] != "") {
              postData += ", \"Inventory__c\" : " + properties["merchandise_inventory"];
            }
            postData += "}";
            
            string endpoint = "https://" + instance_url + "/services/data/v" + properties["api"] + "/sobjects/Merchandise__c";
            string responseFromServer = doHTTPRequest(endpoint, postData, token, true);
            Console.Write("\n");
            responseFromServer = responseFromServer.Replace("{","");
            responseFromServer = responseFromServer.Replace("}","");
            responseFromServer = responseFromServer.Replace("[","");
            responseFromServer = responseFromServer.Replace("]","");
            responseFromServer = responseFromServer.Replace(",","\n");
            
            Console.Write(responseFromServer);
        }

        public string doHTTPRequest(string endpoint, string postData, string token, Boolean isJSON)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            if (isJSON)
            {
                request.ContentType = "application/json";
            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            } 
            if (token != "")
            {
                request.Headers["Authorization"] = "OAuth " + token;
            }

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response;
            try {
                response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
            } catch (System.Net.WebException error) {
                response = error.Response;
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();  
                return responseFromServer;  
            }
            
            

            
        }

        

        
    }
}
