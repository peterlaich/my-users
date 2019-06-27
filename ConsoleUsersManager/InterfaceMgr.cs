using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleUsersManager
{
    /* This is to handle all console commands  */
    class InterfaceMgr
    {
        public const string CommandView = "view";
        public const string CommandAll = "all";
        public const string CommandAdd = "add";
        public const string CommandDelete = "delete";
        public const string CommandUpdate = "update";
        public const string CommandGetMgr = "getmgr";
        public const string CommandGetClients = "getclients";

        public void ErrInvalidInput()
        {
            Console.WriteLine("Invalid Input");
        }

        public string[] SplitInputToTokens(string st)
        {
            // split string into string tokens
            // double quoted strings will be considered as one token
            string[] strings = Regex.Split(st, "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            if (strings.Length > 0)
            {
                strings = strings.Select(x => (x[0] == '"') && (x[x.Length - 1] == '"')
                                            ? x.Substring(1, x.Length - 2)
                                            : x).ToArray();
                return strings;
            }
            else
            {
                return null;
            }
        }
        public void ConsoleInput(string strInput)
        // Read Console Input
        {
            EventMgr eventMgr = new EventMgr();
            string[] strInputTokens = SplitInputToTokens(strInput);
            if (strInputTokens[0] == CommandView) // view
            {
                if (strInputTokens.Length == 1) eventMgr.GetUser(""); // view all users
                else if (strInputTokens.Length == 2 && int.TryParse(strInputTokens[1], out int j)) eventMgr.GetUser(j.ToString()); // view by id
                else ErrInvalidInput();
            }
            else if (strInputTokens[0] == CommandAdd)
            {
                eventMgr.AddUser(SplitInputToTokens(strInput)); // add user
            }
            else if (strInputTokens[0] == CommandDelete)
            {
                if (strInputTokens.Length == 2 && int.TryParse(strInputTokens[1], out int j)) eventMgr.DeleteUser(j); // delete user by id
                else ErrInvalidInput();
            }
            else if (strInputTokens[0] == CommandUpdate)
            {
                if (strInputTokens.Length > 1 && int.TryParse(strInputTokens[1], out int j)) eventMgr.UpdateUser(j, strInputTokens); // update user by id
                else ErrInvalidInput();
            }
            else if (strInputTokens[0] == CommandGetMgr)
            {
                if (strInputTokens.Length == 1) eventMgr.GetManagers(); // get managers with clients
                else if (strInputTokens.Length == 2) eventMgr.GetManagerByUserName(strInputTokens[1]); // get managers by username
                else ErrInvalidInput();
            }
            else if (strInputTokens[0] == CommandGetClients)
            {
                if (strInputTokens.Length == 1) eventMgr.GetClients(); // get client with managers
                else ErrInvalidInput();
            }

            else ErrInvalidInput();
        }

    }
}
