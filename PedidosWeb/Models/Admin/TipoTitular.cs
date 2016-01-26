using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PedidosWeb.Models.Admin
{
    public enum TipoTitular
    {
        [Display(Name="Pessoa Física")]
        PessoaFisica = 0,
        [Display(Name = "Pessoa Jurídica")]
        PessoaJuridica = 1
    }
}