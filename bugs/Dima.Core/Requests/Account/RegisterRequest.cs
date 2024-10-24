using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Account;

public class RegisterRequest : Request
{
    [Required(ErrorMessage = "E-mail não pode ser vazio.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha não pode ser vazia.")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,32}$", 
        ErrorMessage = @"Senha precisa conter entre 8 e 32 caracteres com números, letras e ao menos 1 caracter especial.")]
    public string Password { get; set; } = string.Empty;
}