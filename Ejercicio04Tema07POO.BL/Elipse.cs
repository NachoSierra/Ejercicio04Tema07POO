using System;

namespace Ejercicio04Tema07POO.BL
{
    public class Elipse:ICloneable
    {
        public double SemiejeMayor { get; set; }
        public double SemiejeMenor { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public double GetArea()
        {
            return Math.PI * SemiejeMayor * SemiejeMenor;
        }
        public double GetPerimetro()
        {
            return 2 * Math.PI * Math.Sqrt(Math.Pow(SemiejeMayor, 2) + Math.Pow(SemiejeMenor, 2) / 2);
        }
        public bool Validar()
        {
            bool esValido = true;
            if (SemiejeMayor>15||SemiejeMayor<0||SemiejeMenor>15||SemiejeMenor<0)
            {
                esValido = false;
            }
            return esValido;
        }
        public override bool Equals(object obj)
        {
            if (obj==null||!(obj is Elipse))
            {
                return false;
            }
            return this.SemiejeMayor == ((Elipse)obj).SemiejeMayor && this.SemiejeMenor == ((Elipse)obj).SemiejeMenor;
        }
        public override int GetHashCode()
        {
            return this.SemiejeMayor.GetHashCode() + this.SemiejeMenor.GetHashCode();
        }
    }
}
