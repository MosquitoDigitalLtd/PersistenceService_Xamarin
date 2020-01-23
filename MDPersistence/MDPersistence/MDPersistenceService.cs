using System;
using System.Collections.Generic;
using System.Linq;
using SampleXamarinApp.Services.Interfaces;
using Realms;

namespace SampleXamarinApp.Service
{
    /// <summary>
    /// Crud outline for database
    /// </summary>
    public class MDPersistence : IMDPersistence
    {
#pragma warning disable CS1701 // Assuming assembly reference matches identity

        Realm realm;

		public MDPersistence(RealmConfiguration realmConfiguration)
		{

			realm = Realm.GetInstance(realmConfiguration);
		}
        //get version of item that isnt live and can change with out using update method.
        public T DetachObject<T>(T Model) where T : RealmObject
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(
                       Newtonsoft.Json.JsonConvert.SerializeObject(Model)
                       .Replace(",\"IsManaged\":true", ",\"IsManaged\":false")
                   );
        }

        //get version of item that isnt live and can change with out using update method.
        public List<T> DetachObjects<T>(List<T> Models) where T : RealmObject
        {

            List<T> n = new List<T>();
            foreach(var item in Models)
            {
                n.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<T>(
                       Newtonsoft.Json.JsonConvert.SerializeObject(item)
                       .Replace(",\"IsManaged\":true", ",\"IsManaged\":false")
                   ));
            }
            return n;
        }

        public T GetItem<T>() where T : RealmObject
		{
			return realm.All<T>().ToList().FirstOrDefault();
		}

		public List<T> GetItems<T>() where T : RealmObject
		{
			return realm.All<T>().ToList();
		}

        //  will start managing arelamObject which has been created as a standalone object.
        // or if primiary index exsits it will update the item
        public void SaveItems<T>(List<T> items) where T : RealmObject
		{
			realm.Write(() =>
			{
                foreach (T item in items)
                {
                    realm.Add(item, true);
				}
			});
		}

        //  will start managing arelamObject which has been created as a standalone object.
        // or if primiary index exsits it will update the item
        public void SaveItem<T>(T item) where T : RealmObject
		{
			realm.Write(() =>
			{
				realm.Add(item,true);
			});
		}
        //run and a change to the model and gets saved
		public void Update(Action update)
		{
			realm.Write(update);
		}

		public void Delete<T>(T item) where T : RealmObject
		{
			using (var trans = realm.BeginWrite())
            {
				realm.Remove(item);
                trans.Commit();
            }          
		}

		public void DeleteAll<T>() where T : RealmObject
		{
			realm.RemoveAll<T>();

		}

    }
#pragma warning restore CS1701 // Assuming assembly reference matches identity

}
