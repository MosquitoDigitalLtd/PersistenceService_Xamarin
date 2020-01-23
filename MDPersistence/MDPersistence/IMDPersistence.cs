using System;
using System.Collections.Generic;
using Realms;

namespace SampleXamarinApp.Services.Interfaces
{
    public interface IMDPersistence
    {
        T DetachObject<T>(T Model) where T : RealmObject;
        List<T> DetachObjects<T>(List<T> Models) where T : RealmObject;
        T GetItem<T>() where T : RealmObject;
		List<T> GetItems<T>() where T : RealmObject;
		void SaveItems<T>(List<T> items) where T : RealmObject;
		void SaveItem<T>(T item) where T : RealmObject;
		void Update(Action update);
		void Delete<T>(T item) where T : RealmObject;
		void DeleteAll<T>() where T : RealmObject;

    }
}
