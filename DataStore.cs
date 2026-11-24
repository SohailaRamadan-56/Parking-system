using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Parking_System
{
    public class Vehicle
    {
        public string Plate { get; set; } = "";
        public string Owner { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Type { get; set; } = "Car";
    }

    public class Slot
    {
        public string Number { get; set; } = "";
        public string Zone { get; set; } = "A";
        public string Status { get; set; } = "Free";
    }

    public class Ticket
    {
        public int Id { get; set; }
        public string Plate { get; set; } = "";
        public string SlotNumber { get; set; } = "";
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public decimal Fee { get; set; }
        public bool Paid { get; set; }
    }

    public class ParkingData
    {
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public List<Slot> Slots { get; set; } = new List<Slot>();
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }

    public static class DataStore
    {
        public static List<Vehicle> Vehicles = new List<Vehicle>
        {
            new Vehicle { Plate = "ABC 1234", Owner = "Ahmed Ali", Phone = "01012345678", Type = "Car" },
            new Vehicle { Plate = "XYZ 5678", Owner = "Sara Hassan", Phone = "01198765432", Type = "Motorbike" },
            new Vehicle { Plate = "KLM 9012", Owner = "Omar Khaled", Phone = "01234567890", Type = "Car" },
        };

        public static List<Slot> Slots = new List<Slot>();

        public static List<Ticket> Tickets = new List<Ticket>();

        private static string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Parking_System",
            "parkingData.json"
        );

        static DataStore()
        {
            foreach (string zone in new[] { "A", "B" })
            {
                for (int i = 1; i <= 12; i++)
                {
                    Slots.Add(new Slot
                    {
                        Number = zone + "-" + i.ToString("00"),
                        Zone = zone
                    });
                }
            }
        }

        public static void SaveData()
        {
            string folder = Path.GetDirectoryName(filePath)!;
            Directory.CreateDirectory(folder);

            ParkingData data = new ParkingData
            {
                Vehicles = Vehicles,
                Slots = Slots,
                Tickets = Tickets
            };

            string json = JsonSerializer.Serialize(
                data,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(filePath, json);
        }

        public static void LoadData()
        {
            if (!File.Exists(filePath))
            {
                SaveData();
                return;
            }

            string json = File.ReadAllText(filePath);

            ParkingData? data = JsonSerializer.Deserialize<ParkingData>(json);

            if (data != null)
            {
                Vehicles = data.Vehicles ?? new List<Vehicle>();
                Slots = data.Slots ?? new List<Slot>();
                Tickets = data.Tickets ?? new List<Ticket>();
            }
        }
    }
}