   M S B A M L   `   `   `  ���� F  BFileWatcher, Version=0.0.5.8, Culture=neutral, PublicKeyToken=null)clr-namespace:FileWatcherFileWatcher      FileWatcher.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:FileWatcher     -     �Name$  windows�� � Title$ SystemFileWatcher��$��376.828��$��696.801��.��� -   $��myGrid��$��0,0,0,4q� � RowDefinitions �� $
��Auto �.+��� -    ��Name$ btn_go��$��Start��$
��Left=�$��209,283,0,0q�$	��Top=�$��75���� -   $ btn_beenden��$��Beenden��$
��Left=�$��289,283,0,0q�$	��Top=�$��75���� -    ��Name$ lstview_anzeige��$
��Left=�$	��251��$��	10,28,0,0q�$	��Top=�$	��669�� � 	Resources  �� 
TargetType �� Triggers �� Binding	 �� Value
 �� Property �� Value��%  '��       �� " �� ��  �� *types+	 Changed.a��� 
 � ��
 �  ����  �� *types+	 Created.a��� 
 � ��
 �   ���  �� *types+	 Deleted.a��� 
 � ��
 �   ���  �� *types+	 Renamed.a��� 
 � ��
 ���_� �� View �� .P��� �� -    ��Name$ lbl_counter��$��Label��$
��Left=�$��420,297,0,0q�$	��Top=�$	��136��$��23��$��10(��� -   $ lbl_fwversion��$��Label��$
��Left=�$��420,282,0,0q�$	��Top=�$	��167��$��10(�o� -    o�Name$
 menu��$
��Left=�$��18��$��10,5,0,0q�$	��Top=�$	��669��.@�m� $��Datei��.@�m� $��Überwachungsoptionen��.@�m� -	   $��Ordnerüberwachung��m� $��System��.@�m� -
   $��SetPathMenu��$��SetPath��m� -   $!��FileWatcher Ordner anzeigen��m� -   $�� Log Ordner��.@��� -   $��
lbl_ordner���� -   $��lbl_ordner_clean��.��Log Ordner bereinigen�� -   $��lbl_ShowChangelog��.��Show Changelogm� -   $�� Log Dateien anzeigen��m� -   $#��Infrastruktur Status anzeigen���� �� -    ��Name$ cmb_festplatten��$
��Left=�$��
10,284,0,0q�$	��Top=�$	��189���� -   $ btn_stop��$
��Stop��$
��Left=�$��209,310,0,0q�$	��Top=�$��75���� -   $ lbl_Messages��$��Label��$
��Left=�$��420,312,0,0q�$	��Top=�$	��126��$��10(�