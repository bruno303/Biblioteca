$(document).ready(function () {
    $('.datatable').DataTable(
        {
            "pagingType": "simple_numbers",
            "scrollY": 400,
            "language": {
                "lengthMenu": "Exibindo _MENU_ registros por página",
                "zeroRecords": "A pesquisa não retornou resultados",
                "info": "Página _PAGE_ de _PAGES_",
                "infoEmpty": "Sem registros disponíveis",
                "infoFiltered": "(filtrado de _MAX_ registros)",
                "infoPostFix": "",
                "thousands": ".",
                "loadingRecords": "Carregando...",
                "processing": "Processando...",
                "search": "Pesquisar:",
                "paginate": {
                    "first": "Primeira",
                    "last": "Última",
                    "next": "Próxima",
                    "previous": "Anterior"
                },
                "aria": {
                    "sortAscending": "Ative para filtrar a coluna ascendentemente",
                    "sortDescending": "Ative para filtrar a coluna descendentemente"
                }
            }
        }
    );
});