
function addItem(articleId, quantity) {

    itemAction(articleId, quantity, 'AddItemJson', 'Article has been successfully added!');
}

function removeItem(articleId, quantity) {

    $.ajax({
        type: 'DELETE',
        url: "./Cart/RemoveItem",
        data: "{'articleId': " + articleId + ", 'quantity': " + quantity + "}",
        cache: false,
        contentType: 'application/json',
        success: function (data) {
            toastr.success('Article has been successfully removed!');
            //debugger;
            $('#cart > #cartItems').html(data);
            refreshCartMenu();
        },
        error: function () {
            toastr.error("Error at removing cart item.");
        }
    });
}

function itemAction(articleId, quantity, method, message) {

    $.ajax({        
        url: "./Cart/" + method,
        type: 'POST',
        data: "{'articleId': " + articleId + ", 'quantity': " + quantity + "}",
        traditional: true,
        contentType: 'application/json',
        success: function (data) {
            toastr.success(message);
            refreshCartMenu();
        },
        error: function () {
            toastr.error("Error at " + method + " of the cart.");
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