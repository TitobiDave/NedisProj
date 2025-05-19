using DatabaseLib.DataStruct;
using Misc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommandLib
{
    public class Command
    {
        public readonly Dictionary<string, Delegate> commandDict = new Dictionary<string, Delegate>();
        public readonly DictDb _db;
        public Command(DictDb db)
        {
            _db = db;
            commandDict["GET"] = (string[] parsed) =>
            {
                return _db.GetItemByKey(parsed[1], out _);
            };
            commandDict["SET"] = (string[] parsed ) =>
            {
                TimeSpan span = parsed[3] == null ? TimeSpan.Zero : TimeSpan.FromSeconds(Convert.ToDouble(parsed[3]));
                return _db.DbSetValue(parsed[1], parsed[2], span);
            };
            commandDict["DEL"] = (string[] parsed) =>
            {
                return _db.DbRemoveValue(parsed[1]);
            };
        }


        public ResponseModel ParseCommand(string? commandItem)
        {
            if(commandItem == null)
            {
                return new ResponseModel
                {
                    ErrorMessage = "No command to execute",
                };
            }
            string[] parsed = commandItem.Split(' ', StringSplitOptions.TrimEntries);

            var res = ExecCommand(parsed);
            return new ResponseModel
            {
                ErrorMessage = res?.ErrorMessage?.ToString(),
            };
        }

        public ResponseModel ExecCommand(string[] parsed)
        {
            var commandExist = commandDict.TryGetValue(parsed[0], out Delegate ExecMethod);
            if(!commandExist )
            {
                return new ResponseModel
                {
                    ErrorMessage = "Command does not exist"
                };
            }
            var content = ExecMethod.DynamicInvoke(new object[]{parsed});
            return new ResponseModel
            {
                ErrorMessage = "Command Executed Succesfully"
            };
        }
    }
}
