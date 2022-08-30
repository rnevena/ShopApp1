//using ShopApp1.Application.Commands;
//using ShopApp1.Application.DTO;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Text;

//namespace ShopApp1.Implementation.Commands
//{
//    public class RawSqlCreateGroupCommand : ICreateGroupCommand
//    {
//        private readonly IDbConnection dbConnection;

//        public RawSqlCreateGroupCommand(IDbConnection dbConnection)
//        {
//            this.dbConnection = dbConnection;
//        }
//        public int Id => 2;

//        public string Name => "create new group using raw sql";

//        public void Execute(GroupDto request)
//        {
//            var query = "INSERT INTO GROUPS (name) VALUES('" + request.Name + "')";
//            var command = dbConnection.CreateCommand();
//            command.CommandText = query;
//            command.ExecuteNonQuery();
//        }
//    }
//}
