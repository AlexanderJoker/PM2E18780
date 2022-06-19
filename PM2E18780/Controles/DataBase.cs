
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PM2E18780.Models;

namespace PM2E18780.Controles
{
    public class DataBase
    {
        readonly SQLiteAsyncConnection db;

        // Constructor de clase vacio
        public DataBase() { }

        // Constructor con parametros
        public DataBase(String pathbasedatos)
        {
            db = new SQLiteAsyncConnection(pathbasedatos);

            // CREATE TABLE UbicacionesDB
            db.CreateTableAsync<Ubicaciones>();
        }

        // Funcion para retornar la tabla como lista
        public Task<List<Ubicaciones>> listaubicaciones()
        {
            return db.Table<Ubicaciones>().ToListAsync();
        }

        // Obtener ubicacion por ID
        public Task<Ubicaciones> ObtenerUbicacion(int uId)
        {
            return db.Table<Ubicaciones>()
                .Where(i => i.Id == uId)
                .FirstOrDefaultAsync();
        }

        // Obtener Latitud de UbicacionesDB
        public Task<Ubicaciones> ObtenerLongitud(float uLongitud)
        {
            return db.Table<Ubicaciones>().Where(i => i.Longitud == uLongitud).FirstOrDefaultAsync();
        }

        // Obtener Latitud de UbicacionesDB
        public Task<Ubicaciones> ObtenerLatitud(float uLatitud)
        {
            return db.Table<Ubicaciones>().Where(i => i.Latitud == uLatitud).FirstOrDefaultAsync();
        }

        // Obtener Descripcion de UbicacionesDB
        public Task<Ubicaciones> ObtenerDescripcion(String uDescripcion)
        {
            return db.Table<Ubicaciones>().Where(i => i.Descripcion == uDescripcion).FirstOrDefaultAsync();
        }

        // INSERT-UPDATE UbicacionesDB
        public Task<Int32> GuardarUbicacion(Ubicaciones ubicacion)
        {
            if (ubicacion.Id != 0)
            {
                return db.UpdateAsync(ubicacion);
            }
            else
            {
                return db.InsertAsync(ubicacion);
            }
        }

        // Eliminar ubicacion
        public Task<Int32> EliminarUbicacion(Ubicaciones ubicacion)
        {
            return db.DeleteAsync(ubicacion);
        }

        // Pasar imagen de Byte a Stream
        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}

