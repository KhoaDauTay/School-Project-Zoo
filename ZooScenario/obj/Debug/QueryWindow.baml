   M S B A M L   `   `   `  ����F  BZooScenario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ZooScenarioZooScenario  5   	       ZooScenario.QueryWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ZooScenario     5   	     � Title$  QueryWindow��5   	   $	��250��6   $	��500��6*   .��5	      � .+�5
   
   �� -   5         ��Name$ totalAnimalWeightButton��5
      $��Total Animal Weight��62   $
��Left=�6P   $��	10,10,0,0q�6k   $	��Top=�6~   $	��119��6�   $��25��6�   6�   �� 5   
   -   5        $ averageAnimalWeightButton��5      $��Average Animal Weight��64   $
��Left=�6T   $��	10,40,0,0q�6o   $	��Top=�6�   $	��119��6�   $��25��6�   6�   �� 5   
   -   5        $ animalCountButton��5      $��Count Animals��6,   $
��Left=�6D   $��	10,70,0,0q�6_   $	��Top=�6r   $	��119��6�   $��25��6�   6�   �� 5   
   -   5        $ firstHeavyAnimalButton��5      $��Get Heavy Animals��61   $
��Left=�6M   $��
134,10,0,0q�6h   $	��Top=�6|   $	��119��6�   $��25��6�   6�   �� 5   
   -   5        $ firstYoungGuestButton��5      $��Get Youngest Guests��60   $
��Left=�6N   $��
134,40,0,0q�6i   $	��Top=�6}   $	��119��6�   $��25��6�   6�   �� 5   
   -   5        $ firstFemaleDingoButton��5      $��Get Female Dingoes��61   $
��Left=�6N   $��
134,70,0,0q�6i   $	��Top=�6}   $	��119��6�   $��25��6�   6�   �� 5   
   -   5         ��Name$ Result��5      $��Result:��6    $
��Left=�62   $��
10,115,0,0q�6M   $	��Top=�6a   �� 5   
   -   5         ��Name$ resultTextBox��5      $
��Left=�6)   $��23��6D   $��
60,118,0,0q�6P    �� TextWrapping$
 Wrap=�6d   $	��Top=�6x   $	��100��6�   &   System.Windows.Controls.DataGrid  5   
   -	   5          Name$ resultDataGrid��5      $
��Left=�6+   $��195,118,0,0q�6F   $	��Top=�6[   $��92��6s   $	��287��6   �� 5   
   -
   5        $ GetGuestsByAgeButton��5      $��Get Guests By Age��6/   $
��Left=�6K   $��
258,10,0,0q�6f   $	��Top=�6z   $	��119��6�   $��25��6�   6�   �� 5   
   -   5        $ GetFlyingAnimalsButton��5      $��Get Flying Animals��61   $
��Left=�6N   $��
258,40,0,0q�6i   $	��Top=�6}   $	��119��6�   $��25��6�   6�   �� 5   
   -   5        $ GetAdoptedAnimalsButton��5      $��Get Adopted Animals��62   $
��Left=�6P   $��
258,70,0,0q�6k   $	��Top=�6   $	��119��6�   $��25��6�   6�   �� 5   
   -   5        $" GetTotalBalanceByWalletColor��5      $��Balance By Color��67   $
��Left=�6R   $��
382,10,0,0q�6m   $	��Top=�6�   $	��100��6�   $��25��6�   6�   �� 5   
   -   5        $" GetAverageWeightByAnimalType��5      $��Avg Wght Animal��67   $
��Left=�6Q   $��
382,40,0,0q�6l   $	��Top=�6�   $	��100��6�   $��25��6�   6�   5      5      