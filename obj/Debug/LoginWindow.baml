   M S B A M L   `   `   `  ����  	InfoTools    InfoTools.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  	Connexion��5   	   $	��336��6   $	��400��6(    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � ���	#FFF7F9FC.+�5   
   � $��20q�6    � RowDefinitions 5
      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      �� $��images/logo.png��6   $	��166��5      $��80��6   $��0��5      $��	27,0,0,10q�5      $
��Left=�5      $��1��6+   �� 5!      $��Email��6   $��SemiBold"�5"      $��Center=�5#      $
��0,10q�5$      $��1��5%      $��0��6!   �� 5&      -   5         ��Name$ EmailTextBox��5&      $	��220��5'      $��30��6   $	��8,4q�5(      
����������	#FFCBD5E1$��1q�5+      $��10,10q�5,      $��1��5-      $��1��6   �� 50      $��Mot de passe��6   $��SemiBold"�51      $��Center=�52      $
��0,10q�53      $��2��54      $��0��6!   G� 55      -   5         G�Name$ PasswordBox��55      $	��220��56      $��30��6"   $	��8,4q�57      
����������	#FFCBD5E1$��1q�5:      $��10,10q�5;      $��2��5<      $��1��6#   �� 5>      -   5        $��Se connecter��5>      $	��140��5?   	   $��40��6   $��Center=�5@   	   $��3��5A   	   $��2��6   $��0,20,0,0q�5B   	   
�������$
��Bold"�5E   	   ��5F      �� 5G       �� 
TargetType" ��6&   .R�5H      ��  ��Name$ buttonBorder��6!    �� CornerRadius$ 20��5I      ���	#FF1967D2���	#FF1967D2$��0q�5L      	 J� SnapsToDevicePixels	 .    
DropShadow
 J� Effect#
 �   5N      ._�5O      �� $��Center=�6/   $��Center=�5P   #   $
��10,0q�5Q   #   5R       �� Triggers 5S      P� 5T       P� Property �@J�IsMouseOver P� Value . . �5U   "   ��  �� 
TargetName$ buttonBorder��6)    �� Property � �� �� Value �	#FF135BC26Y   5V      5X      5Z       � 	Resources 5^      3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   ShadowDepth   Color   Opacity��%g   &	          5_      $ 10Q�5`      $ 2Q�5a      $ 	#55000000��5b      $	 0.4Q�5c      5d      5e      5f      5g      