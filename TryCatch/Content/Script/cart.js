
function addItem(articleId, quantity) {

    itemAction(articleId, quantity, 'AddItemJson', 'Article has been successfully added!');
}

function removeItem(articleId, quantity) {

    itemAction(articleId, quantity, 'RemoveItemJson', 'Article has been successfully removed!');

    window.location.reload();
}

function itemAction(articleId, quantity, method, message) {

    $.ajax({        
        url: "./Cart/" + method,
        type: 'POST',
        data: "{'articleId': " + articleId + ", 'quantity': " + quantity + "}",
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            toastr.success(message);
            refreshCartMenu();
        },
        error: function () {
            toastr.error("An error has occured!");
        }
    });
}

function refreshCartMenu() {

    $.ajax({
        type: 'GET',
        url: './Cart/GetJson/',
        cache: false,
        contentType: 'application/json',
        success: function (data) {
            if (eval(data).Items.length > 0)
                $("#cartMenu").html('Cart (' + eval(data).Items.length + ')');
            else
                $("#cartMenu").html('Cart');
        },
        error: function () {
            toastr.error('Error at creating new session cart');
        }
    });
}

refreshCartMenu();