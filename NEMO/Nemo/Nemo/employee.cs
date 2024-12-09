using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemo
{
    internal class employee
    {
        #region Propriétés
        private int idEmp;
        private string nomEmp;
        private string prenomEmp;
        private string typeEmp;
        private string mailEmp;
        private decimal telEmp;
        #endregion

        public int IdEmp
        {
            get { return idEmp; }
            set { idEmp = value; }
        }

        public string NomEmp
        {
            get { return nomEmp; }
            set { nomEmp = value; }
        }

        public string PrenomEmp
        {
            get { return prenomEmp; }
            set { prenomEmp = value; }
        }

        public string TypeEmp
        {
            get { return typeEmp; }
            set { typeEmp = value; }
        }

        public string MailEmp
        {
            get { return mailEmp; }
            set { mailEmp = value; }
        }

        public decimal TelEmp
        {
            get { return telEmp; }
            set { telEmp = value; }
        }


        public employee(int idEmp, string nomEmp, string prenomEmp, string typeEmp, string mailEmp, decimal telEmp)
        {
            this.idEmp = idEmp;
            this.nomEmp = nomEmp;
            this.prenomEmp = prenomEmp;
            this.typeEmp = typeEmp;
            this.mailEmp = mailEmp;
            this.telEmp = telEmp;
        }

        public override string ToString()
        {
            return NomEmp + " " + PrenomEmp;
        }
    }
}
