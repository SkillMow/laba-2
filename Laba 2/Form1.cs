using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace YourAppName
{
    public partial class Form1 : Form
    {
        private List<Organization> organizations = new List<Organization>();

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            Organization newOrganization = new Organization
            {
                Name = txtName.Text,
                Address = txtAddress.Text,
                YearFounded = Convert.ToInt32(txtYearFounded.Text)
            };

            organizations.Add(newOrganization);
            await Serializer.SerializeObjectAsync(organizations, "organizations.xml");
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            organizations = await Serializer.DeserializeObjectAsync<List<Organization>>("organizations.xml");
            DisplayOrganizations();
        }

        private void DisplayOrganizations()
        {
            dataGridView1.Rows.Clear();
            foreach (Organization org in organizations)
            {
                dataGridView1.Rows.Add(org.Name, org.Address, org.YearFounded);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            List<Organization> searchResults = organizations.FindAll(org =>
                org.Name.Contains(searchText) || org.Address.Contains(searchText));

            dataGridView1.Rows.Clear();
            foreach (Organization org in searchResults)
            {
                dataGridView1.Rows.Add(org.Name, org.Address, org.YearFounded);
            }
        }
    }
}
