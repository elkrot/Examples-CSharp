   M S B A M L   `   `   `  ����J  FEmployeeTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:EmployeeTracker.ViewEmployeeTracker.View  5      E'clr-namespace:EmployeeTracker.ViewModelEmployeeTracker.ViewModel  5      -    'EmployeeTracker.View.EmployeeDetailView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0v"clr-namespace:EmployeeTracker.View     5      6vm'clr-namespace:EmployeeTracker.ViewModel     5      .��5      �   � ColumnDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6$   $��Title:��61   �� 5    
   $��1��6   $��0��6"   $��50��6K   ��6/   ��  �� Path$ Title�6K   �� 5"   
   $��0��6   $��1��6$   $��First Name:��61   �� 5#   
   $��1��6   $��1��6"   $	��150��6O   ��6/   �� $ 	FirstName�6O   �� 5%   
   $��0��6   $��2��6$   $��
Last Name:��61   �� 5&   
   $��1��6   $��2��6"   $	��150��6N   ��6/   �� $ LastName�6N   �� 5(   
   $��0��6   $��3��6$   $��	Position:��61   �� 5)   
   $��1��6   $��3��6"   $	��150��6N   ��6/   �� $ Position�6N   �� 5+   
   $��0��6   $��4��6$   $��Department:��61   �� 5,   
   $��1��6   $��4��6#   $	��150��60    �� SelectedValuePath$ .��6<    �� DisplayMemberPath$ DepartmentName��6�    �� SelectedItem 6R   �� $ 
Department���6{   �� $ DepartmentLookup�6�   �� 5.   
   $��0��6   $��5��6$   $��Manager:��61   �� 5/   
   $��1��6   $��5��6#   $	��150��60   $ .��6<   $ DisplayName��6�    6R   �� $ Manager���6x   �� $ ManagerLookup�6�   �� 51   
   $��0��6   $��6��6$   $��Birth Date:��61   (  "System.Windows.Controls.DatePicker  52   
   $��1��6   $��6��6%      SelectedDate 62   �� $ 	BirthDate��� 54   
   $��0��6   $��7��6$   $��
Hire Date:��61     55   
   $��1��6   $��7��6%    62   �� $ HireDate��� 57   
   $��0��6   $��8��6$   $��Termination:��61     58   
   $��1��6   $��8��6%    62   �� $ TerminationDate�59      5:      