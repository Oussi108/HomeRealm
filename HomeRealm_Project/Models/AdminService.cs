using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace HomeRealm_Project.Models
{
    public class AdminService
    {
        private readonly string _adminsFilePath;

        public AdminService(string adminsFilePath)
        {
            _adminsFilePath = adminsFilePath;
        }

        public bool AdminExists(string email, string password)
        {
            // Read the JSON file and deserialize the admin data
            AdminData adminData = ReadAdminData();

            // Check if an admin user with the specified email and password exists
            bool adminExists = adminData.adminUsers.Exists(admin => admin.email == email && admin.password == password);

            return adminExists;
        }


        private AdminData ReadAdminData()
        {
            // Read the JSON file
            string json = File.ReadAllText(_adminsFilePath);

            // Deserialize the JSON into an instance of AdminData
            AdminData adminData = JsonConvert.DeserializeObject<AdminData>(json);

            return adminData;
        }

    }
}
