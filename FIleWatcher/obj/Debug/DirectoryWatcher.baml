   M S B A M L   `   `   `  ����F  BFileWatcher, Version=0.0.5.8, Culture=neutral, PublicKeyToken=null)clr-namespace:FileWatcherFileWatcher  5   	   "    FileWatcher.DirectoryWatcher   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:FileWatcher     5   	   -   5          � Title$  DirectoryWatcher��5   	   $��377.834��6"   $��696.801��63   .��5	      � $��0,0,0,-3q�6   .+�5
   
   �� -   5         ��Name$ lstview_anzeige��5
      $
��Left=�6,   $	��260��6G   $	��Top=�6T   $	��662��6l   $��	10,10,0,0q�6x    �� View 5      �� 5      .P�5      �� 5      5      �� 5   
   -   5         ��Name$ 	btn_Start��5      $��Start��6$   $
��Left=�64   $��362,276,0,0q�6O   $	��Top=�6d   $��75��6|   6�   �� 5   
   -   5        $ btn_Stop��5      $
��Stop��6#   $
��Left=�62   $��442,276,0,0q�6M   $	��Top=�6b   $��75��6z   6�   �� 5   
   -   5         ��Name$ 	txtbx_Dir��5      $
��Left=�6%   $��23��6@   $��
10,275,0,0q�6L    �� TextWrapping$
 Wrap=�6`   $	��Top=�6t   $	��347��6�   �� 5   
   -   5         ��Name$ 
lbl_fehler��5      $��Label��6$   $
��Left=�64   $��522,275,0,0q�6O   $	��Top=�6d   $	��150��6|   $��26��6�   �� 5   
   -   5         ��Name$ cmb_lastdir��5      $
��Left=�6(   $��
10,303,0,0q�6C   $	��Top=�6W   $	��347��6o   �� 5   
   -   5        $ lbl_dircounter��5      $��Label��6(   $
��Left=�68   $��522,301,0,0q�6S   $	��Top=�6h   $	��150��6�   �� 5   
   -	   5        $ btn_save��5      $��Überwachen��6#   $
��Left=�68   $��362,305,0,0q�6S   $	��Top=�6h   $��88��6�    J� RenderTransformOrigin$ 0.071,-0.904&�6�   6�   5      5      