using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Newtonsoft.Json;
using pruebadeacero.Common;
using pruebadeacero.IServices;
using pruebadeacero.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace pruebadeacero.Services
{
    public class CatClienteService : ICatClienteService
    {
        public List<CatCliente> GetList()
        {
            var _catClientes = new List<CatCliente>();

            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                var catClientes = con.Query<CatCliente>("dbo.sp_GetCatClientes_jaciel", commandType: CommandType.StoredProcedure).ToList();

                if (catClientes != null && catClientes.Count() > 0)
                {
                    _catClientes = catClientes;
                }
            }
            
            return _catClientes;
        }

        public CatCliente GetCatCliente(int claCliente)
        {
            var catCliente = new CatCliente();

            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                catCliente = con.Query<CatCliente>(
                    "select ClaCliente, NomCliente, ApiKey, Descripcion from KraSch.KraCatCliente where ClaCliente = " + 
                    claCliente.ToString()).FirstOrDefault();
            }
            
            return catCliente;
        }

        public List<dynamic> GetCatClienteUsers()
        {
            var clienteUsers = new List<dynamic>();

            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                clienteUsers = con.Query<dynamic>(
                    "select b.ClaUsuario, a.NomCliente " +
                    "from KraSch.KraCatCliente a " +
                    "join KraSch.KraRelClienteUsuario b on a.ClaCliente = b.ClaCliente ").ToList();
            }
            
            return clienteUsers;
        }

        public async Task<string> CallApi()
        {
            var request = "{\"columnas\": \"ClaUsuario, NomUsuario, Email\",\"condicion\": \"NomUsuario LIKE '%Arturo %' ORDER BY NomUsuario ASC\",\"tipoEstructura\": 1}";
            var content = new StringContent(
                request, Encoding.UTF8,
                "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-access-token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJDbGFVc3VhcmlvIjoiMTAwMDE3MTE3IiwiTm9tYnJlVXN1YXJpbyI6IkRBUklPIEpBQ0lFTCBHT05aQUxFWiBPVEVSTyIsIkVtYWlsIjoiZGpnb256YWxlekBkZWFjZXJvLmNvbSIsIk5vbWJyZVBjIjoiMTcyLjI1LjEyNy4yNTAiLCJpYXQiOjE1OTgzMDE4NDAsImV4cCI6MTU5ODMwOTA0MH0.51SmZQUTIAcSbEOBoEwMG8izNGEMZMXENrAVDJRHqeY");
            client.DefaultRequestHeaders.Add("x-api-key", "A70CEE06-5784-4085-BC46-05D2E986D568");

            client.BaseAddress = new Uri("http://dlabsdbdev:2022");

            var response = await client.PostAsync("/KrakenServices/GetEntityData/7", content);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return result;
            }
            else
            {
                return "error";
            }
        }
    }
}