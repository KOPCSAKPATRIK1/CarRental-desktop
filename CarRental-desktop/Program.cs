using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CarRental_desktop;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        if(args.Contains("--stat"))
        {
            Statisztika.Run();
        }
        else
        {
            Application application = new Application();
            application.Run(new MainWindow());
        }
    }    
}
