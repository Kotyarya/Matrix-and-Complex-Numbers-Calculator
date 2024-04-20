using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Projekt1_Aksamitnyi62325
{ 
    public class Complex
    {
        public double realPart { get; private set; } = 0;
        public double imaginaryPart { get; private set; } = 0;

        public Complex(double realPart, double imaginaryPart)
        {
            this.realPart = realPart;
            this.imaginaryPart = imaginaryPart;
        }

        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.realPart + b.realPart, a.imaginaryPart + b.imaginaryPart);
        }

        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.realPart - b.realPart, a.imaginaryPart - b.imaginaryPart);
        }

        public static Complex operator *(Complex a, Complex b) 
        {
            double realPart = a.realPart * b.realPart;
            double imaginaryPart = a.realPart * b.imaginaryPart + a.imaginaryPart * b.realPart;

            if (a.imaginaryPart != 0 && b.imaginaryPart != 0)
            {
                realPart = realPart - (a.imaginaryPart * b.imaginaryPart);
            }

            return new Complex(realPart, imaginaryPart);
        }

        public static Complex operator *(Complex a, float num)
        {
            return new Complex(a.realPart * num, a.imaginaryPart * num);
        }

        public static Complex operator /(Complex a, Complex b)
        {
            double realPart = (a * new Complex(b.realPart, b.imaginaryPart * -1)).realPart / (b * new Complex(b.realPart, b.imaginaryPart * -1)).realPart;
            double imaginaryPart = (a * new Complex(b.realPart, b.imaginaryPart * -1)).imaginaryPart / (b * new Complex(b.realPart, b.imaginaryPart * -1)).realPart;


            return new Complex(realPart,imaginaryPart);
        }

        public static bool operator ==(Complex a, Complex b)
        {
            return a.realPart == b.realPart && a.imaginaryPart == b.imaginaryPart;
        }

        public static bool operator !=(Complex a, Complex b)
        {
            return !(a == b);
        }

        public static bool operator <(Complex a, Complex b)
        {
            if ((a.realPart < b.realPart) && (a.imaginaryPart < b.imaginaryPart))
            {
                return true;
            } 
            else
            {
                return a.Module() < b.Module();
            }
        }

        public static bool operator >(Complex a, Complex b)
        {
            if ((a.realPart > b.realPart) && (a.imaginaryPart > b.imaginaryPart))
            {
                return true;
            }
            else
            {
                return a.Module() > b.Module();
            }
        }

        public static bool operator <=(Complex a, Complex b)
        {
            return (a < b || a == b);  
        }

        public static bool operator >=(Complex a, Complex b)
        {
            return (a > b || a == b);
        }

        public static Complex One()
        {
            return new Complex(1, 1);   
        }

        public static Complex Zero()
        {
            return new Complex(0, 0);   
        }

        public double Module()
        {
            return Math.Sqrt((this.realPart * this.realPart) + (this.imaginaryPart * this.imaginaryPart));
        }

        public Complex Factorial()
        {

            if (this == Complex.Zero())
            {
                return Complex.One();
            }

            Complex result = Complex.One();

            for (Complex i = Complex.One(); i <= this; i = i + Complex.One())
            {
                result = i * result;
            }

            return result;              
        }

        public Complex SubFactorial()
        {
            if (this == Complex.Zero())
            {
                return Complex.One();
            }

            Complex result = Complex.One();
            ulong count = 1;

            for(Complex i = Complex.Zero();i <= this; i += Complex.One())
            {
                if (count % 2 == 0) 
                {
                    result += i;
                }
                else
                {
                    result -= i;
                }
                count++;
            }

            return this.Factorial() * (Complex.One() + result);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is Complex))
            {
                return false;
            }

            Complex b = obj as Complex;

            return this == b;
        }

        public override string ToString()
        {
            string realPart = this.realPart.ToString("0.00");
            string imaginaryPart = this.imaginaryPart < 0 ? (this.imaginaryPart * -1).ToString("0.00") : this.imaginaryPart.ToString("0.00");

            return this.imaginaryPart >= 0 ? $"{realPart} + {imaginaryPart}i" : $"{realPart} - {imaginaryPart}i";
        }
    }

}

