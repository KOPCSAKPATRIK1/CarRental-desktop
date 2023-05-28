using K4os.Compression.LZ4.Internal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_desktop;

public static class Statisztika
{
    static List<Car> cars;
    public static void Run()
    {
        try
        {
            ReadCars();
            Console.WriteLine($"20.000 Ft-nál olcsóbb napidíjú autók száma: {Under20()}");
            Console.WriteLine(Above26());
            var mostExpensive = Expensive();
            Console.WriteLine($"Legdrágább napidíjú autó: " +
                $"{mostExpensive.license_plate_number} - " +
                $"{mostExpensive.brand} - " +
                $"{mostExpensive.model} - " +
                $"{mostExpensive.daily_cost}");
            Brands();
            Console.WriteLine("Adjon meg egy rendszámot:");
            Console.WriteLine(FindCar(Console.ReadLine()));
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Hiba a kapcsolat kialakitasakor");
            Console.WriteLine(ex);
        }
       
    }

    private static void ReadCars()
    {
        DBhelper db = new DBhelper();
        cars = db.ReadCars();
    }

    private static int Under20()
    {
       return cars.Where(c => c.daily_cost < 20000).Count();
    }

    private static string Above26()
    {
        if (cars.Where(c => c.daily_cost > 26000).FirstOrDefault() != null)
        {
            return "Van az adatok között 26.000 Ft-nál drágább napidíjú autó";
        }
        else
        {
            return "Nincs az adatok között 26.000 Ft-nál drágább napidíjú autó";
        }
    }

    private static Car Expensive()
    {
        return (Car)cars.OrderBy(c => c.daily_cost).LastOrDefault();
    }

    private static void Brands()
    {
        var brands = cars.GroupBy(c => c.brand).ToDictionary(b => b.Key, b => b.Count());
        foreach (var brand in brands)
        {
            Console.WriteLine($"{brand.Key}: {brand.Value}");
        }
    }

    private static string FindCar(string? plate)
    {
        var car = cars.Where(c => c.license_plate_number == plate).FirstOrDefault();
        if (car != null)
        {
            if (car.daily_cost > 25000)
            {
                return "A megadott autó napidíja nem nagyobb mint 25.000 Ft";
            }
            else
            {
                return "A megadott autó napidíja nagyobb mint 25.000 Ft";
            }
        }
        else
        {
            return "Nincs ilyen auto";
        }
    }
}
