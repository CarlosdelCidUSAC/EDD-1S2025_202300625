using System;
using Gtk;

class Program
{

        static void Main()
    {
        Application.Init();
        LoginWindow win = new LoginWindow();
        win.ShowAll();
        Application.Run();
    }


}