using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SistemaMatriculaURA.Models
{
    public class Pago
    {
        public int Id { get; set; }

        public int MatriculaId { get; set; }

        [ValidateNever]
        public Matricula Matricula { get; set; }

        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string NumeroTransaccion { get; set; } = "";
        public string MetodoPago { get; set; } = "Tarjeta de crédito";
        public string Estado { get; set; } = "Completado";
    }
}
