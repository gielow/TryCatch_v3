﻿function loadArticles(pageNumber) {
    //debugger;
    $.ajax({
        type: 'GET',        
        //url: 'http://localhost/TryCatchApi_v3/api/Article/Page/' + pageNumber,
        url: './Article/',
        data: "{ 'pageNumber': " + pageNumber + "}",
        cache: false,
        contentType: 'application/json',
        headers: { Accept : "application/json"},
        success: function (data) {
            $('#articles > tbody > tr').remove();
            $.each(eval(data), function (index, item) {
                $('#articles > tbody').append("<tr><td>" + item.Description + "</td><td>" + item.Price + "</td>" +
                    "<td><a href='#' onclick='javascript:addItem(" + item.Id + ", 1)'>Add to cart</a></td></tr>");
            });
        }
    });
}

$(document).ready(function () {
    
    loadArticles(1);
});
