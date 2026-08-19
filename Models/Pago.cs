using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SistemaMatriculaURA.Models
{
    // Registra la transacción de pago asociada a una matrícula: es la tabla
    // de "transacciones" del sistema. Cada vez que un estudiante se matricula
    // en un curso (EstudianteController.Matricular) se genera un Pago con su
    // propio número de transacción, que luego aparece en el comprobante de
    // matrícula (factura) que el estudiante puede descargar en PDF.
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
