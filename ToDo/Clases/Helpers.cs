using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Clases
{
    static public class Helpers
    {
        static public Tarea BuscarId(List<Tarea> lista, int? id)
        {
            Tarea tarea = lista.Find(x => x.Id.Equals(id));
            return tarea;
        }

        static public Tarea BuscarPalabraClave(List<Tarea> lista, string palabra)
        {
            Tarea tarea = lista.Find(x => x.Descripcion.Contains(palabra));
            return tarea;
        }
    }
}
