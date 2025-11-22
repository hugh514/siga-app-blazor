using System.ComponentModel.DataAnnotations;

namespace SigaApp.RegrasDeNegocio
{
    public class ValidarCpf
    {
        public static ValidationResult? Validar(string cpf, ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return new ValidationResult("O CPF é obrigatório.");

            // remove tudo que não for número
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return new ValidationResult("CPF deve conter 11 dígitos.");

            // rejeita CPFs com todos os números iguais
            if (new string(cpf[0], 11) == cpf)
                return new ValidationResult("CPF inválido.");

            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf[..9];
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * mult1[i];

            int resto = soma % 11;
            int dig1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += dig1;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * mult2[i];

            resto = soma % 11;
            int dig2 = resto < 2 ? 0 : 11 - resto;

            bool valido = cpf.EndsWith($"{dig1}{dig2}");

            return valido
                ? ValidationResult.Success
                : new ValidationResult("CPF inválido.");
        }
    }
}
