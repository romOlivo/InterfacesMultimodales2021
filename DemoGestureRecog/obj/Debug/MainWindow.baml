   M S B A M L   `   `   `  ����K  GDemoGestureRecog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DemoGestureRecogDemoGestureRecog  5   	   !    DemoGestureRecog.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:DemoGestureRecog     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      ]� .+�5   
   o� $	��Top=�6   .@�5      m� $��_File��6   .@�5      m� -   5        $��_Save��5      6*   m� 5      -   5        $��_Load��5      6*   m� 5      -   5        $��_Import��5      6,   5      5      � 5   
   $��Bottom=�6    � RowDefinitions 5      �� 5      $��* �6    6-   �� 5      $
��auto �6    60   �� 5      $
��auto �6    60   .+�5      � $��8q�6    5      �� 5      $
��auto �6$   64   �� 5      $��* �6$   61   �� 5      $
��auto �6$   64    � ColumnDefinitions 5!      �� 5"      $��* �6'   63   �� 5#      $
��auto �6'   66   .+�5&      �� .��6   
Prototypes6&   �� 5'      -   5        $��etiquetasLB��5'      $��1��6-   $��2��6:   $��4q�6N   
���   �5(      �� 5)      -   5        $��NombreTB��5)      $��2��6*   $��4q�67   .��6B   6E   �� 5*      -   5        $
��AddB��5+      $��2��5*      $��1��6&   $��4q�66   $	��5cm��6A   {�.  .��5+   L   Add5-      5.      �� 50      -   5        $��SalidaL��50      $��8q�6#   $��1��6.   $��Stretch=�51      $��Center=�62   
���   �$��1q�52   (   $��40(�53      
��� � �.��65   	Nothing6@   �� 55      $��8q�6   $��2��6$    �� Orientation$ 
Horizontal=�61   .+�56      	� -   5        $��TrainB��56      $��8q�6,    d� 	IsChecked$
 TrueY�6J   .��6[   
Training6g   	� 57      -	   5        $��RecogB��57      $��8q�6,   {�.  .��6\   Recognition6k   �� 58      -
   5        $��StartB��58      $��8q�6'   $	��3cm��62   {�.  .��59   +   Start64   5:      5<      5>      5?      