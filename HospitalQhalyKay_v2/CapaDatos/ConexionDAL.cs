using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CapaDatos
{
    public class ConexionDAL
    {
        public string cadenaConexion { get; set; }
        public ConexionDAL()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            cadenaConexion = root.GetConnectionString("cn");
        }
    }
}
