using Ejercicio04Tema07POO.BL;
using System;
using System.Windows.Forms;

namespace Ejercicio04Tema07.Windows
{
    public partial class FrmElipsesAE : Form
    {
        public FrmElipsesAE()
        {
            InitializeComponent();
        }
        private Elipse elipse;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (elipse!=null)
            {
                SemiejeMayorTextBox.Text = elipse.SemiejeMayor.ToString();
                SemiejeMenorTextBox.Text = elipse.SemiejeMenor.ToString();
            }
        }
        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (elipse==null)
                {
                    elipse = new Elipse();
                }
                elipse.SemiejeMayor = double.Parse(SemiejeMayorTextBox.Text);
                elipse.SemiejeMenor = double.Parse(SemiejeMenorTextBox.Text);
                if (!elipse.Validar())
                {
                    errorProvider1.SetError(SemiejeMayorTextBox, "Valor fuera de rango");
                    errorProvider1.SetError(SemiejeMenorTextBox, "Valor fuera de rango");
                }
                else
                {
                    DialogResult = DialogResult.OK;
                }

            } 
           
            
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (!double.TryParse(SemiejeMayorTextBox.Text, out double tempResult))
            {
                valido = false;
                errorProvider1.SetError(SemiejeMayorTextBox, "Ingrese un valor");
                errorProvider1.SetError(SemiejeMenorTextBox, "Ingrese un valor");
            }
            return valido;
        }

        internal Elipse GetElipse()
        {
            return elipse;
        }

        internal void SetElipse(Elipse elipse)
        {
            this.elipse = elipse;
        }
    }
}
