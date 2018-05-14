using MVVM.Helper;
using MVVM.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MVVM.Droid
{
    public class SQLiteManager
    {
        SQLiteConnection _sqlconnection;
        public SQLiteManager()
        {
            _sqlconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqlconnection.CreateTable<PersonModel>();

        }
        #region CRUD
        public int Insert(PersonModel p)
        {
            return _sqlconnection.Insert(p);
        }

        public int Update(int p)
        {
            return _sqlconnection.Update(p);
        }

        public int Delete(int Id)
        {
            return _sqlconnection.Delete<PersonModel>(Id);
        }

        public IEnumerable<PersonModel> GetAll()
        {
            return _sqlconnection.Table<PersonModel>().OrderByDescending(x => x.Id); ;
        }
        public PersonModel Get(int Id)
        {
            return _sqlconnection.Table<PersonModel>().Where(x => x.Id == Id).FirstOrDefault();
        }
        public void Dispose()
        {
            _sqlconnection.Dispose();
        }
        #endregion
    }
}