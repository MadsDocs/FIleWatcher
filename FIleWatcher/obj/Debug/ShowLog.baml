   M S B A M L   `   `   `  ����F  BFileWatcher, Version=0.0.5.8, Culture=neutral, PublicKeyToken=null)clr-namespace:FileWatcherFileWatcher  5   	       FileWatcher.ShowLog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:FileWatcher     5   	     � Title$  ShowLog��5   	   $
��1000��6   $
��1000��6'   .��5	      �  � RowDefinitions 5
   
   �� 5      $	��28* �6   �� 5      $
��941* �6   .+�5   
   	� -   5         	�Name$ rdb_showEntry��5      $��Show Entries��6-   $
��Left=�6D   $��
189,14,0,0q�6_   $	��Top=�6s   $��2��6�   	� 5   
   -   5        $ rdb_showLog��5      $��	Show Logs��6+   $
��Left=�6?   $��
281,14,0,0q�6Z   $	��Top=�6n   $��2��6�   �� 5   
   -   5         ��Name$ 
lstbx_show��5      $��Center=�6&   $	��934��6C   $��0,7,0,0q�6P   $��Center=�6a   $	��992��6|   $��1��6�   �� 5   
   -   5         ��Name$ lbl_counter��5      $
��Left=�6%   $��	492,9,0,0q�6@   $	��Top=�6S   $	��210��6k   $��26��6w   $��2��6�   �� 5   
   -   5         ��Name$ 
btn_update��5      $��Update��6%   $
��Left=�66   $��
109,12,0,0q�6Q   $	��Top=�6e   $��75��6}   $��2��6�   �� 5   
   -   5        $ btn_showstats��5      $��
Show Stats��6(   $
��Left=�6=   $��	10,12,0,0q�6X   $	��Top=�6k   $��94��6�   $��2��6�   5      5      