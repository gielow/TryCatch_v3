
function addItem(articleId, quantity) {

    itemAction(articleId, quantity, 'POST', 'AddItem', 'Article has been successfully added!');
}

function removeItem(articleId, quantity) {

    itemAction(articleId, quantity, 'DELETE', 'RemoveItem', 'Article has been successfully removed!');

    window.location.reload();
}

function itemAction(articleId, quantity, action, method, message) {
    //var cartGuid = getCartGuid();
    
    /*$.ajax({
        type: 'POST',
        url: './Cart/'+action+'/' + articleId + '/' + quantity,
        cache: false,
        contentType: 'application/json',
        success: function (data) {
            alert(message);
        },
        error: function () {
            return false;
        }
    });*/

    toastr.success("The item was added the cart!", "Cart");

    $.ajax({
        //url: "./Cart/" + action + "/" + articleId + "/" + quantity,
        url: "./Cart/" + method,
        type: action,
        data: "{'articleId': " + articleId + ", 'quantity': " + quantity + "}",
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.status == "Success") {
                alert("Done");
                //$(element).closest("form").submit(); //<------------ submit form
            } else {
                alert("Error occurs on the Database level!");
            }
        },
        error: function () {
            alert("An error has occured!!!");
        }
    });
}
/*
function getCartGuid() {
    if (sessionStorage.getItem("CartGuid") !== null && sessionStorage.getItem("CartGuid").length > 0)
        return sessionStorage.getItem("CartGuid");

    $.ajax({
        type: 'GET',
        url: './Cart/New',
        cache: false,
        contentType: 'application/json',
        headers: { Accept: "application/json" },
        success: function (data) {
            debugger;
            sessionStorage.setItem("CartGuid", eval(data).Guid);

            return eval(data).Guid;
        },
        error: function () {
            console.error('Error at creating new session cart');
        }
    });
}

getCartGuid();*/