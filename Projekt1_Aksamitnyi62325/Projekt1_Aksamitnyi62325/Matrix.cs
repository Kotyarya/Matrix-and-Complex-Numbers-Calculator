using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt1_Aksamitnyi62325
{
    public class Matrix
    {
      public ushort rowsCount { get; private set;} = 0;
      public ushort columnsCount { get; private set; } = 0;
      public float[,] matrix = new float[0, 0];  
        
        public Matrix (ushort rowsCount = 0, ushort columnsCount = 0)
        {
            this.columnsCount = columnsCount;
            this.rowsCount = rowsCount;
            this.matrix = new float[this.rowsCount, columnsCount];
        }


        public void RewriteDataGridViewToMatrix (DataGridView dgv)
        {
            for (ushort i = 0; i < this.rowsCount; i++)
            {
                for (ushort j = 0; j < this.columnsCount;j++)
                {
                    this.matrix[i, j] = float.Parse(dgv.Rows[i].Cells[j].Value.ToString());
                }
            }
        }

        public void RewriteMatrixToDataGridView (DataGridView dgv)
        {
            for (ushort i = 0; i < this.rowsCount; i++)
            {
                for (ushort j = 0; j < this.columnsCount; j++)
                {
                    dgv.Rows[i].Cells[j].Value = string.Format("{0:F3}", this.matrix[i, j]);
                }
            }

            dgv.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders); 
        }

        public bool isBlank()
        {
            for (ushort i = 0; i < this.rowsCount; i++)
            {
                for (ushort j = 0; j < this.columnsCount; j++)
                {
                    if (this.matrix[i,j] != 0)
                    {
                           return false;
                    }
                }
            }

            return true;
        }


        public static Matrix operator +(Matrix a, Matrix b) 
        {
            if (a.rowsCount != b.rowsCount || a.columnsCount != b.columnsCount)
            {
                throw new ArgumentException("Error");
            }
            else 
            {
                Matrix c = new Matrix(a.rowsCount, a.columnsCount);

                for (ushort i = 0; i < a.rowsCount; i++)
                {
                    for (ushort j = 0; j < a.columnsCount; j++)
                    {
                        c.matrix[i, j] = a.matrix[i, j] + b.matrix[i, j];
                    }
                }

                return c;
            }
        } 

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.rowsCount != b.rowsCount || a.columnsCount != b.columnsCount)
            {
                throw new ArgumentException("Error");
            }
            else
            {
                Matrix c = new Matrix(a.rowsCount, a.columnsCount);

                for (ushort i = 0; i < a.rowsCount; i++)
                {
                    for (ushort j = 0; j < a.columnsCount; j++)
                    {
                        c.matrix[i, j] = a.matrix[i, j] - b.matrix[i, j];
                    }
                }

                return c;
            }
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.rowsCount != b.columnsCount)
            {
                throw new ArgumentException("Error");
            }
            else if (a.rowsCount == 1 && a.columnsCount == 1 && b.rowsCount == 1 && b.columnsCount == 1)
            {
                Matrix c = new Matrix(1, 1);
                c.matrix[0, 0] = a.matrix[0,0] * b.matrix[0,0];

                return c;
            }
            else
            {
                Matrix c = new Matrix(a.rowsCount, b.columnsCount);

                for(ushort i = 0; i < a.rowsCount; i++)
                {
                    for (ushort j = 0;j < b.columnsCount; j++)
                    {
                        c.matrix[i, j] = a.matrix[i,0]*b.matrix[0,j] + a.matrix[i,1]*b.matrix[1,j];
                      
                    }
                }
                return c;
            }
        }

        public static Matrix operator !(Matrix a)
        {
            Matrix c = new Matrix(a.columnsCount, a.rowsCount);

            for (ushort i = 0; i < a.rowsCount; i++)
            {
                for (ushort j = 0; j < a.columnsCount; j++)
                {
                    c.matrix[j, i] = a.matrix[i, j];
                }
            }

            return c;
        }

        public static bool operator ==(Matrix a, Matrix b)
        {
            if (a.rowsCount != b.rowsCount || a.columnsCount != b.columnsCount)
            {
               return false;
            }
            else
            {
                for (ushort i = 0; i < a.rowsCount; i++)
                {
                    for(ushort j = 0; j < a.columnsCount; j++)
                    {
                        if (a.matrix[i,j] != b.matrix[i,j])
                        {
                            return false;
                        }
                    }
                }
            }
            return true;    
        }

        public static bool operator !=(Matrix a, Matrix b) 
        {
            return !(a == b);   
        }

        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is Matrix))
            {
                return false;   
            }
            else
            {
                Matrix c = obj as Matrix;
                for (ushort i = 0; i < c.rowsCount; i++)
                {
                    for (ushort j = 0;j< c.columnsCount; j++)
                    {
                        if (this.matrix[i,j] != c.matrix[i,j]) { return false; }    
                    }
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return this.matrix.GetHashCode();
        }
    }
}
