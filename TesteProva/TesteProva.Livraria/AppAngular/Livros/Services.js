/*Arquivo para carregar o Json*/

livrosApp.service('livrosService', function ($http) {
    this.listarTodosLivros = function () {
        return $http.get("/Home/ListarLivros");
    }
})