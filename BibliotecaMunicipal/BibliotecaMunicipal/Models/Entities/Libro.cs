using Biblioteca.Models.Entities;

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
                if (!ValidarISBN(value))
                    throw new ArgumentException("El codigo ISBN no es valido", nameof(CodigoISBN));
                _codigoISBN = value;

            }
        }


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



        private bool ValidarISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return false;

            isbn = isbn.Replace("-", "").Replace(" ", "");

            if (isbn.Length == 10)
                return ValidarISBN10(isbn);
            else if (isbn.Length == 13)
                return ValidarISBN13(isbn);
            else
                return false;
        }


        private bool ValidarISBN10(string isbn10)
        {
            if (isbn10.Length != 10)
                return false;
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

            return (suma % 11 == 0);
        }

        private bool ValidarISBN13(string isbn13)
        {
            if (isbn13.Length != 13 || !isbn13.All(char.IsDigit))
                return false;

            int suma = 0;
            for (int i = 0; i < 12; i++)
            {
                int digito = isbn13[i] - '0';

                if (i % 2 == 0)
                {
                    suma += digito;
                }
                else
                {
                    suma += digito * 3;
                }
            }

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

            int digitoControlReal = isbn13[12] - '0';

            return digitalControlCaldulado == digitoControlReal;
        }
    }
}

