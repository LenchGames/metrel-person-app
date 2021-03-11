using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person_Managment.Data {
    public class Person : INotifyPropertyChanged {

        private string _givenName;
        private string _lastName;
        private string _taxNo;
        private Address _address;
        /// <summary>
        /// Unikatna vrednost osebe.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Ime osebe.
        /// </summary>
        public string GivenName { 
            get { return _givenName; } 
            set { _givenName = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }
        /// <summary>
        /// Priimek osebe.
        /// </summary>
        public string LastName {
            get { return _lastName; }
            set { _lastName = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }
        /// <summary>
        /// Davčna številka osebe.
        /// </summary>
        public string TaxNumber {
            get { return _taxNo; }
            set { _taxNo = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }
        /// <summary>
        /// Domač naslov osebe.
        /// </summary>
        public Address HomeAddress {
            get { return _address; }
            set { _address = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }

        /// <summary>
        /// Lastnost, ki vrne konkatiniacijo imena in priimka ločena z presledkom.
        /// </summary>
        public string FullName {
            get { return LastName + " " + GivenName; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public Person() {
            _givenName = ""; _lastName = ""; _taxNo = "";
            _address = new Address();
        }
        public Person(int id, string name, string lastName, string taxNo, Address address) {
            ID = id;
            _address = address;
            _givenName = name;
            _lastName = lastName;
            _taxNo = taxNo;
        }
        public Person(int id, string name, string lastName, string taxNo, string houseNo, string street, string city, string postalCode, string country) : 
            this(id, name, lastName, taxNo, new Address(houseNo, street, city, postalCode, country)) {}

        public Person(Person p) : this(p.ID, p._givenName, p._lastName, p._taxNo, new Address(p._address)) { }

        /// <summary>
        /// Metoda za podosobitev podatkov v objektu.
        /// </summary>
        /// <param name="editedValues">Objekt s spremenjenimi vrednostmi</param>
        public void Edit(Person editedValues) {
            _givenName = editedValues._givenName;
            _lastName = editedValues._lastName;
            _taxNo = editedValues._taxNo;
            _address.Edit(editedValues._address);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        /// <summary>
        /// Preveri ali se v objektu nahaja niz, ki se ujema z iskalnim nizom.
        /// </summary>
        /// <param name="searchString">Iskalni niz.</param>
        /// <returns></returns>
        public bool SearchFilter(string searchString) {
            return _givenName.Contains(searchString) ||
                _lastName.Contains(searchString) ||
                _taxNo.Contains(searchString) ||
                _address.SearchFilter(searchString);
        }
    }
}
