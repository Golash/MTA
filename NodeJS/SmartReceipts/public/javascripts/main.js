/**
 * Created by t-nawol on 14-Sep-16.
 */
function tabButtonClicked(btn){
    var tabId = btn.id.replace("btn","tab");
    $(".tab-view").each(function() {
        $( this ).addClass("tab-view-inactive")
    });
    $("#"+tabId).removeClass("tab-view-inactive");
}

function clean() {
}

function addProduct() {

}
function sendReceipt() {
    var customerName = $("#customer-name").val();
    var customerId = $("#customer-id").val();
    var BusinessName = $("#Business").val();
    var Category = $("#Category").val();
    var LastFourNumbers = $("#LastFourNumbers").val();
    var Product = $("#Product").val();

    var data = getData();
    data.Customer.Name = customerName;
    data.Customer.Id = customerId;
    data.Business.Name = BusinessName;
    data.Business.Category = Category;
    data.CreditCard.LastFourNumbers = LastFourNumbers;
    
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: "/api/receipts/send",
        data: data,
        async: false,
        success: function (data) {
            alert("Sent!");
        },
        error: function (err) {
            alert("Failed!" + err);
        }
    });
}

function getData() {
    return {
        "RequestedBy" : "12214112412",
        "Customer": {
            "Id": "123456789",
            "Name": "Israel Israeli"
        },
        "Business": {
            "Id": "12214112412",
            "Name": "Aroma",
            "Category": "Food"
        },
        "CreditCard": {
            "LastFourNumbers": "1234"
        },
        "Products": [
            {
                "Name": "Ice Coffae",
                "Amount": "2",
                "Price": "16.00",
                "TotalPrice": "32"
            },
            {
                "Name": "Omelet Sandwiche",
                "Amount": "2",
                "Price": "29.00",
                "TotalPrice": "58"
            }
        ],
        "TotalPrice": "90"
    }
}