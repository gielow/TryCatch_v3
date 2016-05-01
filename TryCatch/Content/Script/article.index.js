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

$(document).ready(function () {
    loadArticles(1);
});
