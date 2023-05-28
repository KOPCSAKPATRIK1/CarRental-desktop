using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace CarRental_desktop;

internal class DBhelper
{
    MySqlConnection conn;
    public DBhelper() 
    { 
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        builder.Server = "localhost";
        builder.Port = 3306;
        builder.Database = "cars";
        builder.UserID = "root";
        builder.Password = "";
        conn = new MySqlConnection(builder.ConnectionString);
        conn.Open();
    }
    internal List<Car> ReadCars()
    {
        List<Car> cars = new List<Car>();
        string sql = "select * from cars";
        MySqlCommand command = conn.CreateCommand();
        command.CommandText = sql;
        using (MySqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Car car = new Car(
                    reader.GetInt32("id"),
                    reader.GetString("license_plate_number"),
                    reader.GetString("brand"),
                    reader.GetString("model"),
                    reader.GetInt32("daily_cost"));
                cars.Add(car);
            }
        }

        return cars;
    }

    internal bool UpdateBanned(Car car)
    {
        string sql = "delete from cars where cars.id = @id";
        MySqlCommand command = conn.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("@id", car.id);
        return command.ExecuteNonQuery() == 1;
    }
}