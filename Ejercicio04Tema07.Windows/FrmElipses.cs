using Ejercicio04Tema07POO.BL;
using Ejercicio04Tema07POO.DL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ejercicio04Tema07.Windows
{
    public partial class FrmElipses : Form
    {
        public FrmElipses()
        {
            InitializeComponent();
        }
        private RepositorioElipses _repositorio;
        private List<Elipse> _lista;
        private void FrmElipses_Load(object sender, EventArgs e)
        {
            _repositorio = new RepositorioElipses();

            try
            {
                _lista = _repositorio.GetLista();
                MostrarDatosEnGrilla();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void MostrarDatosEnGrilla()
        {
            DatosDataGridView.Rows.Clear();
            foreach (var elipse in _lista)
            {
                DataGridViewRow r = ConstruirFila();
                SetearFila(r, elipse);
                AgregarFila(r);
            }
        }

        private void AgregarFila(DataGridViewRow r)
        {
            DatosDataGridView.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Elipse elipse)
        {
            r.Cells[colSemiejeMayor.Index].Value = elipse.SemiejeMayor;
            r.Cells[colSemiejeMenor.Index].Value = elipse.SemiejeMenor;
            r.Cells[colArea.Index].Value = elipse.GetArea();
            r.Cells[colPerimetro.Index].Value = elipse.GetPerimetro();

            r.Tag = elipse;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(DatosDataGridView);
            return r;
        }

        private void NuevoToolStripButton_Click(object sender, EventArgs e)
        {
            FrmElipsesAE frm = new FrmElipsesAE();
            frm.Text = "Agregar Elipse";
            DialogResult dr = frm.ShowDialog(this);
            if (dr==DialogResult.OK)
            {
                try
                {
                    Elipse elipse = frm.GetElipse();
                    _repositorio.Agregar(elipse);
                    DataGridViewRow r = ConstruirFila();
                    SetearFila(r, elipse);
                    AgregarFila(r);
                    MessageBox.Show("Nuevo registro añadido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exception)
                {

                    MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void SalirToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BorrarToolStripButton_Click(object sender, EventArgs e)
        {
            if (DatosDataGridView.SelectedRows.Count==0)
            {
                return;
            }
            DataGridViewRow r = DatosDataGridView.SelectedRows[0];
            Elipse elipse = (Elipse)r.Tag;
            DialogResult dr = MessageBox.Show($"¿Eliminar el Semieje Mayor {elipse.SemiejeMayor} y Semieje Menor {elipse.SemiejeMenor}?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr==DialogResult.No)
            {
                return;
            }
            try
            {
                _repositorio.Borrar(elipse);
                BorrarFila(r);
                MessageBox.Show("Eliminado exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BorrarFila(DataGridViewRow r)
        {
            DatosDataGridView.Rows.Remove(r);
        }

        private void EditarToolStripButton_Click(object sender, EventArgs e)
        {
            if (DatosDataGridView.SelectedRows.Count==0)
            {
                return;
            }
            DataGridViewRow r = DatosDataGridView.SelectedRows[0];
            Elipse elipse = (Elipse)r.Tag;
            Elipse elipseCopia = (Elipse)elipse.Clone();

            FrmElipsesAE frm = new FrmElipsesAE();
            frm.Text = "Editar valor de semieje";
            frm.SetElipse(elipse);
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                elipse = frm.GetElipse();
                _repositorio.Editar(elipse, elipseCopia);
                SetearFila(r, elipse);
                MessageBox.Show("Valor Modificado", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void ActualizarToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                _lista = _repositorio.GetLista();
                MostrarDatosEnGrilla();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void descendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _lista = _repositorio.OrdenarDeMayoraMenor();
            MostrarDatosEnGrilla();
        }

        private void ascendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _lista = _repositorio.OrdenarDeMenoraMayor();
            MostrarDatosEnGrilla();
        }
    }
}
