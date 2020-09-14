using System;

namespace Clases
{
    public enum EstadoTarea
    {
        Pendiente,
        Realizada
    }
    public class Tarea
    {
        private int id;
        private string descripcion;
        private int duracion;
        private EstadoTarea estado;

        public int Id { get => id; set => id = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Duracion { get => duracion; set => duracion = value; }
        public EstadoTarea Estado { get => estado; set => estado = value; }



        public Tarea(int id, string descripcion, int duracion)
        {
            Id = id;
            Descripcion = descripcion;
            Duracion = duracion;
            Estado = EstadoTarea.Pendiente;
        }

        public void Realizar()
        {
            Estado = EstadoTarea.Realizada;
        }
        
    }
}
