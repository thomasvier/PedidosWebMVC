using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedidosWeb.BLL
{
    public class UteisBll
    {
        /// <summary>
        /// Método que recebe uma string com valores separados por ":" e retorna a soma
        /// </summary>
        /// <param name="valores">string de valores separados por ":"</param>
        /// <returns>Soma total</returns>
        public static string CalcularTotal(string valores)
        {
            decimal total = 0;

            if (!string.IsNullOrEmpty(valores))
            {
                foreach (var item in valores.Split(':'))
                {
                    total += decimal.TryParse(item, out total) ? total : 0;
                }
            }

            string totalPedido = string.Format("{0:N2}", total);

            return totalPedido;
        }
    }
}