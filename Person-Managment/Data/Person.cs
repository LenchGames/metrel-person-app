using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person_Managment.Data {
    public class Person {

        public string GivenName { get; set; }
        public string LastName { get; set; }
        public string TaxNumber { get; set; }
        public Address HomeAddress { get; set; }

        public Person() {

        }

    }
}
