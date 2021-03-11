using Microsoft.VisualStudio.TestTools.UnitTesting;
using Person_Managment.Data;
using System.Collections.ObjectModel;

namespace Person_Managment_Test {
    [TestClass]
    public class DataStorageTest {
        /// <summary>
        /// Test za prevejanje ali je število prebranih obbjektov enako številu zapisanih.
        /// </summary>
        [TestMethod]
        public void Test_SaveAndLoad() {
            ObservableCollection<Person> list = new ObservableCollection<Person>();
            list.Add(new Person(0, "John", "Doe", "10001", new Address()));
            list.Add(new Person(1, "Jane", "Doe", "10002", new Address()));
            DataStorage storage = new DataStorage("./test_ds_save_load.xml");
            storage.SaveData(list);
            ObservableCollection<Person> loadedList = storage.LoadData();
            Assert.AreEqual(list.Count, loadedList.Count);            
        }
        /// <summary>
        /// Preveri ali se ime, priimek in davèna št. pravilno shranejo in preberejo.
        /// </summary>
        [TestMethod]
        public void Test_SavingPersonData() {
            ObservableCollection<Person> list = new ObservableCollection<Person>();
            Person doe = new Person(0, "John", "Doe", "10001", new Address());
            list.Add(doe);
            DataStorage storage = new DataStorage("./test_ds_save_load.xml");
            storage.SaveData(list);
            ObservableCollection<Person> loadedList = storage.LoadData();
            if (loadedList.Count == 1) {
                Person loadedDoe = loadedList[0];
                Assert.AreEqual(doe.GivenName, loadedDoe.GivenName);
                Assert.AreEqual(doe.LastName, loadedDoe.LastName);
                Assert.AreEqual(doe.TaxNumber, loadedDoe.TaxNumber);
            } else Assert.Fail("Prièakovano število prebranih oseb: "+list.Count+" - dejansko ševilo prebranih oseb: " + loadedList.Count);
        }
        /// <summary>
        /// Preveri ali se naslov pravilno shrani in prebere.
        /// </summary>
        [TestMethod]
        public void Test_SavingPersonAddress() {
            ObservableCollection<Person> list = new ObservableCollection<Person>();
            Person doe = new Person(0, "John", "Doe", "10001", new Address("1", "New Street", "Beautiful City", "1337", "USA"));
            list.Add(doe);
            DataStorage storage = new DataStorage("./test_ds_save_load.xml");
            storage.SaveData(list);
            ObservableCollection<Person> loadedList = storage.LoadData();
            if (loadedList.Count == 1) {
                Person loadedDoe = loadedList[0];
                if (loadedDoe.HomeAddress != null) {
                    Assert.AreEqual(doe.HomeAddress.HouseNumber, loadedDoe.HomeAddress.HouseNumber);
                    Assert.AreEqual(doe.HomeAddress.Street, loadedDoe.HomeAddress.Street);
                    Assert.AreEqual(doe.HomeAddress.City, loadedDoe.HomeAddress.City);
                    Assert.AreEqual(doe.HomeAddress.PostalCode, loadedDoe.HomeAddress.PostalCode);
                    Assert.AreEqual(doe.HomeAddress.Country, loadedDoe.HomeAddress.Country);
                } else Assert.Fail("Napaka pri branju naslova. Vrednost naslova je 'null'.");
            } else Assert.Fail("Prièakovano število prebranih oseb: " + list.Count + " - dejansko ševilo prebranih oseb: " + loadedList.Count);
        }
        /// <summary>
        /// Test za prevejanje brisanja osebe.
        /// </summary>
        [TestMethod]
        public void Test_Delete() {
            ObservableCollection<Person> list = new ObservableCollection<Person>();
            Person john = new Person(0, "John", "Doe", "10001", new Address());
            Person jane = new Person(1, "Jane", "Doe", "10002", new Address());
            list.Add(john);
            list.Add(jane);
            DataStorage storage = new DataStorage("./test_ds_save_load.xml");
            storage.SaveData(list);
            storage.Delete(new Person() { ID = 1 });
            ObservableCollection<Person> loadedList = storage.LoadData();
            Assert.AreEqual(list.Count - 1, loadedList.Count);
        }
        /// <summary>
        /// Test za prevejanja urejanja osebe
        /// </summary>
        [TestMethod]
        public void Test_Edit() {
            ObservableCollection<Person> list = new ObservableCollection<Person>();
            Person john = new Person(0, "John", "Doe", "10001", new Address());
            list.Add(john);
            DataStorage storage = new DataStorage("./test_ds_save_load.xml");
            storage.SaveData(list);
            john.TaxNumber = "20002";
            storage.Edit(john);
            ObservableCollection<Person> loadedList = storage.LoadData();
            if (loadedList.Count == 1) {
                Person loadedDoe = loadedList[0];
                Assert.AreEqual(john.GivenName, loadedDoe.GivenName);
                Assert.AreEqual(john.LastName, loadedDoe.LastName);
                Assert.AreEqual(john.TaxNumber, loadedDoe.TaxNumber);
            } else Assert.Fail("Prièakovano število prebranih oseb: " + list.Count + " - dejansko ševilo prebranih oseb: " + loadedList.Count);
        }
        /// <summary>
        /// Test za prevejanje dodajanja oseb
        /// </summary>
        [TestMethod]
        public void Test_AddPerson() {
            ObservableCollection<Person> list = new ObservableCollection<Person>();
            Person john = new Person(0, "John", "Doe", "10001", new Address());
            list.Add(john);
            DataStorage storage = new DataStorage("./test_ds_save_load.xml");
            storage.SaveData(list);

            storage.AddNew(new Person(-1, "Jane", "Doe", "10002", new Address()));
            ObservableCollection<Person> loadedList = storage.LoadData();
            Assert.AreEqual(list.Count + 1, loadedList.Count);
        }        
        /// <summary>
        /// Test za prevejranje iskanja oseb
        /// </summary>
        [TestMethod]
        public void Test_Search() {
            ObservableCollection<Person> list = new ObservableCollection<Person>();
            list.Add(new Person(0, "John", "Doe", "10001", new Address()));
            list.Add(new Person(1, "Jane", "Doe", "10001", new Address()));
            list.Add(new Person(2, "John", "Smith", "10001", new Address()));
            DataStorage storage = new DataStorage("./test_ds_save_load.xml");
            storage.SaveData(list);            
            ObservableCollection<Person> loadedList = storage.Search("Doe");
            Assert.AreEqual(2, loadedList.Count);
        }
        /// <summary>
        /// Test za prevejanje nalaganja iz neobstojeèe datoteke.
        /// </summary>
        [TestMethod]
        public void Test_FailedLoading() {
            DataStorage storage = new DataStorage("./test_ds_non_existant.xml");
            ObservableCollection<Person> loadedList = storage.LoadData();
            Assert.AreEqual(0, loadedList.Count);
        }
    }
}
