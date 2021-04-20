using Ejercicio04Tema07POO.BL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ejercicio04Tema07POO.DL
{
    public class RepositorioElipses
    {
        public List<Elipse> Elipses { get; set; } = new List<Elipse>();
        private readonly string _archivo = Environment.CurrentDirectory + "\\Elipses.txt";
        private readonly string _archivoBak = Environment.CurrentDirectory + "\\Elipses.bak";

        public RepositorioElipses()
        {
            LeerDatosDelArchivo();
        }

        private void LeerDatosDelArchivo()
        {
            StreamReader lector = new StreamReader(_archivo);
            while (!lector.EndOfStream)
            {
                string linea = lector.ReadLine();
                Elipse elipse = ConstruirElipse(linea);
                Elipses.Add(elipse);
            }
            lector.Close();
        }

        private Elipse ConstruirElipse(string linea)
        {
            var campos = linea.Split(';');
            return new Elipse
            {
                SemiejeMayor = double.Parse(campos[0]),
                SemiejeMenor = double.Parse(campos[1])
            };
        }



        public void Agregar(Elipse elipse)
        {
            Elipses.Add(elipse);
            GuardarRegistro(elipse);
        }

        private void GuardarRegistro(Elipse elipse)
        {
            StreamWriter escritor = new StreamWriter(_archivo, true);
            string linea = ConstruirLinea(elipse);
            escritor.WriteLine(linea);
            escritor.Close();
        }

        private string ConstruirLinea(Elipse elipse)
        {
            return $"{elipse.SemiejeMayor};{elipse.SemiejeMenor}";
        }

        public void Borrar(Elipse elipse)
        {
            Elipses.Remove(elipse);
            BorrarRegistroDelArchivo(elipse);
        }

        private void BorrarRegistroDelArchivo(Elipse elipse)
        {
            StreamReader lector = new StreamReader(_archivo);
            StreamWriter escritor = new StreamWriter(_archivoBak);
            while (!lector.EndOfStream)
            {
                string linea = lector.ReadLine();
                Elipse elipseEnArchivo = ConstruirElipse(linea);
                if (!elipseEnArchivo.Equals(elipse))
                {
                    escritor.WriteLine(linea);  
                }
            }
            lector.Close();
            escritor.Close();
            File.Delete(_archivo);
            File.Move(_archivoBak,_archivo);
        }

        public void Editar(Elipse elipseEditada,Elipse elipseaBuscar)
        {
            StreamReader lector = new StreamReader(_archivo);
            StreamWriter escritor = new StreamWriter(_archivoBak);
            while (!lector.EndOfStream)
            {
                string linea = lector.ReadLine();
                Elipse elipseEnArchivo = ConstruirElipse(linea);
                if (elipseEnArchivo.Equals(elipseaBuscar))
                {
                    linea = ConstruirLinea(elipseEditada);
                }
                escritor.WriteLine(linea);
            }
            lector.Close();
            escritor.Close();
            File.Delete(_archivo);
            File.Move(_archivoBak, _archivo);
        }
        public int GetCantidad()
        {
            return Elipses.Count;
        }
        public List<Elipse> GetLista()
        {
            return Elipses;
        }
        public Elipse GetElipse(int posicion)
        {
            return Elipses[posicion];
        }

        public List<Elipse> OrdenarDeMenoraMayor()
        {
            return Elipses.OrderBy(t => t.SemiejeMayor).ToList();
        }

        public List<Elipse> OrdenarDeMayoraMenor()
        {
            return Elipses.OrderByDescending(t => t.SemiejeMayor).ToList();
        }
    }
}
