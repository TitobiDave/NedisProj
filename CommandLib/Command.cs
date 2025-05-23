using DatabaseLib.DataStruct;
using DatabaseLib.Sevices.Expiration.Contract;
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
        private readonly IExpireKey _expireKey;
        public Command(DictDb db, IExpireKey expireKey)
        {
            _db = db;
            _expireKey = expireKey;
            commandDict["GET"] = (List<string> parsed) =>
            {
                return _db.GetItemByKey(parsed[1], out _);
            };
            commandDict["SET"] = (List<string> parsed) =>
            {
                TimeSpan span = default(TimeSpan);

                if (parsed.Count > 3)
                {
                    span = parsed[3] == null ? TimeSpan.Zero : TimeSpan.FromSeconds(Convert.ToDouble(parsed[3]));
                }

                return _db.DbSetValue(parsed[1], parsed[2], span);
            };
            commandDict["DEL"] = (List<string> parsed) =>
            {
                return _db.DbRemoveValue(parsed[1]);
            };
            commandDict["EXPIRE"] = (List<string> parsed) =>
            {
                return _expireKey.SetKeyExpirationSec(parsed[1], TimeSpan.FromSeconds(Convert.ToDouble(parsed[2])));
                throw new Exception();
            };

        }


        public ResponseModel ParseCommand(string? commandItem)
        {
            if (commandItem == null)
            {
                return new ResponseModel
                {
                    IsSuccesful = false,
                    ErrorMessage = "No command to execute",
                };
            }
            List<string> parsed = new List<string>(4);
            parsed.AddRange(commandItem.Split(' ', StringSplitOptions.TrimEntries));

            var res = ExecCommand(parsed);
            return res;
        }

        private ResponseModel ExecCommand(List<string> parsed)
        {
            try
            {
                var commandExist = commandDict.TryGetValue(parsed[0].ToUpper(), out Delegate ExecMethod);
                if (!commandExist)
                {
                    return new ResponseModel
                    {
                        IsSuccesful = false,
                        ErrorMessage = "Command does not exist"
                    };
                }
                var content = ExecMethod.DynamicInvoke(new object[] { parsed });
                return new ResponseModel
                {
                    IsSuccesful = true,
                    ErrorMessage = "Command Executed Succesfully"
                };
            }
            catch (Exception ex)
            {

                return new ResponseModel
                {
                    IsSuccesful = false,
                    ErrorMessage = "Command Failed to execute"
                };
            }

        }
    }
}
