   M S B A M L   `   `   `  ����A  =WpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WpfAppWpfApp  5   	       WpfApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   &localclr-namespace:WpfApp     5   	   Imy@http://schemas.microsoft.com/winfx/2006/xaml/presentation/ribbon  5   5     � Title$  
MainWindow��6	   $	��350��6   $	��525��6)    � 	Resources 5         WpfApp.Phone   
nexusPhone   Title   Company   Price��%O   &	          5   
   $ Nexus X5��6)   $ Google��6:   $ 25000��6K   5	      .��5
      � 
���   � � DataContext# [  6   
������� � ColumnDefinitions 5   
   �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��Модель��6   �� 5   
   $��1��6+   ��6   �� *Title+6+   �� 5   
   $ ��Производитель��6   $��1��6)   �� 5   
   $��1��6-   $��1��6=   ��6   �� *	Company+6=   �� 5   
   $��Цена��6   $��2��6    �� 5   
   -   5        $��2��5   +   $��1��6;   ��6   �� *Price+6H   �� 5   
   -   5        $��button1��5   i   $��Button��6   $��1��6"   $��23��6/   $
��Left=�6;   $��	12,28,0,0q�6V   $	��Top=�6x   $��75��6�   6�   5      5      