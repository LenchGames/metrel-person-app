using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person_Managment.Data {
    public class Address : INotifyPropertyChanged {

        private string _houseNo;
        private string _street;
        private string _city;
        private string _postal;
        private string _country;

        /// <summary>
        /// Hišna številka
        /// </summary>
        public string HouseNumber { 
            get { return _houseNo; }
            set { _houseNo = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }
        /// <summary>
        /// Ulica.
        /// </summary>
        public string Street {
            get { return _street; }
            set { _street = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }
        /// <summary>
        /// Mesto oz. kraj.
        /// </summary>
        public string City {
            get { return _city; }
            set { _city = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }
        /// <summary>
        /// Poštna številka kraja.
        /// </summary>
        public string PostalCode {
            get { return _postal; }
            set { _postal = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }
        /// <summary>
        /// Država.
        /// </summary>
        public string Country {
            get { return _country; }
            set { _country = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Address() {
            _houseNo = ""; _street = ""; _city = ""; _postal = ""; _country = "";
        }
        public Address(string houseNo, string street, string city, string postalCode, string country) {
            _houseNo = houseNo; _street = street; _city = city; _postal = postalCode; _country = country;
        }

        public Address(Address addr) 
           : this(addr._houseNo, addr._street, addr._city, addr._postal, addr._country) {}
        
        /// <summary>
        /// Metoda za podosobitev podatkov v objektu.
        /// </summary>
        /// <param name="editedValues">Objekt s spremenjenimi vrednostmi</param>
        public void Edit(Address editedValues) {
            _houseNo = editedValues._houseNo;
            _street = editedValues._street;
            _city = editedValues._city;
            _postal = editedValues._postal;
            _country = editedValues._country; 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        /// <summary>
        /// Preveri ali se v objektu nahaja niz, ki se ujema z iskalnim nizom.
        /// </summary>
        /// <param name="searchString">Iskalni niz.</param>
        /// <returns></returns>
        public bool SearchFilter(string searchString) {
            return _houseNo.Contains(searchString) || 
                _street.Contains(searchString) || 
                _city.Contains(searchString) || 
                _postal.Contains(searchString) || 
                _country.Contains(searchString);
        }
    }
}
