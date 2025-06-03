using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validation
{
    public class ValidationBd
    {
        public ValidationResultUser ValidationResult(string identificacion, string nombreCompleto,
                                                 string primerNommbre, string segundoNombre,
                                                 string primerApellido, string segundoApellido,
                                                 bool fechaNacimientoValida, string direccion,
                                                 string numeroCelular, string correoElectronico,
                                                 bool fechaCreacionValida,bool estado)
        {
            ValidationResultUser result = new ValidationResultUser();
            if (string.IsNullOrWhiteSpace(identificacion))
            {
                result.Errors.Add("Identificacion Vacia");
                result.IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(nombreCompleto))
            {
                result.Errors.Add(" Nombre Completo esta Vacia");
                result.IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(primerNommbre))
            {
                result.Errors.Add("Primer Nombre vacío.");
                result.IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(segundoNombre))
            {
                result.Errors.Add("Segundo Nombre vacío.");
                result.IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(primerApellido))
            {
                result.Errors.Add("Primer Apellido vacío.");
                result.IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(segundoApellido))
            {
                result.Errors.Add("Segundo Apellid vacío.");
                result.IsValid = false;
            }

            if (!fechaNacimientoValida)
            {
                result.Errors.Add("Formato de fecha de nacimiento incorrecto debe ser yyyy/DD/MM");
                result.IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(direccion))
            {
                result.Errors.Add("Direccion vacío.");
                result.IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(numeroCelular))
            {
                result.Errors.Add("Numero Celular vacío.");
                result.IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(correoElectronico))
            {
                result.Errors.Add("Correo electronico vacío.");
                result.IsValid = false;
            }

            if (!fechaCreacionValida)
            {
                result.Errors.Add("Formato de fecha de creacion incorrecta debe ser yyyy//dd/mm");
                result.IsValid = false;
            }
            if (!estado)
            {
                result.Errors.Add("Valor estado debe ser true or false.");
                result.IsValid = false;
            }
            return result ;
        }
    }
}
