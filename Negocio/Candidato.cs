using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2.Negocio
{
    public class Candidato
    {
        private int idCandidato;
        private string nombre;
        private int partido;
        private int cargo;
        private DateTime fechaNacimiento;

        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }

        public int Cargo
        {
            get { return cargo; }
            set { cargo = value; }
        }

        public int Partido
        {
            get { return partido; }
            set { partido = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int IdCandidato
        {
            get { return idCandidato; }
            set { idCandidato = value; }
        }

        public override string ToString()
        {
            return idCandidato + " - " + nombre;
        }
    }
}
