using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt1_Aksamitnyi62325
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        bool calculatorPagesIsAllowed = true;

        private void Form_SizeChangedHandler(object sender, EventArgs e)
        {
            int width = this.Width - 16;
            int height = this.Height - 40;

            Navigation_TabControl_Form.Width = this.Width < 942 ? 926 : this.Width - 16;
            Navigation_TabControl_Form.Height = this.Height < 669 ? 629 : this.Height - 40;
        }

        private void Form_LoadHandler(object sender, EventArgs e)
        {
            Load_Script();

        }

        private void selectPage_SelectHandler(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == HomePage_TabPage_Navigation)
            {
                calculatorPagesIsAllowed = true;
            } 
            else if (calculatorPagesIsAllowed)
            {
                calculatorPagesIsAllowed = false; 
            }
            else
            {
                e.Cancel = true;
            }
        }

        // Matrices Page

        DataGridView dgvA = new System.Windows.Forms.DataGridView();
        DataGridView dgvB = new System.Windows.Forms.DataGridView();
        DataGridView dgvC = new System.Windows.Forms.DataGridView();
        Matrix MatrixA;
        Matrix MatrixB;
        Matrix MatrixC;

        private void createDataGridView(ushort liczbaWierz, ushort liczbaKolumn, int locationY, DataGridView dgv)
        {
            this.MatricesPage_TabPage_Navigation.Controls.Remove(dgv);
            dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Location = new System.Drawing.Point(matrixSizeConfig_GroupBox_MatricesPage.Location.X + matrixSizeConfig_GroupBox_MatricesPage.Size.Width + 20, locationY);
            dgv.Name = "dataGridView";
            dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dgv.RowTemplate.Height = 24;
            dgv.Size = new System.Drawing.Size(480, 150);
            dgv.TabIndex = 15;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;

            

            dgv.ColumnCount = liczbaKolumn;
            dgv.RowCount = liczbaWierz;




            for (ushort i = 0; i < liczbaKolumn; i++) 
            {
                dgv.Columns[i].Name = "Column " + (i + 1);
                dgv.Columns[i].ValueType = typeof(float);

                for (ushort j = 0; j < liczbaWierz; j++)
                {
                    dgv.Rows[j].Cells[i].Value = 0;
                    dgv.Rows[j].HeaderCell.Value = "Row " + (j + 1);
                }
            }


            this.MatricesPage_TabPage_Navigation.Controls.Add(dgv);
        }

        private void GenerujLosowoWartosci(short min, short max, DataGridView dataGridView)
        {
            Random rnd = new Random();

            for (ushort i = 0;  i < dataGridView.RowCount; i++)
            {
                for (ushort j = 0; j < dataGridView.ColumnCount; j++)
                {
                    dataGridView.Rows[i].Cells[j].Value = (int)(rnd.NextDouble() * (max - min) + min);
                }
            }
        }

        private void changeCellValue__MatrixA_ChangeHandler(object sender, EventArgs e)
        {
            change__MatrixA_Script();
        }

        private void changeCellValue__MatrixB_ChangeHandler(object sender, EventArgs e)
        {
            change__MatrixB_Script();
        }

        private void adding__Matrix_ClickHandler(object sender, EventArgs e)
        {
            
            createDataGridView((ushort)dgvA.RowCount, (ushort)dgvB.ColumnCount, 388, dgvC);
            MatrixC = new Matrix((ushort)dgvC.RowCount, (ushort)dgvC.ColumnCount);
            MatrixC = MatrixA + MatrixB;
            MatrixC.RewriteMatrixToDataGridView(dgvC);
            boolResult_Label_MatricesPage.Visible = false;
        }

        private void subtractionAB__Matrix_ClickHandler(object sender, EventArgs e)
        {
            createDataGridView((ushort)dgvA.RowCount, (ushort)dgvB.ColumnCount, 388, dgvC);
            MatrixC = new Matrix((ushort)dgvC.RowCount, (ushort)dgvC.ColumnCount);
            MatrixC = MatrixA - MatrixB;
            MatrixC.RewriteMatrixToDataGridView(dgvC);
            boolResult_Label_MatricesPage.Visible = false;

        }

        private void subtractionBA__Matrix_ClickHandler(object sender, EventArgs e)
        {
            createDataGridView((ushort)dgvA.RowCount, (ushort)dgvB.ColumnCount, 388, dgvC);
            MatrixC = new Matrix((ushort)dgvC.RowCount, (ushort)dgvC.ColumnCount);
            MatrixC = MatrixB - MatrixA;
            MatrixC.RewriteMatrixToDataGridView(dgvC);
            boolResult_Label_MatricesPage.Visible = false;

        }

        private void multiplication__Matrix_ClickHandler(object sender, EventArgs e)
        {
            createDataGridView((ushort)dgvA.RowCount, (ushort)dgvB.ColumnCount, 388, dgvC);
            MatrixC = new Matrix((ushort)dgvC.RowCount, (ushort)dgvC.ColumnCount);
            MatrixC = MatrixA * MatrixB;
            MatrixC.RewriteMatrixToDataGridView(dgvC);
            boolResult_Label_MatricesPage.Visible = false;

        }

        private void transposed__MatrixA_ClickHandler(object sender, EventArgs e)
        {
            MatrixA = !MatrixA;
            createDataGridView(MatrixA.rowsCount, MatrixA.columnsCount, 22, dgvA);
            MatrixA.RewriteMatrixToDataGridView(dgvA);
            change__MatrixA_Script();
            boolResult_Label_MatricesPage.Visible = false;

        }

        private void transposed__MatrixB_ClickHandler(object sender, EventArgs e)
        {
            MatrixB = !MatrixB;
            createDataGridView(MatrixB.rowsCount, MatrixB.columnsCount, 194, dgvB);
            MatrixB.RewriteMatrixToDataGridView(dgvB);
            change__MatrixB_Script();
            boolResult_Label_MatricesPage.Visible = false;

        }

        private void reset__MatrixA_ClickHandler(object sender, EventArgs e)
        {

            for (int i = 0; i < dgvA.RowCount; i++)
            {
                for (int j = 0; j < dgvA.ColumnCount; j++)
                {
                    dgvA.Rows[i].Cells[j].Value = 0;
                }
            }

            change__MatrixA_Script();
        }

        private void reset__MatrixB_ClickHandler(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvB.RowCount; i++)
            {
                for (int j = 0; j < dgvB.ColumnCount; j++)
                {
                    dgvB.Rows[i].Cells[j].Value = 0;
                }
            }

            change__MatrixB_Script();
        }

        private void isEqual__Matrix_ClickHandler(object sender, EventArgs e)
        {
            this.MatricesPage_TabPage_Navigation.Controls.Remove(dgvC);
            boolResult_Label_MatricesPage.Text = MatrixA == MatrixB ? "Yes, Matrices are Equal" : "No, Matrices are not Equal";
            boolResult_Label_MatricesPage.Visible = true;
        }

        private void isBlank__MatrixA_ClickHandler(object sender, EventArgs e)
        {
            this.MatricesPage_TabPage_Navigation.Controls.Remove(dgvC);
            boolResult_Label_MatricesPage.Text = MatrixA.isBlank() ? "Yes, Matrix A is Blankl" : "No, Matrix B is not Blank";
            boolResult_Label_MatricesPage.Visible = true;
        }

        private void isBlank__MatrixB_ClickHandler(object sender, EventArgs e)
        {
            this.MatricesPage_TabPage_Navigation.Controls.Remove(dgvC);
            boolResult_Label_MatricesPage.Text = MatrixB.isBlank() ? "Yes, Matrix B is Blankl" : "No, Matrix A is not Blank";
            boolResult_Label_MatricesPage.Visible = true;
        }

        private void createDGV__MatrixA_ClickHandler(object sender, EventArgs e)
        {
            createDataGridView(Convert.ToUInt16(rowsCount_Input_matrixSizeConfig.Value), Convert.ToUInt16(columnsCount_Input_matrixSizeConfig.Value), 22, dgvA);
            createDGV__MatrixA_Script();
            dgvA.CellValueChanged += changeCellValue__MatrixA_ChangeHandler;
        }

        private void createDGV__MatrixB_ClickHandler(object sender, EventArgs e)
        {
            createDataGridView(Convert.ToUInt16(rowsCount_Input_matrixSizeConfig.Value), Convert.ToUInt16(columnsCount_Input_matrixSizeConfig.Value), 194, dgvB);
            createDGV__MatrixB_Script();
            dgvB.CellValueChanged += changeCellValue__MatrixB_ChangeHandler;
        }

        private void generateRandomlyValues__MatrixA_ClickHandler(object sender, EventArgs e)
        {
            GenerujLosowoWartosci(0, 10, dgvA);
        }

        private void generateRandomlyValues__MatrixB_ClickHandler(object sender, EventArgs e)
        {
            GenerujLosowoWartosci(0, 10, dgvB);
        }

        private void acceptanceValues__MatrixA_ClickHandler(object sender, EventArgs e)
        {
            MatrixA = new Matrix((ushort)dgvA.RowCount, (ushort)dgvA.ColumnCount);
            MatrixA.RewriteDataGridViewToMatrix(dgvA);
            acceptanceValues__MatrixA_Script();
        }

        private void acceptanceValues__MatrixB_ClickHandler(object sender, EventArgs e)
        {
            MatrixB = new Matrix((ushort)dgvB.RowCount, (ushort)dgvB.ColumnCount);
            MatrixB.RewriteDataGridViewToMatrix(dgvB);
            dgvB.ReadOnly = true;
            acceptanceValues__MatrixB_Script();
        }

        private void reset__Matrix_ClickHandler(object sender, EventArgs e)
        {           
            boolResult_Label_MatricesPage.Visible = false;
            this.MatricesPage_TabPage_Navigation.Controls.Remove(dgvA);
            this.MatricesPage_TabPage_Navigation.Controls.Remove(dgvB);
            this.MatricesPage_TabPage_Navigation.Controls.Remove(dgvC);
            change__MatrixA_Script();
            change__MatrixB_Script();
            deleteDGV__MatrixA_Script();
            deleteDGV__MatrixB_Script();
        }

        private void deleteDGV__MatrixA_ClickHandler(object sender, EventArgs e)
        {
            MatrixA = null;
            MatricesPage_TabPage_Navigation.Controls.Remove(dgvA);
            deleteDGV__MatrixA_Script();
        }

        private void deleteDGV__MatrixB_ClickHandler(object sender, EventArgs e)
        {
            MatrixB = null;
            MatricesPage_TabPage_Navigation.Controls.Remove(dgvB);
            deleteDGV__MatrixB_Script();

        }

        // Script MatricesPage

        public Button[] operationsButtons;
        Button[] isBlankButtons;
        bool matrixAisAccept = false;
        bool matrixBisAccept = false;   

        private void Load_Script()
        {
             operationsButtons = new Button[]
             { 
                adding_Button_selectedOperation,
                subtractionAB_Button_selectedOperation, 
                subtractionBA_Button_selectedOperation,
                multiplication_Button_selectedOperation,
                transposed__MatrixA_Button_MatricesPage,
                transposed__MatrixB_Button_MatricesPage,
                reset__MatrixA_Button_selectedOperation,
                reset__MatrixB_Button_selectedOperation,
                isEqual__Matrix_Button_selectedOperation
             };

             isBlankButtons = new Button[]
             {
                isBlank__MatrixA_Button_isBlank,
                isBlank__MatrixB_Button_isBlank
             };

            for (int i = 0; i < operationsButtons.Length; i++) 
            {
                operationsButtons[i].Enabled = false;
            }

            for (int i = 0; i < isBlankButtons.Length; i++)
            {
                isBlankButtons[i].Enabled = false;
            }

            generateRandomlyValues__MatrixA_Button_functionalButtons.Enabled = false;
            generateRandomlyValues__MatrixB_Button_functionalButtons.Enabled = false;
            acceptanceValues__MatrixA_Button_functionalButtons.Enabled = false;
            acceptanceValues__MatrixB_Button_functionalButtons.Enabled = false;
            deleteDGV__MatrixA_Button_functionalButtons.Enabled = false;
            deleteDGV__MatrixB_Button_functionalButtons.Enabled = false;
        }

        private void createDGV__MatrixA_Script()
        {
            generateRandomlyValues__MatrixA_Button_functionalButtons.Enabled = true;
            reset__MatrixA_Button_selectedOperation.Enabled = true;
            isBlank__MatrixA_Button_isBlank.Enabled = true;
            acceptanceValues__MatrixA_Button_functionalButtons.Enabled = true;
            deleteDGV__MatrixA_Button_functionalButtons.Enabled = true;
            createDGV__MatrixA_Button_functionalButtons.Enabled = false;

        }

        private void createDGV__MatrixB_Script()
        {
            generateRandomlyValues__MatrixB_Button_functionalButtons.Enabled = true;
            reset__MatrixB_Button_selectedOperation.Enabled = true;
            isBlank__MatrixB_Button_isBlank.Enabled = true;
            acceptanceValues__MatrixB_Button_functionalButtons.Enabled = true;
            deleteDGV__MatrixB_Button_functionalButtons.Enabled = true;
            createDGV__MatrixB_Button_functionalButtons.Enabled = false;
        }

        private void acceptanceValues__MatrixA_Script()
        {
            generateRandomlyValues__MatrixA_Button_functionalButtons.Enabled = false;
            acceptanceValues__MatrixA_Button_functionalButtons.Enabled = false;
            matrixAisAccept = true;
            if (matrixBisAccept) 
            {
                for (int i = 0; i < isBlankButtons.Length; i++)
                {
                    isBlankButtons[i].Enabled = true;
                }

                try
                {
                    Matrix test_matrixA = MatrixA;
                    Matrix test_matrixB = MatrixB;

                    Matrix test_matrixResult = test_matrixA + test_matrixB;
                    adding_Button_selectedOperation.Enabled = true;


                    test_matrixResult = test_matrixA - test_matrixB;
                    subtractionAB_Button_selectedOperation.Enabled = true;
                    subtractionBA_Button_selectedOperation.Enabled = true;

                } catch
                {
                    adding_Button_selectedOperation.Enabled = false;
                    subtractionAB_Button_selectedOperation.Enabled = false;
                    subtractionBA_Button_selectedOperation.Enabled = false;
                }

                try
                {
                    Matrix test_matrixA = MatrixA;
                    Matrix test_matrixB = MatrixB;

                    Matrix test_matrixResult = test_matrixA * test_matrixB;
                    multiplication_Button_selectedOperation.Enabled = true;
                }
                catch
                {
                    multiplication_Button_selectedOperation.Enabled = false;
                }

                transposed__MatrixA_Button_MatricesPage.Enabled = true;
                transposed__MatrixB_Button_MatricesPage.Enabled = true;
                reset__MatrixA_Button_selectedOperation.Enabled = true;
                reset__MatrixB_Button_selectedOperation.Enabled = true;
                isEqual__Matrix_Button_selectedOperation.Enabled = true;
            }
        }

        private void acceptanceValues__MatrixB_Script()
        {
            generateRandomlyValues__MatrixB_Button_functionalButtons.Enabled = false;
            acceptanceValues__MatrixB_Button_functionalButtons.Enabled = false;
            matrixBisAccept = true;
            if (matrixAisAccept)
            {
                for (int i = 0; i < isBlankButtons.Length; i++)
                {
                    isBlankButtons[i].Enabled = true;
                }

                try
                {
                    Matrix test_matrixA = MatrixA;
                    Matrix test_matrixB = MatrixB;

                    Matrix test_matrixResult = test_matrixA + test_matrixB;
                    adding_Button_selectedOperation.Enabled = true;


                    test_matrixResult = test_matrixA - test_matrixB;
                    subtractionAB_Button_selectedOperation.Enabled = true;

                    test_matrixResult = test_matrixB - test_matrixA;
                    subtractionBA_Button_selectedOperation.Enabled = true;

                }
                catch
                {
                    adding_Button_selectedOperation.Enabled = false;
                    subtractionAB_Button_selectedOperation.Enabled = false;
                    subtractionBA_Button_selectedOperation.Enabled = false;
                }

                try
                {
                    Matrix test_matrixA = MatrixA;
                    Matrix test_matrixB = MatrixB;

                    Matrix test_matrixResult = test_matrixA * test_matrixB;
                    multiplication_Button_selectedOperation.Enabled = true;
                }
                catch
                {
                    multiplication_Button_selectedOperation.Enabled = false;
                }

                transposed__MatrixA_Button_MatricesPage.Enabled = true;
                transposed__MatrixB_Button_MatricesPage.Enabled = true;
                reset__MatrixA_Button_selectedOperation.Enabled = true;
                reset__MatrixB_Button_selectedOperation.Enabled = true;
                isEqual__Matrix_Button_selectedOperation.Enabled = true;
            }
        }

        private void deleteDGV__MatrixA_Script()
        {
            createDGV__MatrixA_Button_functionalButtons.Enabled = true;
            deleteDGV__MatrixA_Button_functionalButtons.Enabled = false;
            generateRandomlyValues__MatrixA_Button_functionalButtons.Enabled = false;
            acceptanceValues__MatrixA_Button_functionalButtons.Enabled = false;
        }

        private void deleteDGV__MatrixB_Script()
        {
            createDGV__MatrixB_Button_functionalButtons.Enabled = true;
            deleteDGV__MatrixB_Button_functionalButtons.Enabled = false;
            generateRandomlyValues__MatrixB_Button_functionalButtons.Enabled = false;
            acceptanceValues__MatrixB_Button_functionalButtons.Enabled = false;
        }

        private void change__MatrixA_Script()
        {
            acceptanceValues__MatrixA_Button_functionalButtons.Enabled = true;

            for (int i = 0; i < operationsButtons.Length; i++)
            {
                operationsButtons[i].Enabled = false;
            }

            for (int i = 0; i < isBlankButtons.Length; i++) 
            {
                isBlankButtons[i].Enabled = false;  
            }
        }

        private void change__MatrixB_Script()
        {
            acceptanceValues__MatrixB_Button_functionalButtons.Enabled = true;

            for (int i = 0; i < operationsButtons.Length; i++)
            {
                operationsButtons[i].Enabled = false;
            }

            for (int i = 0; i < isBlankButtons.Length; i++)
            {
                isBlankButtons[i].Enabled = false;
            }
        }

        // Complex Page

        Complex ComplexA = new Complex(0, 0);
        Complex ComplexB = new Complex(0, 0);
        Complex ComplexC = new Complex(0, 0);
        Complex ComplexD = new Complex(0, 0);
        Complex ComplexResult = new Complex(0, 0);
        double ComplexModuleResult;
        bool ComplexBoolResult;

        private void showResult()
        {
            if (ComplexResult.realPart is double.NaN || ComplexC.imaginaryPart is double.NaN)
            {
                result_Label_calculator.Text = "Re + Im * i";
            }
            else
            {
                result_Label_calculator.Text = ComplexResult.ToString();
            }
        }

        private void showNumberResult()
        {
            result_Label_calculator.Text = ComplexModuleResult.ToString("0.000");
        }

        private void showBoolResult()
        {
            result_Label_calculator.Text = ComplexBoolResult ? "Yes" : "No";
        }

        private void adding__Complex_ClickHandler(object sender, EventArgs e)
        {
            ComplexResult = ComplexA + ComplexB;
            showResult();
        }

        private void subtractionAB__Complex_ClickHandler(object sender, EventArgs e)
        {
            ComplexResult = ComplexA - ComplexB;
            showResult();
        }

        private void subtractionBA__Complex_ClickHandler(object sender, EventArgs e)
        {
            ComplexResult = ComplexB - ComplexA;
            showResult();
        }

        private void multiplication__Complex_ClickHandler(object sender, EventArgs e)
        {
            ComplexResult = ComplexA * ComplexB;
            showResult();
        }

        private void division__Complex_ClickHandler(object sender, EventArgs e)
        {
            ComplexResult = ComplexA / ComplexB;
            showResult();
        }

        private void revers__ComplexA_ClickHandler(object sender, EventArgs e)
        {
            ComplexResult = ComplexA * -1;
            showResult();
        }

        private void formula__Complex_ClickHandler(object sender, EventArgs e)
        {
            ComplexResult = (ComplexA + ComplexB * ComplexC) - (ComplexD * ComplexA);
            showResult();
        }

        private void factorial__ComplexA_ClickHandler(object sender, EventArgs e)
        {
            ComplexResult = ComplexA.Factorial();
            showResult();
        }

        private void subfactorial__ComplexA_ClickHandler(object sender, EventArgs e)
        {
            ComplexResult = ComplexA.SubFactorial();
            showResult();
        }

        private void round__ComplexA_ClickHandler(object sender, EventArgs e)
        {
            ComplexModuleResult = ComplexA.Module();
            showNumberResult();
        }

        private void roundSum__Complex_ClickHandler(object sender, EventArgs e)
        {
            ComplexModuleResult = ComplexA.Module() + ComplexB.Module();
            showNumberResult();
        }

        private void isNotEqual__Complex_ClickHandler(object sender, EventArgs e)
        {
            ComplexBoolResult = ComplexA != ComplexB;
            showBoolResult();
        }

        private void isEqual__Complex_ClickHandler(object sender, EventArgs e)
        {
            ComplexBoolResult = ComplexA == ComplexB;
            showBoolResult();
        }

        private void getRealPart__ComplexA_ClickHandler(object sender, EventArgs e)
        {
            ComplexModuleResult = ComplexA.realPart;
            showNumberResult();
        }

        private void getImaginaryPart__ComplexA_ClickHandler(object sender, EventArgs e)
        {
            ComplexModuleResult = ComplexA.imaginaryPart;
            showNumberResult();
        }

        private void realPart__ComplexA_ChangedHandler(object sender, EventArgs e)
        {
            NumericUpDown input = (NumericUpDown)sender;

            ComplexA = new Complex((double)(input.Value),(double)(imaginaryPart__ComplexA_Input_complexSizeConfig.Value));
        }

        private void imaginaryPart__ComplexA_ChangedHandler(object sender, EventArgs e)
        {
            NumericUpDown input = (NumericUpDown)sender;

            ComplexA = new Complex((double)(realPart__ComplexA_Input_complexSizeConfig.Value), (double)(input.Value));
        }

        private void realPart__ComplexB_ChangedHandler(object sender, EventArgs e)
        {
            NumericUpDown input = (NumericUpDown)sender;

            ComplexB = new Complex((double)(input.Value), (double)(imaginaryPart__ComplexB_Input_complexSizeConfig.Value));
        }

        private void imaginaryPart__ComplexB_ChangedHandler(object sender, EventArgs e)
        {
            NumericUpDown input = (NumericUpDown)sender;

            ComplexB = new Complex((double)(realPart__ComplexB_Input_complexSizeConfig.Value), (double)(input.Value));
        }

        private void realPart__ComplexC_ChangedHandler(object sender, EventArgs e)
        {
            NumericUpDown input = (NumericUpDown)sender;

            ComplexC = new Complex((double)(input.Value), (double)(imaginaryPart__ComplexC_Input_complexSizeConfig.Value));
        }

        private void imaginaryPart__ComplexC_ChangedHandler(object sender, EventArgs e)
        {
            NumericUpDown input = (NumericUpDown)sender;

            ComplexC = new Complex((double)(realPart__ComplexC_Input_complexSizeConfig.Value), (double)(input.Value));
        }

        private void realPart__ComplexD_ChangedHandler(object sender, EventArgs e)
        {
            NumericUpDown input = (NumericUpDown)sender;

            ComplexD = new Complex((double)(input.Value), (double)(imaginaryPart__ComplexD_Input_complexSizeConfig.Value));
        }

        private void imaginaryPart__ComplexD_ChangedHandler(object sender, EventArgs e)
        {
            NumericUpDown input = (NumericUpDown)sender;

            ComplexD = new Complex((double)(realPart__ComplexD_Input_complexSizeConfig.Value), (double)(input.Value));
        }

        private void reset__Complex_ClickHandler(object sender, EventArgs e)
        {
            NumericUpDown[] numericUpDowns = {
                realPart__ComplexA_Input_complexSizeConfig,
                imaginaryPart__ComplexA_Input_complexSizeConfig,
                realPart__ComplexB_Input_complexSizeConfig,
                imaginaryPart__ComplexB_Input_complexSizeConfig,
                realPart__ComplexC_Input_complexSizeConfig,
                imaginaryPart__ComplexC_Input_complexSizeConfig,
                realPart__ComplexD_Input_complexSizeConfig,
                imaginaryPart__ComplexD_Input_complexSizeConfig
            };


            for (int i = 0; i < numericUpDowns.Length; i++)
            {
                numericUpDowns[i].Value = 0;
                result_Label_calculator.Text = "Re + Im * i";
            }

        }
    }
}
