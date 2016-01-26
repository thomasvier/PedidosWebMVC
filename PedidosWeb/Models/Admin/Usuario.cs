using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace PedidosWeb.Models.Admin
{
    public class Usuario
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "CampoObrigatorio")]
        [StringLength(500)]
        public string Nome { get; set; }

        [StringLength(20)]
        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName="CampoObrigatorio")]
        public string Login { get; set; }
        
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength=6, ErrorMessage="O campo Senha deve ter entre 6 a 20 caracterers")]
        public string Senha { get; set; }

        [Display(Name="E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage="Insira um e-mail válido")]         
        [StringLength(100)]
        public string Email { get; set; }

        public bool Ativo { get; set; }

        public TipoUsuario Tipo { get; set; }

        public Usuario()
        {
            Ativo = true;
            Tipo = TipoUsuario.Administrador;
        }
    }
}