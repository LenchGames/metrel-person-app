   M S B A M L   `   `   `  ����K  GPerson-Managment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Person_Managment.ComponentPerson_Managment.Component  5      .    (Person_Managment.Component.PersonDisplay   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Person_Managment.Component     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $' !../Design/PersonDisplayStyle.xaml?�6%   %    5      .��5      �    	container#��[  6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��0��6     data_display_header#��[ 6   .��6J   Podatki o osebi:6\   �� 5    
   $��1��6     label_keywords#��[ 6   .��6E   Ime:6K   �� 5!   
   $��2��6   #��[ 6   .��6E   
Priimek:6O   �� 5"   
   $��3��6   #��[ 6   .��6E   Davčna št.:6R   �� 5#   
   $��4��6   #��[ 6   .��6E   	Naslov:6N   �� 5$   
   $��1��6   $��1��6"     
label_data#��[ 6/   {�6S   ��  �� Path$ 
IsEditMode���6y   �� $ SelectedPerson.GivenName��� 5%   
   $��1��6   $��2��6"   #��[ 6/   {�6S   �� $ 
IsEditMode���6y   �� $ SelectedPerson.LastName�6�   �� 5&   
   $��1��6   $��3��6"   #��[ 6/   {�6S   �� $ 
IsEditMode���6y   �� $ SelectedPerson.TaxNumber�6�   � 5'   
   $��1��6   $��4��6    5(      �� 5)      $��1* �6    �� 5*      $��1* �6    �� 5+      $��1* �6    �� 5,      $��1* �6    �� 5-      $��1* �6    �� 5.      $��1* �6     50      �� 51      $
��auto �6#   �� 52      $��1* �6#   .+�54      �� $�� ��6   �� 55      $��1��6   #��[ 6!   .��6I   	Ulica.:6R   �� 56      $��2��6   #��[ 6!   .��6I   Hišna št.:6U   �� 57      $��3��6   #��[ 6!   .��6I   Kraj:6P   �� 58      $��4��6   #��[ 6!   .��6I   Poštna št.:6V   �� 59      $��5��6   #��[ 6!   .��6I   
Država:6R   �� 5:      $��1��6   $��1��6&   #��[ 63   {�6W   �� $ 
IsEditMode���6}   �� $, &SelectedPerson.HomeAddress.HouseNumber��� 5;      $��1��6   $��2��6&   #��[ 63   {�6W   �� $ 
IsEditMode���6}   �� $' !SelectedPerson.HomeAddress.Street��� 5<      $��1��6   $��3��6&   #��[ 63   {�6W   �� $ 
IsEditMode���6}   �� $% SelectedPerson.HomeAddress.City��� 5=      $��1��6   $��4��6&   #��[ 63   {�6W   �� $ 
IsEditMode���6}   �� $+ %SelectedPerson.HomeAddress.PostalCode��� 5>      $��1��6   $��5��6&   #��[ 63   {�6W   �� $ 
IsEditMode���6}   �� $( "SelectedPerson.HomeAddress.Country�5?      5@      5A      