   M S B A M L   `   `   `  ����F  BFileWatcher, Version=0.0.5.8, Culture=neutral, PublicKeyToken=null)clr-namespace:FileWatcherFileWatcher  5   	       FileWatcher.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:FileWatcher     5   	   -   5          �Name$  windows��5   	    � Title$ SystemFileWatcher��5   	   $��376.828��6#   $��696.801��64   .��5	      � -   5        $��myGrid��5	      $��0,0,0,4q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   5      .+�5   
   �� -   5         ��Name$ btn_go��5      $��Start��6!   $
��Left=�61   $��209,283,0,0q�6L   $	��Top=�6a   $��75��6y   6�   �� 5   
   -   5        $ btn_beenden��5      $��Beenden��6&   $
��Left=�68   $��289,283,0,0q�6S   $	��Top=�6h   $��75��6�   6�   �� 5   
   -   5         ��Name$ lstview_anzeige��5      $
��Left=�6,   $	��251��6G   $��	10,28,0,0q�6T   $	��Top=�6g   $	��669��6    � 	Resources 5       �� 
TargetType �� Triggers �� Binding	 �� Value
 �� Property �� Value��%�  '��       �� 5      " ��6    5      �� 5       6&   �� *types+	 Changed6@   .a�5      �� 
 � ��
 �  ��5      5      �� 5       6&   �� *types+	 Created6@   .a�5      �� 
 � ��
 �   �5      5      �� 5       6&   �� *types+	 Deleted6@   .a�5      �� 
 � ��
 �   �5!      5"      �� 5#       6&   �� *types+	 Renamed6@   .a�5$      �� 
 � ��
 ���_�5&      5'      5)      5*       �� View 5+      �� 5-      .P�5.      �� 5/      51      �� 52   
   -   5         ��Name$ lbl_counter��52      $��Label��6%   $
��Left=�65   $��420,297,0,0q�6P   $	��Top=�6e   $	��136��6}   $��23��6�   $��10(�6�   �� 53   
   -   5        $ lbl_fwversion��53      $��Label��6'   $
��Left=�67   $��420,282,0,0q�6R   $	��Top=�6g   $	��167��6   $��10(�6�   o� 54   
   -   5         o�Name$
 menu��54      $
��Left=�6   $��18��68   $��10,5,0,0q�6D   $	��Top=�6V   $	��669��6n   .@�55      m� $��Datei��6   .@�56      m� $��Überwachungsoptionen��6   .@�57      m� -	   5        $��Ordnerüberwachung��57      58      5;      m� 5<      $��System��6   .@�5=      m� -
   5        $��SetPathMenu��5=   F   $��SetPath��6   6[   m� 5>      -   5        $!��FileWatcher Ordner anzeigen��5>      6Y   m� 5?      -   5        $�� Log Ordner��5?      .@�5@      �� -   5        $��
lbl_ordner��5@       64   �� 5A      -   5        $��lbl_ordner_clean��5A       .��6_   Log Ordner bereinigen6v   �� 5B      -   5        $��lbl_ShowChangelog��5B       .��6a   Show Changelog6q   5C      �� 5D      -   5        $��lbl_Settings��5D      .��6S   Show FileWatcher Settings6n   m� 5F      -   5        $�� Log Dateien anzeigen��5F      5H      m� 5I      -   5        $#��Infrastruktur Status anzeigen��5I      6Z   5J      5K      �� 5L      6   5M      �� 5N   
   -   5         ��Name$ cmb_festplatten��5N      $
��Left=�6,   $��
10,284,0,0q�6G   $	��Top=�6[   $	��189��6s   6   �� 5O   
   -   5        $ btn_stop��5O      $
��Stop��6#   $
��Left=�62   $��209,310,0,0q�6M   $	��Top=�6b   $��75��6z   6�   �� 5P   
   -   5        $ lbl_Messages��5P      $��Label��6&   $
��Left=�66   $��420,312,0,0q�6Q   $	��Top=�6f   $	��126��6~   $��10(�6�   5Q      5R      