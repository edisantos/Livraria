/*Aquivos Controller para Livros*/

livrosApp.controller('livrosCtrl', function ($scope, livrosService) {

    CarregarLivros(); // Listando todos os dados no load da pagina

    function CarregarLivros() {
        var listarLivros = livrosService.listarTodosLivros();

        listarLivros.then(function (d) {
            $scope.livros = d.data;
        },
            function () {
                alert("Erro ao listar os livros");
            });
    }
});