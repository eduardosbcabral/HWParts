﻿using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Application.ViewModels.Account
{
    public class LoginAccountViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres..", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public LoginAccountViewModel()
        {

        }

        public LoginAccountViewModel(string returnUrl) => ReturnUrl = returnUrl;
    }
}