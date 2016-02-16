using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PedidosWeb.Models.Admin
{
    public class Representante
    {
        public Representante()
        {
            Ativo = true;
        }

        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="{0:000000}")]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "CampoObrigatorio")]
        [StringLength(500)]
        public string Nome { get; set; }

        [StringLength(500)]
        [Display(Name="E-mail")]
        public string Email { get; set; }

        [StringLength(20)]
        public string Telefone { get; set; }

        [StringLength(20)]
        public string Celular { get; set; }

        public bool Ativo { get; set; }
    }
}