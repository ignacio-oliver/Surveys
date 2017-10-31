using SQLite;
using Surveys.Entities;
using Surveys.Core.ServiceInterfaces;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Surveys.Core.Services
{
    public class LocalDbService : ILocalDbService
    {
        private readonly SQLiteConnection connection = null;

        public LocalDbService()
        {
            this.connection = DependencyService.Get<ISqliteService>().GetConnection();
            CreateDatabase();
        }

        private void CreateDatabase()
        {
            IEnumerator<TableMapping> tables = connection.TableMappings.GetEnumerator();
            bool exists = false;
            while(!exists && tables.MoveNext())
            {
                if(tables.Current.TableName == nameof(Survey))
                {
                    exists = true;
                }
            }
            if (!exists)
                connection.CreateTable<Survey>();
        }

        public Task<IEnumerable<Survey>> GetAllSurveysAsync()
        {
            return Task.Run(() => (IEnumerable<Survey>)connection.Table<Survey>());
        }

        public Task InsertSurveyAsync(Survey survey)
        {
            return Task.Run(() => connection.Insert(survey));
        }

        public Task DeleteSurveyAsync(Survey survey)
        {
            return Task.Run(() =>
            {
                var query = $"DELETE FROM Survey WHERE Id = '{survey.Id}'";
                var command = connection.CreateCommand(query);
                var result = command.ExecuteNonQuery();
                return result > 0;
            });
        }
    }
}
