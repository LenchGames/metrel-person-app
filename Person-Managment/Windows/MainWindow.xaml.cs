using Microsoft.Win32;
using Person_Managment.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Person_Managment {
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged {

        private ObservableCollection<Person> personList;

        private DataStorage Storage;
        /// <summary>
        /// Lastnost, ki vrne pot datotečne shrambe (Za izris v grafičnem prikazu).
        /// </summary>
        public string FileStoragePath { get { return Storage?.FilePath; } }
        
        /// <summary>
        /// Oseba, ki se jo trenutno prikazuje/ureja/ustvarja.
        /// </summary>
        private Person editedPerson;
        private Person _person;

        /// <summary>
        /// Trenutna izbrana oseba.
        /// </summary>
        public Person SelectedPerson {
            get { return _person; }
            set { _person=value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }
        public bool isPersonSelected { get { return _person != null; } }



        private ManagerMode _mode = ManagerMode.DISPLAY;
        /// <summary>
        /// Način izvajanja aplikacije.
        /// </summary>
        public ManagerMode Mode {
            get { return _mode; }
            set { _mode = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }

        /// <summary>
        /// Besedilo, ki se naj izpiše na gumbu za dodajanje oseb. Odvisno od načina izvajanja aplikacije (vrednost Mode)
        /// </summary>
        public string AddPersonButtonText { get { return _mode == ManagerMode.CREATE ? "Shrani osebo" : "Dodaj osebo"; } }
        /// <summary>
        /// Besedilo, ki se naj izpiše na gumbu za urejanje podatkov o osebi. Odvisno od načina izvajanja aplikacije (vrednost Mode)
        /// </summary>
        public string EditPersonButtonText { get { return _mode == ManagerMode.EDIT ? "Shrani spremembe" : "Uredi osebo"; } }        
        /// <summary>
        /// Vrednost, ki pove ali je interakcija z gumbom za dodajanje oseb možna ali ne.
        /// </summary>
        public bool AddPersonButtonEnabled { get { return _mode != ManagerMode.EDIT && Storage != null; } }
        /// <summary>
        /// Vrednost, ki pove ali je interakcija z gumbom za urejanje podatkov o osebi možna ali ne.
        /// </summary>
        public bool EditPersonButtonEnabled { get { return _mode != ManagerMode.CREATE && SelectedPerson != null && Storage != null; } }
        /// <summary>
        /// Lastnost, ki vrne vrednost Visibility za gumb za preklic urejanja.
        /// </summary>
        public Visibility CancleButtonVisibility { get { return _mode == ManagerMode.DISPLAY ? Visibility.Collapsed : Visibility.Visible; } }
        /// <summary>
        /// Lastnost, ki vrne vrednost Visibility za gumb za izbris osebe.
        /// </summary>
        public Visibility DeleteButtonVisibility { get { return _mode == ManagerMode.DISPLAY && SelectedPerson != null && Storage != null ? Visibility.Visible : Visibility.Collapsed; } }
        /// <summary>
        /// Lastnost, ki pove ali je aplikacija v načinu urejanja (EDIT ali CREATE).
        /// </summary>
        public bool IsEditMode { get { return _mode != ManagerMode.DISPLAY; } }


        public MainWindow() {
            personList = new ObservableCollection<Person>();
            InitializeComponent();
            DataContext = this;
            peopleListDisplay.ItemsSource = personList;
        }
        /// <summary>
        /// Event za posodabljanje grafičnega vmesnika
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Metoda gumba za dodajanje novih oseb.
        /// </summary>
        private void AddPersonButtonClick(object sender, RoutedEventArgs e) {
            if(Mode == ManagerMode.DISPLAY) {
                SelectedPerson = new Person();
                Mode = ManagerMode.CREATE;
            }else if(Mode == ManagerMode.CREATE) {
                personList.Add(SelectedPerson);
                SelectedPerson.ID = (int)Storage?.AddNew(SelectedPerson);
                Mode = ManagerMode.DISPLAY;
            }
        }        
        /// <summary>
        /// Metoda gumba za spreminjanje podatkov o osebi.
        /// </summary>
        private void EditPersonButtonClick(object sender, RoutedEventArgs e) {
            if (Mode == ManagerMode.DISPLAY) {
                editedPerson = SelectedPerson;
                SelectedPerson = new Person(SelectedPerson);
                Mode = ManagerMode.EDIT;
            } else if (Mode == ManagerMode.EDIT) {
                editedPerson.Edit(SelectedPerson);
                SelectedPerson = editedPerson;
                Storage?.Edit(SelectedPerson);
                Mode = ManagerMode.DISPLAY;
            }
        }
        /// <summary>
        /// Metoda gumba za preklic urejanja/dodajanja osebe.
        /// </summary>
        private void CancleButtonClick(object sender, RoutedEventArgs e) {
            Mode = ManagerMode.DISPLAY;
            SelectedPerson = (Person)peopleListDisplay.SelectedItem;
        }
        /// <summary>
        /// Metoda gumba za izbris o osebe.
        /// </summary>
        private void DeleteButtonClick(object sender, RoutedEventArgs e) {
            if(peopleListDisplay.SelectedItem != null) {
                SelectedPerson = (Person)peopleListDisplay.SelectedItem;
                MessageBoxResult messageBoxResult = MessageBox.Show("Ste prepričani, da želite izbirsati osebo \"" + SelectedPerson.FullName+"\"?", "Potrditev izbrisa", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes) {
                    personList.Remove(SelectedPerson);
                    Storage.Delete(SelectedPerson);
                    SelectedPerson = null;
                }
            }
        }

        /// <summary>
        /// Metoda gumba za iskanje oseb.
        /// </summary>
        private void SearchButtonClick(object sender, RoutedEventArgs e) {
            if(Storage != null) {
                personList = Storage.Search(searchInput.Text);
                peopleListDisplay.ItemsSource = personList;
            } else {
                MessageBox.Show("Manjka podatkovna shramba! Odprite XML podatkovno shrambo z klikom na gumb 'Odpri' ali pa ustvarite novo s klikom na gumb 'Nova'.", "Manjakajoča podatkovna shramba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Metoda gumba za ustvarjanje nove datotečne shrambe.
        /// </summary>
        private void NewStorageClick(object sender, RoutedEventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML Files | *.xml";
            if(sfd.ShowDialog() == true) {
                Storage = new DataStorage(sfd.FileName);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
                Storage.SaveData(personList);
            }
        }
        /// <summary>
        /// Metoda gumba za nalaganje datotečne shrambe.
        /// </summary>
        private void LoadStorageClick(object sender, RoutedEventArgs e) {
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.Filter = "XML Files | *.xml";
            if (sfd.ShowDialog() == true) {
                Storage = new DataStorage(sfd.FileName);
                personList = Storage.Search(searchInput.Text);
                peopleListDisplay.ItemsSource = personList;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }

        /// <summary>
        /// Metoda, ki se kliče, ko uporabnik spustu levi mišlin klik na seznamu oseb.
        /// </summary>
        private void peopleListClick(object sender, MouseButtonEventArgs e) {
            if(Mode == ManagerMode.DISPLAY)
                SelectedPerson = (Person)peopleListDisplay.SelectedItem;
        }
    }

    /// <summary>
    /// Enum z vrednostmi za vodenje načina izvajanja aplikacije.<br/>
    /// DISPLAY - Prikaz podatkov o osebi. Urejanje je onemogočeno.<br/>
    /// EDIT - Urejanje podatkov o osebi. Urejanje je omogočeno.<br/>
    /// CREATE - Urejanje nove osebe. Urejanje je omogočeno.<br/>
    /// </summary>
    public enum ManagerMode { DISPLAY, EDIT, CREATE }
}
