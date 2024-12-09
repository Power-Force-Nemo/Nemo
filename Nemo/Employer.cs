using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemo
{
    internal class Employer
    {
        #region Champs
        private int idEmp;
		private string nomEmp;
		private string prenomEmp;
		private string typeEmp;
		private string mailEmp;
		private decimal telEmp;
        #endregion

        #region Accesseurs/Mutateurs
        public int IDEmp
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
        #endregion

        #region Constructeurs
        public Employer(int _idEmp, string _nomEmp, string _prenomEmp, string _typeEmp, string _mailEmp, decimal _telEmp)
        {
            idEmp = _idEmp;
            nomEmp = _nomEmp;
            prenomEmp = _prenomEmp;
            typeEmp = _typeEmp;
            mailEmp = _mailEmp;
            telEmp = _telEmp;
        }
        #endregion
    }
}
