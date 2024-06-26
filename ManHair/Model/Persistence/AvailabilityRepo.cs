﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.Model.Persistence
{
    public class AvailabilityRepo
    {
        private string? connectionString;
        private List<Availability> availabilityList = new List<Availability>();
        public AvailabilityRepo() 
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            connectionString = config.GetConnectionString("MyDBConnection");
        }

        // Load all Costumers from database and polulating into CostumerList

        public List<Availability> LoadAllAvailabilities()
        {
            availabilityList.Clear();
            try
            {
                //before we can access the database we have to connect to the database, here we use SqlConnection object and refer it to the connectionstring
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Now Connection is open and we can run a Query on the database
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT Date,Time FROM Availability", connection))
                    {
                        SqlDataReader dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            DateTime Date = dataReader.GetDateTime(0).Date;
                            DateOnly date = DateOnly.FromDateTime(Date);
                            TimeSpan Time = (dataReader.GetTimeSpan(1));
                            TimeOnly time = TimeOnly.FromTimeSpan(Time);
                            Availability availability = new Availability(date, time);
                            availabilityList.Add(availability);
                        }

                    }

                }

            }
            catch (SqlException ex)
            {

                throw new Exception("An error occured while trying to fetch data from the database");
            }

            return availabilityList;    
        }

        public List<Availability> GetAvailabilities(DateOnly date)
        {
           List<Availability> filteredAvailibilty = new List<Availability>();

            if (filteredAvailibilty.Count() < 0)
            {
                filteredAvailibilty.Clear();
            }
            
            List<Availability> availabilities = LoadAllAvailabilities();
            filteredAvailibilty = availabilities.Where(availability => availability.Date == date)
                    .OrderBy(availability => availability.Time).ToList();
            
            return filteredAvailibilty;
        }
        public void AddAvailability(DateOnly date, TimeOnly time)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);

                    using (SqlCommand command = new SqlCommand("INSERT INTO Availability (Date, Time)"
                        + " VALUES(@date, @time)", sqlConnection))
                    {
                        command.Parameters.Add("@date", SqlDbType.Date).Value = dateTime.Date;
                        command.Parameters.Add("@time", SqlDbType.Time).Value = dateTime.TimeOfDay;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {

                throw new Exception("Something went wrong when trying to add a new use to the DB" + e);
            }
        }
        public void RemoveAvailability(DateOnly date, TimeOnly time)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);

                    using (SqlCommand command = new SqlCommand("DELETE FROM Availability WHERE Date = @date AND Time = @time", sqlConnection))
                    {
                        command.Parameters.Add("@date", SqlDbType.Date).Value = dateTime.Date;
                        command.Parameters.Add("@time", SqlDbType.Time).Value = dateTime.TimeOfDay;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {

                throw new Exception("Something went wrong when trying to remove a use to the DB" + e);
            }
        }
    }
}
