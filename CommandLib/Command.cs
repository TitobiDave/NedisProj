using DatabaseLib.DataStruct;
using DatabaseLib.DataStruct.ListDb;
using DatabaseLib.DataStruct.ListDb.Contract;
using DatabaseLib.Sevices.Expiration.Contract;
using Misc;
using Misc.Tokens;
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
           
            DefCommands();
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
            ITokenizer tokenizer = new Tokenizer(commandItem);
            var tokens = tokenizer.Tokenize();
            var res = ExecCommand(tokens);
            return res;
        }

        private ResponseModel ExecCommand(List<Token> parsed)
        {
            try
            {
                if (parsed == null)
                    throw new InvalidOperationException("Empty item");
                var commandExist = commandDict.TryGetValue(parsed[0].Value.ToUpper(), out Delegate? ExecMethod);
                if (!commandExist)
                {
                    return new ResponseModel
                    {
                        IsSuccesful = false,
                        ErrorMessage = "Command does not exist"
                    };
                }
                var content = ExecMethod?.DynamicInvoke(new object[] { parsed });
                return new ResponseModel
                {
                    IsSuccesful = true,
                    ErrorMessage = "Command Executed Succesfully"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseModel
                {
                    IsSuccesful = false,
                    ErrorMessage = "Command Failed to execute"
                };
            }

        }

        private void DefCommands()
        {
            commandDict["GET"] = (List<Token> parsed) =>
            {
                string index = default;
                if(parsed.Any(x=>x.Type == TokenType.Index))
                {
                    index = parsed.FirstOrDefault(x => x.Type == TokenType.Index).Value;
                }
                
                return _db.GetItemByKey(parsed[1].Value, out _, index);
            };
            commandDict["SET"] = (List<Token> parsed) =>
            {
                TimeSpan span = default(TimeSpan);

                if (parsed.Count > 3)
                {
                    span = parsed[3] == null ? TimeSpan.Zero : TimeSpan.FromSeconds(Convert.ToDouble(parsed[3]));
                }

                return _db.DbSetValue(parsed[1].Value, parsed[2].Value, span);
            };
            commandDict["DEL"] = (List<Token> parsed) =>
            {
                return _db.DbRemoveValue(parsed[1].Value);
            };
            commandDict["EXPIRE"] = (List<Token> parsed) =>
            {
                return _expireKey.SetKeyExpirationSec(parsed[1].Value, TimeSpan.FromSeconds(Convert.ToDouble(parsed[2].Value)));
            };
            commandDict["SETL"] = (List<Token> parsed) =>
            {
                TimeSpan span = default(TimeSpan);

                if (parsed.Count > 3)
                {
                    span = parsed[3] == null ? TimeSpan.Zero : TimeSpan.FromSeconds(Convert.ToDouble(parsed[3]));
                }
                return _db.DbSetList(parsed[1].Value, parsed[2].Value, span);
            };
        }
    }
}
