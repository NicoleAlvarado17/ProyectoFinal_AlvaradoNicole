using SistemaMatriculaURA.Models;
using Xunit;

namespace ProyectoFinal_AlvaradoNicole.Tests
{
    // Pruebas de la utilidad de paginación usada en el catálogo de cursos,
    // Gestión de Cursos y Gestión de Carreras.
    public class PaginatedListTests
    {
        [Fact]
        public void CalculaElTotalDePaginasRedondeandoHaciaArriba()
        {
            var items = new List<int> { 4, 5, 6 };

            // 20 elementos en total, 3 por página -> 7 páginas (ceil(20/3))
            var pagina = new PaginatedList<int>(items, count: 20, pageIndex: 2, pageSize: 3);

            Assert.Equal(7, pagina.TotalPages);
            Assert.Equal(2, pagina.PageIndex);
            Assert.Equal(20, pagina.TotalCount);
            Assert.Equal(3, pagina.Count);
        }

        [Fact]
        public void LaPrimeraPaginaNoTienePaginaAnterior()
        {
            var pagina = new PaginatedList<int>(new List<int> { 1, 2 }, count: 10, pageIndex: 1, pageSize: 2);

            Assert.False(pagina.HasPreviousPage);
            Assert.True(pagina.HasNextPage);
        }

        [Fact]
        public void LaUltimaPaginaNoTienePaginaSiguiente()
        {
            // 10 elementos, 2 por página -> 5 páginas; en la página 5 no hay siguiente.
            var pagina = new PaginatedList<int>(new List<int> { 9, 10 }, count: 10, pageIndex: 5, pageSize: 2);

            Assert.True(pagina.HasPreviousPage);
            Assert.False(pagina.HasNextPage);
        }

        [Fact]
        public void UnaSolaPaginaNoTieneAnteriorNiSiguiente()
        {
            var pagina = new PaginatedList<int>(new List<int> { 1, 2, 3 }, count: 3, pageIndex: 1, pageSize: 8);

            Assert.Equal(1, pagina.TotalPages);
            Assert.False(pagina.HasPreviousPage);
            Assert.False(pagina.HasNextPage);
        }
    }
}
