using Biblioteca.Models.Entities;
using System.Runtime.InteropServices;

namespace BibliotecaMunicipal.Models.Entities
{
    public class Libro
    {
        public Guid Id { get; set; }
        public required string Titulo { get; set; }
        public required string Autor { get; set; }
        public string? Editorial { get; set; }
        public required int AñoPublicacion { get; set; }
        public bool EstadoPrestado { get; set; }

        private string _codigoISBN;
        public required string CodigoISBN
        {
            get => _codigoISBN;
            set
            {
                //valida  el ISBN antes de ocuparlo
                if (!ValidarISBN(value))
                    throw new ArgumentException("El codigo ISBN no es valido", nameof(CodigoISBN));
                _codigoISBN = value;

            }
        }


        //Lista de prestamos asociados a este libro
        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

        public Libro()
        {
        }

        public Libro(Guid id, string titulo, string autor, string? editorial, int añoPublicacion, bool estadoPrestamo, string codigoISBN)
        {

            this.Id = id;
            this.Titulo = titulo;
            this.Autor = autor;
            this.Editorial = editorial;
            this.AñoPublicacion = añoPublicacion;
            this.EstadoPrestado = estadoPrestamo;
            this.CodigoISBN = codigoISBN;

        }


        //metodo privado  que determina  si un ISBN es valido  mientras sea ISBN10 o ISBN13
        private bool ValidarISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return false;
            //elimina guiones  y espacios
            isbn = isbn.Replace("-", "").Replace(" ", "");

            //llamam al validador  correspondido segun la longitud 
            if (isbn.Length == 10)
                return ValidarISBN10(isbn);
            else if (isbn.Length == 13)
                return ValidarISBN13(isbn);
            else
                return false;
        }

        //valida  un ISBN10 mediante calculos de suma ponderada
        private bool ValidarISBN10(string isbn10)
        {
            if (isbn10.Length != 10)
                return false;

            //los primeros 9 carcacteres deben ser digitos
            for (int i = 0; i < 9; i++)
            {
                if (!char.IsDigit(isbn10[i]))
                    return false;
            }

            int suma = 0;
            for (int i = 0; i < 9; i++)
            {
                suma += (10 - i) * (isbn10[i] - '0');
            }

            //ultimo digito puede ser x equivale a 10 o un digito 
            char ultimo = isbn10[9];
            int digitalControl;

            if (ultimo == 'X' || ultimo == 'x')
            {
                digitalControl = 10;
            }
            else if (char.IsDigit(ultimo))
            {
                digitalControl = ultimo - '0';
            }
            else
            {
                return false;
            }

            suma += digitalControl;

            // el ISBN10 es valido si la suma  es divisible por 11
            return (suma % 11 == 0);
        }

        //valida el ISBN13 usando el algoritmo de suma ponderada  alterna 1 y 3
        private bool ValidarISBN13(string isbn13)
        {
            //debe tener exactamente 13 digitos
            if (isbn13.Length != 13 || !isbn13.All(char.IsDigit))
                return false;

            int suma = 0;
            for (int i = 0; i < 12; i++)
            {
                int digito = isbn13[i] - '0';
                // alterna entre 1 y 3 la posicion 
                if (i % 2 == 0)
                {
                    suma += digito;
                }
                else
                {
                    suma += digito * 3;
                }
            }
            //calcula el digito de control
            int digitalControlCaldulado;
            int resto = suma % 10;

            if (resto == 0)
            {
                digitalControlCaldulado = 0;
            }
            else
            {
                digitalControlCaldulado = 10 - resto;
            }
            //compara con el digito real de control en la posicion 13
            int digitoControlReal = isbn13[12] - '0';

            return digitalControlCaldulado == digitoControlReal;
        }
    }
}

