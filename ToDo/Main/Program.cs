using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Clases;
using ConsoleTables;

namespace Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Tarea> ListaPendientes = new List<Tarea>();
            List<Tarea> ListaRealizadas = new List<Tarea>();
            int? accion = null;
            do
            {
                // menu principal
                do
                {
                    Console.WriteLine("\n1. Cargar tareas");
                    Console.WriteLine("2. Mostrar tareas");
                    Console.WriteLine("3. Marcar tarea como realizada");
                    Console.WriteLine("4. Buscar tarea con palabra clave");
                    Console.WriteLine("5. Buscar tarea por id");
                    Console.WriteLine("0. Salir");
                    Console.Write("> Accion: ");

                    try
                    {
                        accion = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        if (e is FormatException)
                        {
                            Console.WriteLine("Error!: el valor ingresado debe ser un numero entero\n");
                        }
                        continue;
                    }

                    if (accion < 0 || accion > 5)
                    {
                        Console.WriteLine("Error!: opcion incorrecta\n");
                    }

                } while (accion < 0 || accion > 5 || accion == null);
                // fin menu principal

                switch (accion)
                {
                    case 1:
                        int? cantTareas = null;

                        do
                        {
                            Console.Write("\n> Ingresar la cantidad de tareas que desea cargar (0: Cancelar carga): ");

                            try
                            {
                                cantTareas = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                if (e is FormatException)
                                {
                                    Console.WriteLine("Error!: el valor ingresado debe ser un numero entero\n");
                                }
                                continue;
                            }

                            if (cantTareas < 0)
                            {
                                Console.WriteLine("Error!: el numero intresado debe ser positivo\n");
                            }

                        } while (cantTareas < 0 || cantTareas == null);

                        CargarTareas(cantTareas, ListaPendientes);
                        cantTareas = null;
                        break;

                    case 2:
                        MostrarTareas(ListaPendientes, ListaRealizadas);
                        break;

                    case 3:
                        int? id = null;

                        do
                        {
                            Console.Write("Ingrese el id de la tarea a realizar: ");

                            try
                            {
                                id = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                if (e is FormatException)
                                {
                                    Console.WriteLine("Error!: el valor ingresado debe ser un numero entero\n");
                                }
                            }

                            if (id < 0)
                            {
                                Console.WriteLine("Error!: en numero debe ser positivo\n");
                            }

                            if (id > ListaPendientes.Count)
                            {
                                Console.WriteLine("Error!: no se encuentra la tarea en la lista\n");
                            }
                        } while (id < 0 || id > ListaPendientes.Count || id == null);

                        if (id > 0)
                        {
                            Tarea miTarea = ListaPendientes.Find(tarea => tarea.Id.Equals(id));
                            miTarea.Realizar();
                            ListaPendientes.Remove(miTarea);
                            ListaRealizadas.Add(miTarea);
                        }
                        id = null;
                        break;
                    case 4:
                        string palabraClave = null;

                        do
                        {
                            Console.Write("> Palabra clave: ");
                            palabraClave = Console.ReadLine();
                            if (palabraClave == "")
                            {
                                Console.WriteLine("Error!: ingrese una descripcion\n");
                            }

                        } while (palabraClave == "");

                        MostrarTarea(Helpers.BuscarPalabraClave(ListaPendientes.Concat(ListaRealizadas).ToList(), palabraClave));

                        break;

                    case 5:
                        int? idClave = null;

                        do
                        {
                            Console.Write("ID: ");

                            try
                            {
                                idClave = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                if (e is FormatException)
                                {
                                    Console.WriteLine("Error!: el valor ingresado debe ser un numero entero\n");
                                }
                            }

                            if (idClave < 0)
                            {
                                Console.WriteLine("Error!: en numero debe ser positivo\n");
                            }

                            if (idClave > ListaPendientes.Count && idClave > ListaRealizadas.Count)
                            {
                                Console.WriteLine("Error!: no se encuentra la tarea en la lista\n");
                            }
                        } while (idClave < 0 || (idClave > ListaPendientes.Count && idClave > ListaRealizadas.Count) || idClave == null);

                        MostrarTarea(Helpers.BuscarId(ListaPendientes.Concat(ListaRealizadas).ToList(), idClave));

                        break;
                }
            } while (accion != 0);
            accion = null;
             
        }

        static void CargarTareas(int? cantTareas, List<Tarea> lista)
        {
            string descripcion;
            int duracion;
            if (cantTareas > 0)
            {
                for (int i = 0; i < cantTareas; i++)
                {
                    Console.WriteLine("\n### Cargar tarea nro. " + (lista.Count + 1) + " ###");

                    do
                    {
                        Console.Write("> Descripcion: ");
                        descripcion = Console.ReadLine();
                        if (descripcion == "")
                        {
                            Console.WriteLine("Error!: ingrese una descripcion\n");
                        }
                    
                    } while (descripcion == "");

                    duracion = 0;
                    do
                    {
                        Console.Write("> Duracion: ");

                        try
                        {
                            duracion = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            if (e is FormatException)
                            {
                                Console.WriteLine("Error!: el valor ingresado debe ser un numero entero\n");
                            }
                            continue;
                        }

                        if (duracion <= 0)
                        {
                            Console.WriteLine("Error!: ingrese un valor positivo\n");
                        }
                    } while (duracion <= 0);
                   
                    lista.Add(new Tarea(lista.Count + 1, descripcion, duracion));
                }
            }
        }

        static void MostrarTareas(List<Tarea> listaPendientes, List<Tarea> listaRealizadas)
        {
            ConsoleTable tabla = new ConsoleTable("id", "Descripcion", "Duracion", "Estado");

            foreach(Tarea tarea in listaPendientes)
            {
                tabla.AddRow(tarea.Id, tarea.Descripcion, tarea.Duracion, tarea.Estado);
            }
            foreach (Tarea tarea in listaRealizadas)
            {
                tabla.AddRow(tarea.Id, tarea.Descripcion, tarea.Duracion, tarea.Estado);
            }
            Console.WriteLine();
            tabla.Write();
        }

        static void MostrarTarea(Tarea tarea)
        {
            ConsoleTable tabla = new ConsoleTable("id", "Descripcion", "Duracion", "Estado");
            tabla.AddRow(tarea.Id, tarea.Descripcion, tarea.Duracion, tarea.Estado);
            Console.WriteLine();
            tabla.Write();
        }
    }
}
