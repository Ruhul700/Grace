var app = angular.module('Grace', ['angularUtils.directives.dirPagination', 'akFileUploader', 'naif.base64', 'ngSanitize', 'ui.select',]);
//'ui.grid.selection', 'ui.grid', 'ui.grid.pagination', 'ui.grid.exporter', 'datatables', 
//'angularUtils.directives.dirPagination', 'ngSanitize', 'ui.select', 'angularModalService', 
//'akFileUploader', '720kb.datepicker'
app.run(function ($rootScope, $templateCache) {
    $rootScope.$on('$routeChangeStart', function (event, next, current) {
        if (typeof (current) !== 'undefined') {
            $templateCache.remove(current.templateUrl);
        }
    });
});
app.factory('Data', function () {
    return { obj: '' };
});