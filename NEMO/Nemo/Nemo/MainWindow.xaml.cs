using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Nemo
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Employer> lesEmployee = new List<Employer>();

        public MainWindow()
        {
            bdd.Initialize();
            InitializeComponent();

            lesEmployee = bdd.SelectEmployer();

            dtgEmp.ItemsSource = lesEmployee;

            dtgEmp.SelectedIndex = 0;

            cboTypeEmp.ItemsSource = lesEmployee;
            cboTypeEmp.SelectedValuePath = "IdEmp";
        }


        // ------------- Employer --------------
        private void dtgEmp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             Employer selectedEmp = dtgEmp.SelectedItem as Employer;
            if (dtgEmp.SelectedItem != null)
            {
                try
                {
                    txtNumEmp.Text = Convert.ToString(selectedEmp.IDEmp);
                    txtNomEmp.Text = selectedEmp.NomEmp;
                    txtPrenomEmp.Text = Convert.ToString(selectedEmp.PrenomEmp);
                    cboTypeEmp.SelectedIndex = Convert.ToInt16(selectedEmp.TypeEmp);
                    txtMailEmp.Text = Convert.ToString(selectedEmp.MailEmp);
                    txtTelEmp.Text = Convert.ToString(selectedEmp.TelEmp);
                }
                catch (Exception)
                {

                    Console.WriteLine("Erreur sur la mise à jour du formulaire lors du changement dans le Datagrid dtgContrat");
                }
            }
        }

        private void btnAjouterEmp_Click(object sender, RoutedEventArgs e)
        {
            if (dtgEmp.SelectedIndex >= 0)
            {
                Employer tmpEmp = new Employer(0, txtNomEmp.Text, txtPrenomEmp.Text, cboTypeEmp.Text, txtMailEmp.Text, Convert.ToDecimal(txtTelEmp.Text));
                bdd.InsertEmployer(tmpEmp.IDEmp, tmpEmp.NomEmp, tmpEmp.PrenomEmp, tmpEmp.TypeEmp, tmpEmp.MailEmp, tmpEmp.TelEmp);

                // Mets a jours pigistes
                lesEmployee = bdd.SelectEmployer();

                // Met à jour le DataGrid
                dtgEmp.ItemsSource = lesEmployee;
                dtgEmp.SelectedIndex = 0;
                dtgEmp.Items.Refresh();
            }
        }

        private void btnModifierEmp_Click(object sender, RoutedEventArgs e)
        {
            //On recherche à quel indice de la collection se trouve l'object selectionné dans le datagrid
            int indice = lesEmployee.IndexOf((Employer)dtgEmp.SelectedItem);

            // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
            lesEmployee.ElementAt(indice).NomEmp = txtNomEmp.Text;
            lesEmployee.ElementAt(indice).PrenomEmp = txtPrenomEmp.Text;
            lesEmployee.ElementAt(indice).TypeEmp = cboTypeEmp.Text;
            lesEmployee.ElementAt(indice).MailEmp = txtMailEmp.Text;
            lesEmployee.ElementAt(indice).TelEmp = Convert.ToDecimal(txtTelEmp.Text);

            Employer empModifie = lesEmployee.ElementAt(indice);
            bdd.UpdateEmployer(
                empModifie.IDEmp,
                empModifie.NomEmp,
                empModifie.PrenomEmp,
                empModifie.TypeEmp,
                empModifie.NomEmp,
                empModifie.TelEmp
            );

            dtgEmp.Items.Refresh();
        }

        private void btnSupprimerEmp_Click(object sender, RoutedEventArgs e)
        {
            if (dtgEmp.SelectedIndex >= 0)
            {
                bdd.DeleteEmployer(Convert.ToInt16(txtNumEmp.Text));

                // Actualise magazines dans l'application
                lesEmployee = bdd.SelectEmployer();

                dtgEmp.ItemsSource = lesEmployee;
                dtgEmp.SelectedIndex = 0;
            }
        }


    }
}
