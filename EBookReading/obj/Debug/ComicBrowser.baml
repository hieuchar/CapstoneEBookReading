   M S B A M L   `   `   `  ����G  CEBookReading, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:EBookReadingEBookReading  5   	       EBookReading.ComicBrowser   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4CustomCommandclr-namespace:EBookReading     5   	     � Title$  Comic Browser��5   	   $	��825��6   $	��700��6,    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
   
   -   5           Command$% CustomCommand:Commands.NextPage��5
      6k     5   
   -   5        $% CustomCommand:Commands.PrevPage��5      6k   .��5      �� .+�5   
   � $��35��6    � ColumnDefinitions 5      �� 5      6%   �� 5      6%   �� 5      6%   .+�5      �� -   5        $��PrevPage��5   L   $��0q�6   
�������$��0��6<   .��5      �� -   5        $��	LeftArrow��5      5      5      �� 5      -   5        $��
PageNumber��5   a   $��Center=�6   $��Center=�64   $��1��6Q    �� Orientation$ 
Horizontal=�6s   .+�5      �� -   5        $��CurrentPageBox��5   g   $��0q�6   $��16(�6.   6�   �� 5      -   5        $��
TotalPages��5   N   $��0q�6   $��16(�6.    �� 
IsReadOnly . 6b   5      �� 5      -   5        $��NextPage��5   L   $��0q�6   
�������$��2��6<   .��5      �� -	   5        $��
RightArrow��5      5       5!      5"      �� 5#   
   -
   5        $��ImageContent��5#   /   $	��750��6   $	��675��6    $
��Fill=�6C   5$      5%      5&      