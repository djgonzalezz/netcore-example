using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using pruebadeacero.Common;
using pruebadeacero.IServices;
using pruebadeacero.Models;

namespace pruebadeacero.Services
{
    public class MascotasService : IMascotasService
    {
        public List<Mascota> Get()
        {
            var _mascotas = new List<Mascota>();

            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Accion", 4);

                var mascotas = con.Query<Mascota>(
                    "dbo.sp_Mascotas_pda", 
                    parameters, 
                    commandType: CommandType.StoredProcedure
                    ).ToList();

                if (mascotas != null && mascotas.Count() > 0)
                {
                    _mascotas = mascotas;
                }
            }
            
            return _mascotas;
        }

        public Mascota Save(Mascota mascota)
        {
            var _mascota = new Mascota();

            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                _mascota = con.Query<Mascota>(
                    "dbo.sp_Mascotas_pda", 
                    this.SetParameters(mascota, 1), 
                    commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();
            }
            
            return _mascota;
        }

        public Mascota Update(Mascota mascota)
        {
            var _mascota = new Mascota();

            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                _mascota = con.Query<Mascota>(
                    "dbo.sp_Mascotas_pda", 
                    this.SetParameters(mascota, 2), 
                    commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();
            }
            
            return _mascota;
        }

        public string Delete(int idMascota)
        {
            var _mascota = new Mascota();

            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Accion", 3);
                parameters.Add("@IdMascota", idMascota);

                _mascota = con.Query<Mascota>(
                    "dbo.sp_Mascotas_pda", 
                    parameters, 
                    commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();
            }
            
            return "Se elimino la mascota";
        }

        private DynamicParameters SetParameters(Mascota mascota, int accion)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Accion", accion);
            parameters.Add("@IdMascota", mascota.IdMascota);
            parameters.Add("@Nombre", mascota.Nombre);
            parameters.Add("@Especie", mascota.Especie);
            parameters.Add("@Edad", mascota.Edad);

            return parameters;
        }
    }
}