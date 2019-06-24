using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using userapi.Models;
using Newtonsoft.Json;

namespace ConsoleUsersManager
{
    /* This is to handle events called from console */
    class EventMgr
    {
        // Modify api_url if api calling from elsewhere
        public const string api_url = "https://localhost:44309/api/";

        public string GetResFromApi(string ctrler, string param)
        // Get response from API call by GET 
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString(api_url + ctrler + "/" + param);
                return result;
            }
        }

        public string SendDataToApi(string ctrler, string method, string data, int? id = null)
        // Send data to API and get response using method in parameters
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(api_url + ctrler + "/" + (id.HasValue ? id.Value.ToString() : ""), method, data);
                return result;
            }
        }

        public void GetUser(string st)
        // Syntax: view [userid]
        {
            string res = GetResFromApi("users", st);
            Console.WriteLine(Environment.NewLine + res);
        }

        public void AddUser(string[] tokens)
        // Syntax: add [username] [email] [alias] [firstname] [lastname]
        {
            try
            {
                Users user = new Users
                {
                    UserName = tokens[1],
                    Email = tokens[2],
                    Alias = tokens[3],
                    FirstName = tokens[4],
                    LastName = tokens[5]
                };
                string res = SendDataToApi("users", "POST", JsonConvert.SerializeObject(user));
                Console.WriteLine(Environment.NewLine + res);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Syntax.");
                Console.WriteLine("Syntax: add [username] [email] [alias] [firstname] [lastname]");
            }
        }
        public void DeleteUser(int id)
        // Syntax: delete [userid]
        {
            string res = SendDataToApi("users", "DELETE", string.Empty, id);
            Console.WriteLine(Environment.NewLine + res);
        }

        public void UpdateUser(int id, string[] tokens)
        // Syntax: update [userid] [username] [email] [alias] [firstname] [lastname]
        {
            try
            {
                Users user = new Users
                {
                    UserId = id,
                    UserName = tokens[2],
                    Email = tokens[3],
                    Alias = tokens[4],
                    FirstName = tokens[5],
                    LastName = tokens[6]
                };
                string res = SendDataToApi("users", "PUT", JsonConvert.SerializeObject(user), id);
                Console.WriteLine(Environment.NewLine + res == "\r\n" ? "User " + id + " updated" : res);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Syntax.");
                Console.WriteLine("Syntax: update [userid] [username] [email] [alias] [firstname] [lastname]");
            }
        }

        public void GetManagers()
        // Syntax: getmgr
        {
            string res = GetResFromApi("managers", "");
            Console.WriteLine(Environment.NewLine + res);
        }

        public void GetManagerByUserName(string username)
        // syntax: getmgr [username]
        {
            string res = GetResFromApi("managers/getclientsbyname", username);
            Console.WriteLine(Environment.NewLine + res);
        }

        public void GetClients()
        // Syntax: getclients
        {
            string res = GetResFromApi("clients", "");
            Console.WriteLine(Environment.NewLine + res);
        }
    }
}
