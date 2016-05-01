function loadArticles(pageNumber) {
    $.ajax({
        type: 'GET',        
        url: './Article/Page/?number=' + pageNumber,
        cache: false,
        contentType: 'application/json',
        success: function (data) {
            $('#articles > tbody').html(data);
        },
        error: function () {
            toastr.error("Error at loading articles.");
        }
    });
}

function showArticle(id) {
    $.ajax({
        type: 'GET',
        url: './Article/Details/' + id + '/?partial=true',
        cache: false,
        contentType: 'application/json',
        success: function (data) {
            // Put article HTML inside de modal div
            $('#articleModal .modal-body').html(data);
            // Call bootstrap funtions to show the modal
            $('#articleModal').modal('toggle');
            $('#articleModal').modal('show');
        },
        error: function () {
            toastr.error("Error at load article.");
        }
    });
}

$(document).ready(function () {
    loadArticles(1);
});
