using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Person_Managment.Data {
    /// <summary>
    /// Razred za manipulacijo s shrambo podatkov.
    /// </summary>
    public class DataStorage {
        /// <summary>
        /// Pot do datoteke, ki predstavlja shrambo podatkov.
        /// </summary>
        public string FilePath { get; private set; }

        public DataStorage(string path) {
            FilePath = path;
        }
        /// <summary>
        /// Metoda za ustvajranje nove osebe.
        /// </summary>
        /// <param name="p">Nova oseba, ki naj se shrani v bazo.</param>
        /// <returns>Vrne ID vrednost nove osebe.</returns>
        public int AddNew(Person p) {
            ObservableCollection<Person> list = LoadData();
            if (list.Count == 0) p.ID = 0;
            else p.ID = list[list.Count - 1].ID + 1;
            list.Add(p);
            SaveData(list);
            return p.ID;
        }
        /// <summary>
        /// Metoda za urejanje osebe.
        /// </summary>
        /// <param name="p">Objekt tipa Person s spremenjenimi vrednostmi.</param>
        public void Edit(Person p) {
            ObservableCollection<Person> list = LoadData();
            foreach(Person pers in list) {
                if(p.ID == pers.ID) {
                    pers.Edit(p);
                    break;
                }
            }
            SaveData(list);
        }
        /// <summary>
        /// Metoda za brisanje osebe iz baze.
        /// </summary>
        /// <param name="p">oseba, ki se naj izbriše</param>
        public void Delete(Person p) {
            ObservableCollection<Person> list = LoadData();
            for (int i = 0; i < list.Count; i++) {
                if (p.ID == list[i].ID) {
                    list.RemoveAt(i);
                    break;
                }
            }
            SaveData(list);
        }
        /// <summary>
        /// Metoda za iskanje po bazi.
        /// </summary>
        /// <param name="searchString">Iskalni niz. Če je niz prazen, 'whitespace' ali enak null, se vrne celoten seznam oseb.</param>
        /// <returns>Seznam oseb, ki se ujemajo z iskalnim nizom</returns>
        public ObservableCollection<Person> Search(string searchString) {
            ObservableCollection<Person> allList = LoadData();
            if (string.IsNullOrEmpty(searchString) || string.IsNullOrWhiteSpace(searchString)) return allList;
            ObservableCollection<Person> list = new ObservableCollection<Person>();
            foreach(Person p in allList) {
                if (p.SearchFilter(searchString)) list.Add(p);
            }
            return list;
        }
        /// <summary>
        /// Metoda za serializacijo podatkov v XML datoteko.
        /// </summary>
        /// <param name="list">Seznam oseb, ki se naj shranejo.</param>
        public void SaveData(ObservableCollection<Person> list) {
            XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<Person>));
            TextWriter writer = new StreamWriter(FilePath);
            ser.Serialize(writer, list);
            writer.Close();
        }
        /// <summary>
        /// Metoda za deserializacijo podatkov iz XML datoteko.
        /// </summary>
        /// <returns>Seznam oseb v XML datoteki.</returns>
        public ObservableCollection<Person> LoadData() {
            try {
                XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<Person>));
                TextReader reader = new StreamReader(FilePath);
                ObservableCollection<Person> list = (ObservableCollection<Person>)ser.Deserialize(reader);
                reader.Close();
                return list == null ? new ObservableCollection<Person>() : list;
            }catch {
                return new ObservableCollection<Person>();
            }
        }
    }
}
