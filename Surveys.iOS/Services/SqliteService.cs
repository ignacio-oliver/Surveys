﻿using SQLite;
using Surveys.Core.ServiceInterfaces;
using Surveys.iOS.Services;
using Xamarin.Forms;

[assembly:Dependency(typeof(SqliteService))]

namespace Surveys.iOS.Services
{
    public class SqliteService : ISqliteService
    {
        public SQLiteConnection GetConnection()
        {
            var localDBFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "surveys.db");
            return new SQLiteConnection(localDBFile);
        }
    }
}