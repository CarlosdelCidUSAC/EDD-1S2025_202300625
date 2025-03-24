using System;
using Gtk;

class Program
{

        static void Main()
    {
        Application.Init();
        LoginWindow win = new LoginWindow();
        win.ShowAll();
        MenuAdminWindow win2 = new MenuAdminWindow();
        win2.ShowAll();
        MenuUsuarioWindow win3 = new MenuUsuarioWindow();
        win3.ShowAll();
        Application.Run();
    }


}