using Parking_System;

namespace Parking_System.Froms;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        DataStore.LoadData();

        Application.Run(new LoginForm());
    }
}