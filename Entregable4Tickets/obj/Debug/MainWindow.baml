   M S B A M L   `   `   `  ����M  IEntregable4Tickets, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Entregable4TicketsEntregable4Tickets  5   	   #    Entregable4Tickets.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Entregable4Tickets     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      ]� .+�5
   
   � -   5        $��GDebug��5
   '   $��Bottom=�6   $y�	Collapsed=�65    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6     � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��0��6!   .��61   Texto reconocido:6E   �� 5      -   5        $��labelTextoReconocido��5      $��0��60   $��1��6=   �� 5      $��1��6   $��0��6!   .��61   Probabilidad:6B   �� 5      -   5        $��labelProbabilidad��5      $��1��6-   $��1��6:   5      o� 5   
   $	��Top=�6   .@�5      m� -   5        $��MenuOpciones��5   *   $��	_Opciones��6   .@�5      m� -   5        $��bDebug��5   7   $��_Modo Debug��5       m� IsCheckable . 5   7   5      5      � 5    
    5!      �� 5"      $
��auto �6    �� 5#      $
��auto �6     5%      �� 5&      $��* �6#   .+�5(      A�  A� Columns$ 4��6    A� Rows$ 1��6&   
�������$��10q�6I   .+�5)      �� $��30(�6   .��6&   Desde:6.   �� 5*      -   5        $��Lfrom��5+   U   $��24(�5*      $
��Left=�6&   $
��Left=�6H   $��Center=�5+      $��Center=�63   .��6b   
Cualquiera6n   �� 5,      $��30(�6   .��6&   Hasta:6.   �� 5-      -   5        $	��Lto��5.   U   $��24(�5-      $
��Left=�6&   $
��Left=�6H   $��Center=�5.      $��Center=�63   .��6`   
Cualquiera6l   5/      A� 51      $ 2��6   $ 2��6&   $��1��6/   
�������$��10q�6S   .+�52      �� $��26(�6   $��	200,0,0,0q�6&   .��69   	Cantidad:6D   �� 53      -   5        $��	Lntickets��54   U   $��20(�53      $
��Left=�6&   $
��Left=�6H   $��Center=�54      $��Center=�63   $��30,0,0,0q�55      .��6*   06-   �� 56      $��26(�6   $��	200,0,0,0q�6&   .��69   Dia:6?   �� 57      -	   5        $
��Lday��58   U   $��20(�57      $
��Left=�6&   $
��Left=�6H   $��Center=�58      $��Center=�63   $��30,0,0,0q�59      .��6*   Cualquier día69   5:      5;      5=      5>      