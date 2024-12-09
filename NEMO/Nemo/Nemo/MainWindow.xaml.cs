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

        List<Employee> lesEmployee = new List<Employee>();

        public MainWindow()
        {
            bdd.Initialize();
            InitializeComponent();

            lesEmployee = bdd.SelectEmployee();

            dtgEmp.ItemsSource = lesEmployee;

            dtgEmp.SelectedIndex = 0;

            cboTypeEmp.ItemsSource = lesEmployee;
            cboTypeEmp.SelectedValuePath = "IdEmp";
        }


        // ------------- Employer --------------
        private void dtgEmp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             selectedProspect = dtgProspect.SelectedItem as Prospect;
            if (dtgProspect.SelectedItem != null)
            {
                try
                {
                    //Remplissage des Textboxs avec les données de l'objet Contrat selectedContrat récupéré dans le Datagrid dtgContrat
                    txtNumProspect.Text = Convert.ToString(selectedProspect.IdProspect);
                    txtNomProspect.Text = selectedProspect.NomProspect;
                    txtPrenomProspect.Text = Convert.ToString(selectedProspect.PrenomProspect);
                    txtTelProspect.Text = Convert.ToString(selectedProspect.TelephoneProspect);
                    txtMailProspect.Text = Convert.ToString(selectedProspect.EmailProspect);
                    dtpCeationProspect.Text = Convert.ToString(selectedProspect.DateCreation);
                    //cboProspectCommercial.SelectedIndex = selectedProspect.LeCommercial;
                    cboProspectCommercial.SelectedValue = selectedProspect.LeCommercial.IdCommerciaux;

                    // Sélection du pigiste concerné dans la Combobox
                    //cboPigiste.SelectedItem = selectedContrat.PigisteContrat;

                    int i = 0;
                    bool trouve = false;

                    while (i < cboProspectCommercial.Items.Count && trouve == false)
                    {
                        if (Convert.ToString(cboProspectCommercial.Items[i]) == Convert.ToString(selectedProspect.LeCommercial))
                        {
                            trouve = true;
                            cboProspectCommercial.SelectedIndex = i;
                        }
                        i++;
                    }


                }
                catch (Exception)
                {

                    Console.WriteLine("Erreur sur la mise à jour du formulaire lors du changement dans le Datagrid dtgContrat");
                }
            }
        }

        private void btnAjouterEmp_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProspect.SelectedIndex >= 0)
            {
                // Récupération du Pigiste sélectionné dans le Combobox cboPigiste
                Commerciaux ModifCommercial = cboProspectCommercial.SelectedItem as Commerciaux;


                Prospect tmpProspect = new Prospect(0, txtNomProspect.Text, txtPrenomProspect.Text, txtTelProspect.Text, txtMailProspect.Text, dtpCeationProspect.Text, (Commerciaux)cboProspectCommercial.SelectedItem);
                bdd.InsertProspect(tmpProspect.NomProspect, tmpProspect.PrenomProspect, tmpProspect.TelephoneProspect, tmpProspect.EmailProspect, tmpProspect.DateCreation, tmpProspect.LeCommercial);

                // Mets a jours pigistes
                lesProspect = bdd.SelectProspect();

                // Met à jour le DataGrid
                dtgProspect.ItemsSource = lesProspect;
                dtgProspect.SelectedIndex = 0;
                dtgProspect.Items.Refresh();
            }
        }

        private void btnModifierEmp_Click(object sender, RoutedEventArgs e)
        {
            //On recherche à quel indice de la collection se trouve l'object selectionné dans le datagrid
            int indice = lesProspect.IndexOf((Prospect)dtgProspect.SelectedItem);

            // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
            lesProspect.ElementAt(indice).NomProspect = txtNomProspect.Text;
            lesProspect.ElementAt(indice).PrenomProspect = txtPrenomProspect.Text;
            lesProspect.ElementAt(indice).TelephoneProspect = txtTelProspect.Text;
            lesProspect.ElementAt(indice).EmailProspect = txtMailProspect.Text;
            lesProspect.ElementAt(indice).DateCreation = dtpCeationProspect.Text;
            lesProspect.ElementAt(indice).LeCommercial = (Commerciaux)cboProspectCommercial.SelectedItem;

            Prospect prospectModifie = lesProspect.ElementAt(indice);
            bdd.UpdateProspect(
                prospectModifie.IdProspect,
                prospectModifie.NomProspect,
                prospectModifie.PrenomProspect,
                prospectModifie.TelephoneProspect,
                prospectModifie.EmailProspect,
                prospectModifie.DateCreation,
                prospectModifie.LeCommercial.IdCommerciaux // Ici on passe l'ID du commercial
            );

            dtgProspect.Items.Refresh();
        }

        private void btnSupprimerEmp_Click(object sender, RoutedEventArgs e)
        {
            //On recherche à quel indice de la collection se trouve l'object selectionné dans le datagrid
            int indice = lesProspect.IndexOf((Prospect)dtgProspect.SelectedItem);

            // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
            lesProspect.ElementAt(indice).NomProspect = txtNomProspect.Text;
            lesProspect.ElementAt(indice).PrenomProspect = txtPrenomProspect.Text;
            lesProspect.ElementAt(indice).TelephoneProspect = txtTelProspect.Text;
            lesProspect.ElementAt(indice).EmailProspect = txtMailProspect.Text;
            lesProspect.ElementAt(indice).DateCreation = dtpCeationProspect.Text;
            lesProspect.ElementAt(indice).LeCommercial = (Commerciaux)cboProspectCommercial.SelectedItem;

            Prospect prospectModifie = lesProspect.ElementAt(indice);
            bdd.UpdateProspect(
                prospectModifie.IdProspect,
                prospectModifie.NomProspect,
                prospectModifie.PrenomProspect,
                prospectModifie.TelephoneProspect,
                prospectModifie.EmailProspect,
                prospectModifie.DateCreation,
                prospectModifie.LeCommercial.IdCommerciaux // Ici on passe l'ID du commercial
            );

            dtgProspect.Items.Refresh();
        }


    }
}
