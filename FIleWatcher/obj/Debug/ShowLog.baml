   M S B A M L   `   `   `  ����F  BFileWatcher, Version=0.0.5.8, Culture=neutral, PublicKeyToken=null)clr-namespace:FileWatcherFileWatcher  5   	       FileWatcher.ShowLog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:FileWatcher     5   	   -   5          � Title$  ShowLog��5   	   $
��1000��6   $
��1000��6'   .��5	      �  J� RenderTransformOrigin$ 0.5,0.5&�6   z�5
   
   V� 5      .~�5      �� �� 5      �� 5       �� Angle$ 0.007Q�6"   T� 5      5       � ColumnDefinitions 5   
   �� 5      $
��573* �6   �� 5      $
��427* �6    � RowDefinitions 5   
   �� 5      �� 5      $	��23* �6   .+�5   
   	� -   5         	�Name$ rdb_showEntry��5      $��Show Entries��6-   $
��Left=�6D   $��	250,0,0,0q�6_   $��Center=�6r   $ 0.471,1.102&�6�   $��15��6�   $��87��6�   �� 5   
   -   5         ��Name$ lbl_counter��5      $
��Left=�6%   $��	492,0,0,0q�6@   $��Center=�6S   $	��210��6n   $��26��6z   $��2��6�   �� 5   
   -   5         ��Name$ 
btn_update��5      $��Update��6%   $
��Left=�66   $��	108,0,0,5q�6Q   $��Bottom=�6d   $��75��6   $��20��6�   �� 5   
   -   5        $ btn_showstats��5      $��
Show Stats��6(   $
��Left=�6=   $��	10,15,0,0q�6X   $	��Top=�6k   $��94��6�   $��20��6�   �� 5   
   -   5         ��Name$ lstview_logs��5      $��1��6)   $��0,12,0,0q�6a   $��2��6s   	 �� View	 5      �� 5       .P�5!      �� 5"      5$      5&      5'      