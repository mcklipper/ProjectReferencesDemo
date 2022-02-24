using ProjectReferencesDemo.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace ProjectReferencesDemo.Desktop
{
    public partial class MainForm : Form
    {
        private readonly ApplicationDbContext context;

        public MainForm()
        {
            InitializeComponent();
            context = new();

            foreach (var customer in context.Customers)
            {
                dgvCustomers.Rows.Add(new object[] 
                {
                    customer.Id,
                    customer.Name,
                    customer.DateOfRegistration.ToString("yyyy. MMMM dd. HH:mm"),
                    customer.Age,
                    customer.Gender ? "Férfi" : "Nõ"
                });
            }


        }
    }
}