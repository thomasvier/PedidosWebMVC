using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel;
using PedidosWeb.Resources;
using PedidosWeb.Models.Admin;

namespace PedidosWeb.Models.Admin
{
    public class Cliente
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "CampoObrigatorio")]
        [Display(Name="Razão Social")]
        [StringLength(500)]
        public string RazaoSocial { get; set; }

        [Display(Name="Nome Fantasia")]
        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "CampoObrigatorio")]
        [StringLength(500)]
        public string NomeFantasia { get; set; }

        [StringLength(18)]
        [Display(Name="CPF/CNPJ")]
        public string CPFCNPJ { get; set; }

        [Display(Name="Inscrição Estadual")]
        public string InscricaoEstadual { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [DataType(DataType.PhoneNumber)]        
        public string Celular { get; set; }

        [Display(Name="E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage="Insira um e-mail válido.")]
        public string Email { get; set; }

        [Display(Name="Endereço")]
        [StringLength(500)]
        public string Endereco { get; set; }

        [Display(Name="Cidade")]
        [StringLength(500)]
        public string Cidade { get; set; }

        [StringLength(500)]
        public string Bairro { get; set; }

        [StringLength(2)]
        public string Estado { get; set; }

        [Display(Name="Número")]
        public string Numero { get; set; }

        public string Cep { get; set; }

        [StringLength(500)]
        public string Complemento { get; set; }
        
        public bool Ativo { get; set; }

        public TipoTitular Tipo { get; set; }

        public Cliente()
        {
            Tipo = TipoTitular.PessoaJuridica;
            Ativo = true;
        }
    }
}